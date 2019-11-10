using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace RateMyP.WebApp
    {
    public class TeacherStatisticsAnalyzer
    {
        private RateMyPDbContext m_context;

        public TeacherStatisticsAnalyzer(RateMyPDbContext context)
            {
            m_context = context;
            }

        public async Task<double> GetTeacherAverageMark(Guid teacherId)
            {
            var allRatings = await m_context.Ratings.ToListAsync();
            var teacherRatings = allRatings.Where(r => r.TeacherId.Equals(teacherId)).ToList();
            double sum = 0;
            foreach (var rating in teacherRatings)
                sum += rating.OverallMark;
            return teacherRatings.Count > 0 ? sum / teacherRatings.Count : 0;
            }

        public async Task<double> GetTeacherAverageMark(Guid teacherId, DateTime startDate, DateTime endDate)
            {
            var allRatings = await m_context.Ratings.ToListAsync();
            var teacherRatings = allRatings.Where(r => r.TeacherId.Equals(teacherId) && r.DateCreated >= startDate && r.DateCreated <= endDate).ToList();
            double sum = 0;
            foreach (var rating in teacherRatings)
                sum += rating.OverallMark;
            return teacherRatings.Count > 0 ? sum / teacherRatings.Count : 0;
            }

        public async Task<List<Tuple<DateTime, double>>> GetTeacherAverageMarkList(Guid teacherId, DateTime startDate, DateTime endDate, int parts)
            {
            var teachersAverageMarks = new List<Tuple<DateTime, double>>();

            var difference = endDate - startDate;
            var singlePartInterval = TimeSpan.FromTicks(difference.Ticks / parts);

            var start = startDate - singlePartInterval;
            var end = startDate;

            for (var i = 0; i < parts; i++)
                {
                start += singlePartInterval;
                end += singlePartInterval;
                teachersAverageMarks.Add(Tuple.Create(start, await GetTeacherAverageMark(teacherId, start, end)));
                }
            return teachersAverageMarks;
            }

        public async Task<double> GetTeachersAverageLevelOfDifficultyRating(Guid teacherId)
            {
            var allRatings = await m_context.Ratings.ToListAsync();
            var teacherRatings = allRatings.Where(r => r.TeacherId.Equals(teacherId)).ToList();
            double sum = 0;
            foreach (var rating in teacherRatings)
                sum += rating.LevelOfDifficulty;
            return teacherRatings.Count > 0 ? sum / teacherRatings.Count : 0;
            }

        public async Task<double> GetTeachersWouldTakeTeacherAgainRatio(Guid teacherId)
            {
            var allRatings = await m_context.Ratings.ToListAsync();
            var teacherRatings = allRatings.Where(r => r.TeacherId.Equals(teacherId)).ToList();
            double wouldTakeCount = 0;

            foreach (var rating in teacherRatings)
                if (rating.WouldTakeTeacherAgain) wouldTakeCount++;

            return teacherRatings.Count > 0 ? wouldTakeCount / teacherRatings.Count : 0;
            }
        }
    }
