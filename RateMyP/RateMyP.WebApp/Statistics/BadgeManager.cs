using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RateMyP.WebApp.Models;

namespace RateMyP.WebApp.Statistics
    {
    public interface IBadgeManager
        {
        Task AwardBadgesToTopThreeTeachers();
        Task AwardBadgesToTopTenTeachers();
        Task AwardBadgesToTopTwentyTeachers();
        Task AwardBadgesByRatingAverages();

        };

    public class BadgeManager : IBadgeManager
        {
        private readonly RateMyPDbContext m_context;
        private readonly ITeacherStatisticsAnalyzer m_analyzer;
        private readonly int m_currentYear = int.Parse(ConfigurationManager.AppSettings["CurrentAcademicYear"]);

        public BadgeManager(RateMyPDbContext context, ITeacherStatisticsAnalyzer analyzer)
            {
            m_context = context;
            m_analyzer = analyzer;
            }

        public async Task FullUpdateAsync()
            {
            await AwardBadgesToTopThreeTeachers();
            await AwardBadgesToTopTenTeachers();
            await AwardBadgesToTopTwentyTeachers();
            await AwardBadgesByRatingAverages();
            }

        private async Task AwardBadges(List<Teacher> teachers, string badgeName)
            {
            var badge = await m_context.Badges.FindAsync(Guid.Parse(ConfigurationManager.AppSettings[badgeName]));

            foreach (var teacher in teachers)
                {
                var award = new TeacherBadge()
                    {
                    BadgeId = badge.Id,
                    DateObtained = DateTime.Now,
                    Id = new Guid(),
                    TeacherId = teacher.Id,
                    };

                m_context.TeacherBadges.Add(award);
                }

            m_context.SaveChanges();
            }

        public async Task AwardBadgesToTopThreeTeachers()
            {
            var entries = await m_context.TeacherLeaderboard
                                .Include(x => x.Teacher)
                                .OrderBy(x => x.AllTimePosition)
                                .Take(3)
                            .ToListAsync();

            var teachers = new List<Teacher>();

            foreach (var entry in entries)
                {
                var teacher = await m_context.Teachers.FindAsync(entry.TeacherId);
                teachers.Add(teacher);
                }

            await AwardBadges(teachers.Take(1).ToList(), "Badge.TheVeryBest");
            await AwardBadges(teachers.Skip(1).Take(1).ToList(), "Badge.RunnerUp");
            await AwardBadges(teachers.Skip(2).Take(1).ToList(), "Badge.StrongContender");
            }

        public async Task AwardBadgesToTopTenTeachers()
            {
            var entries = await m_context.TeacherLeaderboard
                                .Include(x => x.Teacher)
                                .OrderBy(x => x.AllTimePosition)
                                .Skip(3)
                                .Take(7)
                            .ToListAsync();
            var teachers = new List<Teacher>();

            foreach (var entry in entries)
                {
                var teacher = await m_context.Teachers.FindAsync(entry.TeacherId);
                teachers.Add(teacher);
                }

            await AwardBadges(teachers, "Badge.TheBest");
            }

        public async Task AwardBadgesToTopTwentyTeachers()
            {
            var entries = await m_context.TeacherLeaderboard
                                .Include(x => x.Teacher)
                                .OrderBy(x => x.AllTimePosition)
                                .Skip(10)
                                .Take(10)
                            .ToListAsync();
            var teachers = new List<Teacher>();

            foreach (var entry in entries)
                {
                var teacher = await m_context.Teachers.FindAsync(entry.TeacherId);
                teachers.Add(teacher);
                }

            await AwardBadges(teachers, "Badge.Beloved");
            }

        public async Task AwardBadgesByRatingAverages()
            {
            var teachers = await m_context.Teachers.ToListAsync();
            foreach (var teacher in teachers)
                {
                if (await m_analyzer.GetIfTeacherHasRatings(teacher.Id))
                    {
                    var target = new List<Teacher> { teacher };

                    if (await m_analyzer.GetTeacherAverageMarkInYear(teacher.Id, m_currentYear) >= 4.5)
                        {
                        await AwardBadges(target, "Badge.FullOfStars");
                        }

                    if (await m_analyzer.GetTeacherAverageLevelOfDifficulty(teacher.Id) >= 4.5)
                        {
                        await AwardBadges(target, "Badge.Hardmode");
                        }

                    if (await m_analyzer.GetTeacherWouldTakeTeacherAgainRatio(teacher.Id) >= 0.9f)
                        {
                        await AwardBadges(target, "Badge.Leader");
                        }
                    }
                }
            }
        }
    }
