using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RateMyP.WebApp.Models;
using RateMyP.WebApp.Statistics;

namespace RateMyP.WebApp.Jobs
    {
    public class LeaderboardUpdate
        {
        private RateMyPDbContext m_context;
        private readonly int m_minimumRatings = Int32.Parse(ConfigurationManager.AppSettings["leaderboardEntryThreshold"]);

        public async void RunFullLeaderboardUpdate()
            {
            var statisticsAnalyzer = new TeacherStatisticsAnalyzer(m_context);

            var teachers = await m_context.Teachers.ToListAsync();
            //var courses = await m_context.Courses.ToListAsync();
            var weightedTeachers = new List<Tuple<double, double, TeacherLeaderboardEntry>>();
            //var weightedCourses = new List<Tuple<double, double, CourseLeaderboardPosition>>();

            foreach (var teacher in teachers)
                {
                var globalRatingCount = await statisticsAnalyzer.GetTeacherRatingCount(teacher.Id, DateTime.MinValue);
                var yearlyRatingCount = await statisticsAnalyzer.GetTeacherRatingCount(teacher.Id, new DateTime(2019, 1, 1));

                if (globalRatingCount >= m_minimumRatings)
                    {
                    var globalAverage = await statisticsAnalyzer.GetTeacherAverageMark(teacher.Id);
                    var globalRange = await statisticsAnalyzer.GetTeacherRatingDateRange(teacher.Id, DateTime.MinValue);
                    var yearlyAverage = await statisticsAnalyzer.GetTeacherAverageMarkInYear(teacher.Id, 2019);
                    var yearlyRange = await statisticsAnalyzer.GetTeacherRatingDateRange(teacher.Id, new DateTime(2019, 1, 1));

                    var teacherPosition = new TeacherLeaderboardEntry
                        {
                        Id = teacher.Id,
                        AllTimePosition = 0,
                        AllTimeRatingCount = globalRatingCount,
                        AllTimeAverage = globalAverage,
                        ThisYearPosition = 0,
                        ThisYearRatingCount = yearlyRatingCount,
                        ThisYearAverage = yearlyAverage,
                        };

                    var globalTeacherScore = ParseScore(globalAverage, globalRatingCount, globalRange);
                    var yearlyTeacherScore = ParseScore(yearlyAverage, yearlyRatingCount, yearlyRange);

                    weightedTeachers.Add(new Tuple<double, double, TeacherLeaderboardEntry>
                                        (globalTeacherScore, yearlyTeacherScore, teacherPosition));
                    }
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

            CreateLeaderboardEntries((weightedTeachers.Select(t => t.Item3).ToList()));

            }

        private double ParseScore(double averageRating, int ratingCount, TimeSpan ratingRange)
            {
            if (ratingRange != TimeSpan.Zero)
                return averageRating * MathF.Log(ratingCount) * MathF.Log10(ratingRange.Hours);
            return averageRating * MathF.Log2(ratingCount);

            }

        private async void CreateLeaderboardEntries(List<TeacherLeaderboardEntry> entries)
            {
            await m_context.TeacherLeaderboardEntries.AddRangeAsync(entries);
            m_context.SaveChanges();
            }

        }
    }
