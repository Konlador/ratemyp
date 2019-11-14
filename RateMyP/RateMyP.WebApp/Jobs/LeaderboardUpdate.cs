using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RateMyP.WebApp.Models;

namespace RateMyP.WebApp.Jobs
    {
    public class LeaderboardUpdate
        {
        private RateMyPDbContext m_context;

        public async void RunFullLeaderboardUpdate()
            {
            var teachers = await m_context.Teachers.ToListAsync();
            var courses = await m_context.Courses.ToListAsync();
            var statisticsAnalyzer = new TeacherStatisticsAnalyzer(m_context);
            var weightedTeachers = new List<Tuple<double, TeacherLeaderboardPosition>>();
            var weightedCourses = new List<Tuple<double, CourseLeaderboardPosition>>();
            int i = 0;

            foreach (var teacher in teachers)
                {
                var globalRatingCount = await RetrieveTeacherRatingCount(teacher.Id);
                if (globalRatingCount >= 10)
                    {
                    var globalAverage = await statisticsAnalyzer.GetTeacherAverageMark(teacher.Id);
                    var globalRange = await RetrieveTeacherRatingDateRange(teacher.Id);
                    var teacherPosition = new TeacherLeaderboardPosition
                        {
                        AllTimeAverage = globalAverage,
                        AllTimePosition = 0,
                        AllTimeRatingCount = globalRatingCount,
                        Id = teacher.Id,
                        ThisYearAverage = 0.0d,
                        ThisYearPosition = 0,
                        ThisYearRatingCount = 0,
                        };
                    var teacherScore = ParseScore(globalAverage, globalRatingCount, globalRange);
                    weightedTeachers.Add(new Tuple<double, TeacherLeaderboardPosition>(teacherScore, teacherPosition));

                    }
                }

            foreach (var course in courses)
                {
                var courseCount = await RetrieveCourseRatingCount(course.Id);
                if (courseCount >= 10)
                    {

                    }
                }
            }

        private double ParseScore(double averageRating, int ratingCount, TimeSpan ratingRange)
            {
            return averageRating * MathF.Log2(ratingCount) * MathF.Log10(ratingRange.Hours);
            }

        public async Task<int> RetrieveTeacherRatingCount(Guid teacherId)
            {
            var allRatings = await m_context.Ratings.ToListAsync();
            return allRatings.Where(r => r.TeacherId.Equals(teacherId)).ToList().Count;
            }

        public async Task<int> RetrieveCourseRatingCount(Guid courseId)
            {
            var allRatings = await m_context.Ratings.ToListAsync();
            return allRatings.Where(r => r.CourseId.Equals(courseId)).ToList().Count;
            }

        public async Task<TimeSpan> RetrieveTeacherRatingDateRange(Guid teacherId)
            {
            var allRatings = await m_context.Ratings.ToListAsync();
            var teacherRatings = allRatings.Where(r => r.TeacherId.Equals(teacherId))
                                           .OrderBy(r => r.DateCreated).ToList();
            return teacherRatings[^0].DateCreated - teacherRatings.First().DateCreated;
            }

        public async Task<TimeSpan> RetrieveCourseRatingDateRange(Guid courseId)
            {
            var allRatings = await m_context.Ratings.ToListAsync();
            var courseRatings = allRatings.Where(r => r.CourseId.Equals(courseId))
                                          .OrderBy(r => r.DateCreated).ToList();
            return courseRatings[^0].DateCreated - courseRatings.First().DateCreated;
            }
        }
    }
