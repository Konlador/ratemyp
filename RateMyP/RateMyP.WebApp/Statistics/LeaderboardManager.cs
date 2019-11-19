using Microsoft.EntityFrameworkCore;
using RateMyP.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Threading.Tasks;

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
            }

        public double GetGlobalScore(double averageRating, int ratingCount, TimeSpan ratingRange)
            {
            return averageRating; // * MathF.Log10(ratingCount + 1) * MathF.Log10(ratingRange.Hours + 1);
            }

        public double GetYearlyScore(double averageRating, int ratingCount)
            {
            return averageRating; // * MathF.Log10(ratingCount + 1);
            }

        public async Task FullUpdate()
            {
            var teachers = await m_context.Teachers.ToListAsync();
            var leaderboardEntries = new List<LeaderboardEntry>();

            foreach (var teacher in teachers)
                {
                var entry = await GetEntry(teacher.Id);
                if (entry != null)
                    leaderboardEntries.Add(entry);
                }

            SetPositions(leaderboardEntries);
            await RefreshLeaderboardEntries(leaderboardEntries);
            }

        public async Task UpdateFromTeacher(Guid teacherId)
            {
            var leaderboardEntries = await m_context.Leaderboard.ToListAsync();
            var entry = await GetEntry(teacherId);
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

        private static void SetPositions(List<LeaderboardEntry> entries)
            {
            entries.Sort((left, right) => right.AllTimeScore.CompareTo(left.AllTimeScore));
            for (var i = 0; i < entries.Count; i++)
                entries[i].AllTimePosition = i + 1;

            entries.Sort((left, right) => right.ThisYearScore.CompareTo(left.ThisYearScore));
            for (var i = 0; i < entries.Count; i++)
                entries[i].ThisYearPosition = i + 1;
            }

        private async Task<LeaderboardEntry> GetEntry(Guid teacherId)
            {
            var globalRatingCount = await m_analyzer.GetTeacherRatingCount(teacherId);
            if (globalRatingCount < m_minimumRatings)
                return null;

            var entry = await m_context.Leaderboard.FindAsync(teacherId) ??
                        new LeaderboardEntry
                            {
                            Id = teacherId,
                            EntryType = EntryType.Teacher
                            };

            entry.AllTimeRatingCount = globalRatingCount;
            try
                {
                entry.AllTimeAverage = await m_analyzer.GetTeacherAverageMark(teacherId);
                }
            catch (InvalidDataException e)
                {
                if (e.Message.Equals("Teacher has no ratings."))
                    entry.AllTimeAverage = 0;
                }

            entry.ThisYearRatingCount = await m_analyzer.GetTeacherRatingCount(teacherId, new DateTime(m_currentYear, 9, 1));
            try
                {
                entry.ThisYearAverage = await m_analyzer.GetTeacherAverageMarkInYear(teacherId, m_currentYear);
                }
            catch (InvalidDataException e)
                {
                if (e.Message.Equals("Teacher has no ratings."))
                    entry.ThisYearAverage = 0;
                }

            var globalRange = await m_analyzer.GetRatingMaxTimeDifference(teacherId);
            entry.AllTimeScore = GetGlobalScore(entry.AllTimeAverage, entry.AllTimeRatingCount, globalRange);
            entry.ThisYearScore = GetYearlyScore(entry.ThisYearAverage, entry.ThisYearRatingCount);
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
