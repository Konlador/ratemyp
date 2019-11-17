﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace RateMyP.WebApp.Statistics
    {
    public interface ITeacherStatisticsAnalyzer
        {
        Task<double> GetTeacherAverageMark(Guid teacherId);
        Task<double> GetTeacherAverageLevelOfDifficulty(Guid teacherId);
        Task<double> GetTeacherWouldTakeTeacherAgainRatio(Guid teacherId);
        Task<double> GetTeacherAverageMarkInYear(Guid teacherId, int year);
        Task<List<DateMark>> GetTeacherAverageMarks(Guid teacherId, int timeStampCount = 5);
        Task<int> GetTeacherRatingCount(Guid teacherId, DateTime? date = null);
        }

    public class TeacherStatisticsAnalyzer : ITeacherStatisticsAnalyzer
        {
        private readonly RateMyPDbContext m_context;

        public TeacherStatisticsAnalyzer(RateMyPDbContext context)
            {
            m_context = context;
            }

        public async Task<double> GetTeacherAverageMark(Guid teacherId)
            {
            var ratings = await m_context.Ratings.Where(r => r.TeacherId.Equals(teacherId)).ToListAsync();
            return ratings.Count > 0 ? ratings.Average(rating => rating.OverallMark) : 0;
            }

        public async Task<double> GetTeacherAverageLevelOfDifficulty(Guid teacherId)
            {
            var ratings = await m_context.Ratings.Where(r => r.TeacherId.Equals(teacherId)).ToListAsync();
            return ratings.Count > 0 ? ratings.Average(rating => rating.LevelOfDifficulty) : 0;
            }

        public async Task<double> GetTeacherWouldTakeTeacherAgainRatio(Guid teacherId)
            {
            var ratings = await m_context.Ratings.Where(r => r.TeacherId.Equals(teacherId)).ToListAsync();
            return ratings.Count > 0 ? ratings.Average(rating => rating.WouldTakeTeacherAgain ? 1 : 0) : 0;
            }

        public async Task<double> GetTeacherAverageMarkInYear(Guid teacherId, int year)
            {
            var startDate = new DateTime(year, 9, 1);
            var endDate = new DateTime(year + 1, 9, 1);
            var ratings = await m_context.Ratings
                                         .Where(r => r.TeacherId.Equals(teacherId) &&
                                                     r.DateCreated >= startDate &&
                                                     r.DateCreated < endDate)
                                         .ToListAsync();
            return ratings.Count > 0 ? ratings.Average(rating => rating.OverallMark) : 0;
            }

        public async Task<List<DateMark>> GetTeacherAverageMarks(Guid teacherId, int timeStampCount = 5)
            {
            if (timeStampCount <= 1)
                return null;

            var ratings = m_context.Ratings
                                   .Where(r => r.TeacherId.Equals(teacherId))
                                   .OrderBy(o => o.DateCreated)
                                   .AsAsyncEnumerable()
                                   .GetAsyncEnumerator();

            if (!await ratings.MoveNextAsync())
                return null;

            var firstDate = ratings.Current.DateCreated;
            var lastDate = DateTime.Now;

            var timeStamps = GetTimeStamps(firstDate, lastDate, timeStampCount);

            var sum = 0;
            var count = 0;
            var averageMarks = new List<DateMark>();
            foreach (var timeStamp in timeStamps)
                {
                while (ratings.Current != null && ratings.Current.DateCreated <= timeStamp)
                    {
                    sum += ratings.Current.OverallMark;
                    count++;
                    await ratings.MoveNextAsync();
                    }
                averageMarks.Add(new DateMark { Date = timeStamp, Mark = (double)sum / count });
                }

            await ratings.DisposeAsync();
            return averageMarks;
            }

        private static IEnumerable<DateTime> GetTimeStamps(DateTime fromDate, DateTime toDate, int timeStampCount)
            {
            var bigInterval = toDate - fromDate;
            var smallInterval = new TimeSpan(bigInterval.Ticks / (timeStampCount - 1));
            var timeStamps = new List<DateTime>();

            for (var currentTime = fromDate; currentTime < toDate; currentTime += smallInterval)
                timeStamps.Add(currentTime);

            return timeStamps;
            }

        public async Task<int> GetTeacherRatingCount(Guid teacherId, DateTime? date = null)
            {
            if (date == null)
                date = DateTime.MinValue;

            var ratings = await m_context.Ratings.ToListAsync();
            return ratings.Where(r => r.TeacherId.Equals(teacherId) &&
                                         r.DateCreated > date).ToList().Count;
            }

        public async Task<TimeSpan> GetTeacherRatingDateRange(Guid teacherId, DateTime date)
            {
            var allRatings = await m_context.Ratings.ToListAsync();
            var teacherRatings = allRatings.Where(r => r.TeacherId.Equals(teacherId) &&
                                                       r.DateCreated > date).ToList();
            return teacherRatings.Max(r => r.DateCreated) - teacherRatings.Min(r => r.DateCreated);
            }
        }
    }
