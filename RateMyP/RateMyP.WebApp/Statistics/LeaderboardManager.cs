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
        void RunFullLeaderboardUpdate();
        void UpdateSingleTeacherEntry(Guid teacherId);
        }

    public enum StatType
        {
        GlobalRatingAverage,
        YearRatingAverage,
        GlobalRatingCount,
        YearRatingCount,
        GlobalRatingRange,
        YearRatingRange
        }

    public class LeaderboardManager : ILeaderboardManager
        {
        private RateMyPDbContext m_context;
        private readonly ITeacherStatisticsAnalyzer m_analyzer;
        private readonly int m_minimumRatings = Int32.Parse(ConfigurationManager.AppSettings["leaderboardEntryThreshold"]);
        private readonly int m_currentYear = Int32.Parse(ConfigurationManager.AppSettings["currentAcademicYear"]);

        public LeaderboardManager(ITeacherStatisticsAnalyzer analyzer)
            {
            m_context = new RateMyPDbContext();
            m_analyzer = analyzer;
            }

        public async void RunFullLeaderboardUpdate()
            {
            var teachers = await m_context.Teachers.ToListAsync();
            var weightedTeachers = new List<Tuple<double, double, TeacherLeaderboardEntry>>();

            foreach (var teacher in teachers)
                {
                var entry = await RecalculateTeacherEntryData(teacher.Id);
                if (entry != null) weightedTeachers.Add(entry);
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

            RefreshLeaderboardEntries((weightedTeachers.Select(t => t.Item3).ToList()));
            }

        private async Task<Tuple<double, double, TeacherLeaderboardEntry>> RecalculateTeacherEntryData(Guid teacherId)
            {
            var globalRatingCount = await GetTeacherRatingCount(teacherId, StatType.GlobalRatingCount);

            if (globalRatingCount >= m_minimumRatings)
                {
                var yearlyRatingCount = await GetTeacherRatingCount(teacherId, StatType.YearRatingCount);
                var globalAverage = await GetTeacherRatingAverage(teacherId, StatType.GlobalRatingAverage);
                var globalRange = await GetTeacherRatingRange(teacherId, StatType.GlobalRatingRange);
                var yearlyAverage = await GetTeacherRatingAverage(teacherId, StatType.YearRatingAverage);
                var yearlyRange = await GetTeacherRatingRange(teacherId, StatType.YearRatingRange);

                var entry = await CreateOrUpdateTeacherLeaderboardEntry(teacherId, globalRatingCount,
                    yearlyRatingCount, globalAverage, yearlyAverage);

                var globalTeacherScore = ParseScore(globalAverage, globalRatingCount, globalRange);
                var yearlyTeacherScore = ParseScore(yearlyAverage, yearlyRatingCount, yearlyRange);

                return new Tuple<double, double, TeacherLeaderboardEntry>(globalTeacherScore, yearlyTeacherScore, entry);
                }

            return null;
            }

        private async Task<TeacherLeaderboardEntry> CreateOrUpdateTeacherLeaderboardEntry(Guid teacherId, int globalRatingCount, int yearRatingCount,
                                                                                                          double globalAverage, double yearAverage)
            {
            var entry = await m_context.TeacherLeaderboardEntries.FindAsync(teacherId);
            if (entry == null)
                {
                entry = new TeacherLeaderboardEntry
                    {
                    Id = teacherId,
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

        private async Task<int> GetTeacherRatingCount(Guid teacherId, StatType type)
            {
            return type switch
                {
                StatType.GlobalRatingCount => await m_analyzer.GetTeacherRatingCount(teacherId, DateTime.MinValue),
                StatType.YearRatingCount => await m_analyzer.GetTeacherRatingCount(teacherId, new DateTime(m_currentYear, 9, 1)),
                _ => throw new ArgumentException(),
                };
            }

        private async Task<double> GetTeacherRatingAverage(Guid teacherId, StatType type)
            {
            return type switch
                {
                StatType.GlobalRatingAverage => await m_analyzer.GetTeacherAverageMark(teacherId),
                StatType.YearRatingAverage => await m_analyzer.GetTeacherAverageMarkInYear(teacherId, m_currentYear),
                _ => throw new ArgumentException(),
                };
            }

        private async Task<TimeSpan> GetTeacherRatingRange(Guid teacherId, StatType type)
            {
            return type switch
                {
                StatType.GlobalRatingRange => await m_analyzer.GetTeacherRatingDateRange(teacherId, DateTime.MinValue),
                StatType.YearRatingRange => await m_analyzer.GetTeacherRatingDateRange(teacherId, new DateTime(2019, 9, 1)),
                _ => throw new ArgumentException(),
                };
            }

        public void UpdateSingleTeacherEntry(Guid teacherId)
            {
            // This is where only the required entry should be updated and the leaderboard refreshed in some other way other than full update.
            RunFullLeaderboardUpdate();
            }

        public double ParseScore(double averageRating, int ratingCount, TimeSpan ratingRange)
            {
            if (ratingRange != TimeSpan.Zero)
                return averageRating * MathF.Log(ratingCount) * MathF.Log10(ratingRange.Hours);
            return averageRating * MathF.Log2(ratingCount);
            }

        public async void RefreshLeaderboardEntries(List<TeacherLeaderboardEntry> entries)
            {
            foreach (var entry in entries)
                {
                if (m_context.TeacherLeaderboardEntries.Find(entry.Id) == null)
                    {
                    m_context.TeacherLeaderboardEntries.Add(entry);
                    }
                else m_context.TeacherLeaderboardEntries.Update(entry);
                }
            await m_context.SaveChangesAsync();
            }

        }
    }
