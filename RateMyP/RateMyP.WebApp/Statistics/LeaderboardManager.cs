using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RateMyP.WebApp.Models;

namespace RateMyP.WebApp.Statistics
    {
    public interface ILeaderboardManager
        {
        Task FullUpdate();
        Task UpdateFromTeacher(Guid teacherId);
        }

    public class LeaderboardManager : ILeaderboardManager
        {
        private readonly RateMyPDbContext m_context;
        private readonly ITeacherStatisticsAnalyzer m_analyzer;
        private readonly int m_minimumRatings = int.Parse(ConfigurationManager.AppSettings["LeaderboardEntryThreshold"]);
        private readonly int m_currentYear = int.Parse(ConfigurationManager.AppSettings["CurrentAcademicYear"]);

        public LeaderboardManager(ITeacherStatisticsAnalyzer analyzer, RateMyPDbContext context)
            {
            m_context = context;
            m_analyzer = analyzer;
            // subscrive to event
            // () => FullUpdate
            }

        public double GetGlobalScore(double averageRating, int ratingCount, TimeSpan ratingRange)
            {
            return averageRating * MathF.Log(ratingCount) * MathF.Log10(ratingRange.Hours + 1);
            }

        public double GetYearlyScore(double averageRating, int ratingCount)
            {
            return averageRating * MathF.Log2(ratingCount);
            }

        public async Task FullUpdate()
            {
            var teachers = await m_context.Teachers.ToListAsync();
            var weightedTeachers = new List<Tuple<double, double, LeaderboardEntry>>();

            foreach (var teacher in teachers)
                {
                var entryWithScores = await GetEntryWithScores(teacher.Id);
                if (entryWithScores != null)
                    weightedTeachers.Add(entryWithScores);
                }

            weightedTeachers.Sort((t1, t2) => t1.Item1.CompareTo(t2.Item1));

            for (var i = 0; i < weightedTeachers.Count; i++)
                {
                var teacherPosition = weightedTeachers[i].Item3;
                teacherPosition.AllTimePosition = i + 1;
                weightedTeachers[i] =
                    Tuple.Create(weightedTeachers[i].Item1, weightedTeachers[i].Item2, teacherPosition);
                }

            weightedTeachers.Sort((t1, t2) => t1.Item2.CompareTo(t2.Item2));

            for (var i = 0; i < weightedTeachers.Count; i++)
                {
                var teacherPosition = weightedTeachers[i].Item3;
                teacherPosition.ThisYearPosition = i + 1;
                weightedTeachers[i] =
                    Tuple.Create(weightedTeachers[i].Item1, weightedTeachers[i].Item2, teacherPosition);
                }

            RefreshLeaderboardEntries(weightedTeachers.Select(t => t.Item3));
            }

        public async Task UpdateFromTeacher(Guid teacherId)
            {
            // This is where only the required entry should be updated and the leaderboard refreshed in some other way other than full update.
            await FullUpdate();
            }

        private async Task<Tuple<double, double, LeaderboardEntry>> GetEntryWithScores(Guid teacherId)
            {
            var globalRatingCount = await m_analyzer.GetTeacherRatingCount(teacherId);
            if (globalRatingCount < m_minimumRatings)
                return null;
            var globalAverage = await m_analyzer.GetTeacherAverageMark(teacherId);
            var globalRange = await GetRatingMaxTimeDifference(teacherId);
            var globalTeacherScore = GetGlobalScore(globalAverage, globalRatingCount, globalRange);

            var yearlyRatingCount = await m_analyzer.GetTeacherRatingCount(teacherId, new DateTime(m_currentYear, 9, 1));
            var yearlyAverage = await m_analyzer.GetTeacherAverageMarkInYear(teacherId, m_currentYear);
            var yearlyTeacherScore = GetYearlyScore(yearlyAverage, yearlyRatingCount);

            var entry = await CreateOrUpdateTeacherLeaderboardEntry(teacherId, globalRatingCount, yearlyRatingCount, globalAverage, yearlyAverage);
            return new Tuple<double, double, LeaderboardEntry>(globalTeacherScore, yearlyTeacherScore, entry);
            }

        private async Task<LeaderboardEntry> CreateOrUpdateTeacherLeaderboardEntry(Guid teacherId, int globalRatingCount, int yearRatingCount, double globalAverage, double yearAverage)
            {
            var entry = await m_context.Leaderboard.FindAsync(teacherId);
            if (entry == null)
                {
                entry = new LeaderboardEntry
                    {
                    Id = teacherId,
                    EntryType = EntryType.Teacher,
                    AllTimePosition = 0,
                    AllTimeRatingCount = globalRatingCount,
                    AllTimeAverage = globalAverage,
                    ThisYearPosition = 0,
                    ThisYearRatingCount = yearRatingCount,
                    ThisYearAverage = yearAverage,
                    };
                return entry;
                }

            entry.AllTimeRatingCount = globalRatingCount;
            entry.AllTimeAverage = globalAverage;
            entry.ThisYearRatingCount = yearRatingCount;
            entry.ThisYearAverage = yearAverage;
            return entry;
            }

        private async Task<TimeSpan> GetRatingMaxTimeDifference(Guid teacherId)
            {
            var ratings = await m_context.Ratings.Where(x => x.TeacherId.Equals(teacherId)).ToListAsync();
            var minDate = ratings.Min(rating => rating.DateCreated);
            var maxDate = ratings.Max(rating => rating.DateCreated);
            return maxDate - minDate;
            }

        public async void RefreshLeaderboardEntries(IEnumerable<LeaderboardEntry> entries)
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
