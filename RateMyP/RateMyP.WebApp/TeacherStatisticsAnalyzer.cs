using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RateMyP.WebApp.Models;
using RateMyP.WebApp.Statistics;

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

        public async Task<List<DateMark>> GetTeacherAverageMarks(Guid teacherId, int timeStampCount)
            {
            var teachersAverageMarks = new List<DateMark>();
            var ratings = await m_context.Ratings.Where(r => r.TeacherId.Equals(teacherId)).OrderBy(o => o.DateCreated).ToListAsync();

            if (ratings.Count == 0 || timeStampCount <= 1)
                return teachersAverageMarks;

            var timeStamps = new List<DateTime>();

            var startDate = ratings.First().DateCreated;
            var endDate = DateTime.Now;

            var globalDifference = endDate - startDate;
            var partDifference = new TimeSpan(globalDifference.Ticks / (timeStampCount - 1));

            var currentDate = startDate;

            while (currentDate <= endDate)
                {
                timeStamps.Add(currentDate);
                currentDate += partDifference;
                }

            foreach (var timeStamp in timeStamps)
                {
                var ratingsBeforeStamp = ratings.Where(r => r.DateCreated <= timeStamp).ToList();
                var averageMark = (double)ratingsBeforeStamp.Sum(r => r.OverallMark) / ratingsBeforeStamp.Count;
                teachersAverageMarks.Add(new DateMark
                    {
                    Date = timeStamp,
                    Mark = averageMark
                    });
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

        private static DateTime TruncateToMinutes(DateTime dt)
            {
            return dt.Date.AddMinutes((int)dt.TimeOfDay.TotalMinutes);
            }
        }
    }
