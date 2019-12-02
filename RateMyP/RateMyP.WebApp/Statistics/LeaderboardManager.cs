using Microsoft.EntityFrameworkCore;
using RateMyP.WebApp.Helpers;
using RateMyP.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RateMyP.WebApp.Statistics
    {
    public interface ILeaderboardManager
        {
        Task FullUpdate();
        Task Update(Guid id, EntryType type);
        }

    public class LeaderboardManager : ILeaderboardManager
        {
        private readonly RateMyPDbContext m_context;
        private readonly ITeacherStatisticsAnalyzer m_teacherAnalyzer;
        private readonly ICourseStatisticsAnalyzer m_courseAnalyzer;
        private readonly int m_minimumRatings = int.Parse(ConfigurationManager.AppSettings["LeaderboardEntryThreshold"]);
        private readonly int m_currentYear = int.Parse(ConfigurationManager.AppSettings["CurrentAcademicYear"]);

        public LeaderboardManager(ITeacherStatisticsAnalyzer teacherAnalyzer, ICourseStatisticsAnalyzer courseAnalyzer, RateMyPDbContext context)
            {
            m_context = context;
            m_teacherAnalyzer = teacherAnalyzer;
            m_courseAnalyzer = courseAnalyzer;
            }

        public double GetScore(double aR, int tR, int pR)
            {
            // Score is calculated by multiplying average rating by the lower bound of Wilson score confidence interval for a Bernoulli parameter
            // https://www.evanmiller.org/how-not-to-sort-by-average-rating.html
            // We assume that ratings of 3 and above are considered "positive", whereas 1 and 2 star ratings are "negative"

            const double z = 1.96;
            const double w = 1.9208;
            const double u = 0.9604;
            const double v = 3.8416;
            return (((pR + w) / tR - z * Math.Sqrt((pR * (tR - pR)) / (double)tR + u) / tR) / (1 + v / tR)) * aR;
            }

        public async Task FullUpdate()
            {
            var teachers = await m_context.Teachers.ToListAsync();
            var courses = await m_context.Courses.ToListAsync();
            var teacherLeaderboardEntries = new List<LeaderboardEntry>();
            var courseLeaderboardEntries = new List<LeaderboardEntry>();

            foreach (var teacher in teachers)
                {
                var entry = await GetTeacherEntry(teacher.Id);
                if (entry != null)
                    teacherLeaderboardEntries.Add(entry);
                }

            foreach (var course in courses)
                {
                var entry = await GetCourseEntry(course.Id);
                if (entry != null)
                    courseLeaderboardEntries.Add(entry);
                }

            SetPositions(teacherLeaderboardEntries);
            SetPositions(courseLeaderboardEntries);
            await RefreshLeaderboardEntries(teacherLeaderboardEntries);
            await RefreshLeaderboardEntries(courseLeaderboardEntries);
            }

        public async Task Update(Guid id, EntryType type)
            {
            if (type == EntryType.Teacher)
                await UpdateFromTeacher(id);
            else
                await UpdateFromCourse(id);
            }

        public async Task UpdateFromTeacher(Guid teacherId)
            {
            var leaderboardEntries = await m_context.Leaderboard.Where(e => e.EntryType == EntryType.Teacher).ToListAsync();
            var entry = await GetTeacherEntry(teacherId);
            if (entry == null)
                return;

            var index = leaderboardEntries.FindIndex(x => x.Id.Equals(teacherId));
            if (index != -1)
                leaderboardEntries[index] = entry;
            else
                leaderboardEntries.Add(entry);

            SetPositions(leaderboardEntries);
            await RefreshLeaderboardEntries(leaderboardEntries);
            }

        public async Task UpdateFromCourse(Guid courseId)
            {
            var leaderboardEntries = await m_context.Leaderboard.Where(e => e.EntryType == EntryType.Course).ToListAsync();
            var entry = await GetCourseEntry(courseId);
            if (entry == null)
                return;

            var index = leaderboardEntries.FindIndex(x => x.Id.Equals(courseId));
            if (index != -1)
                leaderboardEntries[index] = entry;
            else
                leaderboardEntries.Add(entry);

            SetPositions(leaderboardEntries);
            await RefreshLeaderboardEntries(leaderboardEntries);
            }

        private static void SetPositions(List<LeaderboardEntry> entries)
            {
            entries.Sort((left, right) => right.AllTimeScore.CompareTo(left.AllTimeScore));
            for (var i = 0; i < entries.Count; i++)
                entries[i].AllTimePosition = i + 1;

            entries.Sort((left, right) => right.ThisYearScore.CompareTo(left.ThisYearScore));
            for (var i = 0; i < entries.Count; i++)
                entries[i].ThisYearPosition = i + 1;
            }

        private async Task<LeaderboardEntry> GetTeacherEntry(Guid teacherId)
            {
            var globalRatingCount = await m_teacherAnalyzer.GetTeacherRatingCount(teacherId);
            if (globalRatingCount < m_minimumRatings)
                return null;

            var entry = new LeaderboardEntry();
            try
                {
                entry = await m_context.Leaderboard.FirstAsync(e => e.Id == teacherId && e.EntryType == EntryType.Teacher);
                }
            catch (InvalidOperationException)
                {
                entry = new LeaderboardEntry
                        {
                        Id = teacherId,
                        EntryType = EntryType.Teacher
                        };
                }

            entry.AllTimeRatingCount = globalRatingCount;
            try
                {
                entry.AllTimeAverage = await m_teacherAnalyzer.GetTeacherAverageMark(teacherId);
                }
            catch (InvalidDataException e)
                {
                if (e.Message.Equals("Teacher has no ratings."))
                    entry.AllTimeAverage = 0;
                }

            entry.ThisYearRatingCount = await m_teacherAnalyzer.GetTeacherRatingCount(teacherId, new DateTime(m_currentYear, 9, 1));
            try
                {
                entry.ThisYearAverage = await m_teacherAnalyzer.GetTeacherAverageMarkInYear(teacherId, m_currentYear);
                }
            catch (InvalidDataException e)
                {
                if (e.Message.Equals("Teacher has no ratings."))
                    entry.ThisYearAverage = 0;
                }

            var globalPositiveReviews = await m_teacherAnalyzer.GetTeacherPositiveRatingCount(teacherId);
            var yearlyPositiveReviews = await m_teacherAnalyzer.GetTeacherPositiveRatingCountInYear(teacherId, m_currentYear);
            entry.AllTimeScore = GetScore(entry.AllTimeAverage, entry.AllTimeRatingCount, globalPositiveReviews);
            entry.ThisYearScore = GetScore(entry.ThisYearAverage, entry.ThisYearRatingCount, yearlyPositiveReviews);
            return entry;
            }

        private async Task<LeaderboardEntry> GetCourseEntry(Guid courseId)
            {
            var globalRatingCount = await m_courseAnalyzer.GetCourseRatingCount(courseId);
            if (globalRatingCount < m_minimumRatings)
                return null;

            var entry = new LeaderboardEntry();
            try
                {
                entry = await m_context.Leaderboard.FirstAsync(e => e.Id == courseId && e.EntryType == EntryType.Course);
                }
            catch (InvalidOperationException)
                {
                entry = new LeaderboardEntry
                    {
                    Id = courseId,
                    EntryType = EntryType.Course
                    };
                }

            entry.AllTimeRatingCount = globalRatingCount;
            try
                {
                entry.AllTimeAverage = await m_courseAnalyzer.GetCourseAverageMark(courseId);
                }
            catch (InvalidDataException e)
                {
                if (e.Message.Equals("Course has no ratings."))
                    entry.AllTimeAverage = 0;
                }

            entry.ThisYearRatingCount = await m_courseAnalyzer.GetCourseRatingCount(courseId, new DateTime(m_currentYear, 9, 1));
            try
                {
                entry.ThisYearAverage = await m_courseAnalyzer.GetCourseAverageMarkInYear(courseId, m_currentYear);
                }
            catch (InvalidDataException e)
                {
                if (e.Message.Equals("Course has no ratings."))
                    entry.ThisYearAverage = 0;
                }

            var globalPositiveReviews = await m_courseAnalyzer.GetCoursePositiveRatingCount(courseId);
            var yearlyPositiveReviews = await m_courseAnalyzer.GetCoursePositiveRatingCountInYear(courseId, m_currentYear);
            entry.AllTimeScore = GetScore(entry.AllTimeAverage, entry.AllTimeRatingCount, globalPositiveReviews);
            entry.ThisYearScore = GetScore(entry.ThisYearAverage, entry.ThisYearRatingCount, yearlyPositiveReviews);
            return entry;
            }

        public async Task RefreshLeaderboardEntries(IEnumerable<LeaderboardEntry> entries)
            {
            foreach (var entry in entries)
                {
                if (m_context.Leaderboard.Find(entry.Id) == null)
                    m_context.Leaderboard.Add(entry);
                else m_context.Leaderboard.Update(entry);
                }
            await m_context.SaveChangesAsync();
            }
        }
    }
