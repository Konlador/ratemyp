using Microsoft.EntityFrameworkCore;
using RateMyP.WebApp.Models.Leaderboard;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Threading.Tasks;

namespace RateMyP.WebApp.Statistics
    {
    public interface ILeaderboardManager
        {
        Task FullUpdateAsync();
        Task UpdateTeacherAsync(Guid teacherId);
        Task UpdateCourseAsync(Guid courseId);
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

        public async Task FullUpdateAsync()
            {
            var teachers = await m_context.Teachers.ToListAsync();
            var courses = await m_context.Courses.ToListAsync();
            var teacherLeaderboardEntries = new List<TeacherLeaderboardEntry>();
            var courseLeaderboardEntries = new List<CourseLeaderboardEntry>();

            foreach (var teacher in teachers)
                {
                var entry = await GetTeacherEntryAsync(teacher.Id);
                if (entry != null)
                    teacherLeaderboardEntries.Add(entry);
                }

            foreach (var course in courses)
                {
                var entry = await GetCourseEntryAsync(course.Id);
                if (entry != null)
                    courseLeaderboardEntries.Add(entry);
                }

            SetPositions(teacherLeaderboardEntries);
            SetPositions(courseLeaderboardEntries);
            await RefreshTeacherLeaderboardEntriesAsync(teacherLeaderboardEntries);
            await RefreshCourseLeaderboardEntriesAsync(courseLeaderboardEntries);
            }

        public async Task UpdateTeacherAsync(Guid teacherId)
            {
            var leaderboardEntries = await m_context.TeacherLeaderboard.ToListAsync();
            var entry = await GetTeacherEntryAsync(teacherId);
            if (entry == null)
                return;

            var index = leaderboardEntries.FindIndex(x => x.TeacherId.Equals(teacherId));
            if (index != -1)
                leaderboardEntries[index] = entry;
            else
                leaderboardEntries.Add(entry);

            SetPositions(leaderboardEntries);
            await RefreshTeacherLeaderboardEntriesAsync(leaderboardEntries);
            }

        public async Task UpdateCourseAsync(Guid courseId)
            {
            var leaderboardEntries = await m_context.CourseLeaderboard.ToListAsync();
            var entry = await GetCourseEntryAsync(courseId);
            if (entry == null)
                return;

            var index = leaderboardEntries.FindIndex(x => x.CourseId.Equals(courseId));
            if (index != -1)
                leaderboardEntries[index] = entry;
            else
                leaderboardEntries.Add(entry);

            SetPositions(leaderboardEntries);
            await RefreshCourseLeaderboardEntriesAsync(leaderboardEntries);
            }

        private static void SetPositions<T>(List<T> entries) where T : LeaderboardEntry
            {
            entries.Sort((left, right) => right.AllTimeScore.CompareTo(left.AllTimeScore));
            for (var i = 0; i < entries.Count; i++)
                entries[i].AllTimePosition = i + 1;

            entries.Sort((left, right) => right.ThisYearScore.CompareTo(left.ThisYearScore));
            for (var i = 0; i < entries.Count; i++)
                entries[i].ThisYearPosition = i + 1;
            }

        private async Task<TeacherLeaderboardEntry> GetTeacherEntryAsync(Guid teacherId)
            {
            var globalRatingCount = await m_teacherAnalyzer.GetTeacherRatingCount(teacherId);
            if (globalRatingCount < m_minimumRatings)
                return null;

            var entry = await m_context.TeacherLeaderboard.FindAsync(teacherId) ??
                        new TeacherLeaderboardEntry
                            {
                            TeacherId = teacherId,
                            };

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

        private async Task<CourseLeaderboardEntry> GetCourseEntryAsync(Guid courseId)
            {
            var globalRatingCount = await m_courseAnalyzer.GetCourseRatingCount(courseId);
            if (globalRatingCount < m_minimumRatings)
                return null;

            var entry = await m_context.CourseLeaderboard.FindAsync(courseId) ??
                        new CourseLeaderboardEntry
                            {
                            CourseId = courseId,
                            };


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

        private async Task RefreshTeacherLeaderboardEntriesAsync(IEnumerable<TeacherLeaderboardEntry> entries)
            {
            foreach (var entry in entries)
                {
                if (m_context.TeacherLeaderboard.Find(entry.TeacherId) == null)
                    m_context.TeacherLeaderboard.Add(entry);
                else m_context.TeacherLeaderboard.Update(entry);
                }
            await m_context.SaveChangesAsync();
            }

        private async Task RefreshCourseLeaderboardEntriesAsync(IEnumerable<CourseLeaderboardEntry> entries)
            {
            foreach (var entry in entries)
                {
                if (m_context.CourseLeaderboard.Find(entry.CourseId) == null)
                    m_context.CourseLeaderboard.Add(entry);
                else m_context.CourseLeaderboard.Update(entry);
                }
            await m_context.SaveChangesAsync();
            }
        }
    }
