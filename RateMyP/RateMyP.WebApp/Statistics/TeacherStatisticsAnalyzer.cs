using System;
using System.Collections.Generic;
using System.IO;
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
        Task<TimeSpan> GetRatingMaxTimeDifference(Guid teacherId);
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

            if (ratings.Count == 0)
                throw new InvalidDataException("Teacher has no ratings.");

            return ratings.Average(rating => rating.OverallMark);
            }

        public async Task<double> GetTeacherAverageLevelOfDifficulty(Guid teacherId)
            {
            var ratings = await m_context.Ratings.Where(r => r.TeacherId.Equals(teacherId)).ToListAsync();

            if (ratings.Count == 0)
                throw new InvalidDataException("Teacher has no ratings.");

            return ratings.Average(rating => rating.LevelOfDifficulty);
            }

        public async Task<double> GetTeacherWouldTakeTeacherAgainRatio(Guid teacherId)
            {
            var ratings = await m_context.Ratings.Where(r => r.TeacherId.Equals(teacherId)).ToListAsync();

            if (ratings.Count == 0)
                throw new InvalidDataException("Teacher has no ratings.");

            return ratings.Average(rating => rating.WouldTakeTeacherAgain ? 1 : 0);
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

            if (ratings.Count == 0)
                throw new InvalidDataException("Teacher has no ratings.");

            return ratings.Average(rating => rating.OverallMark);
            }

        public async Task<List<DateMark>> GetTeacherAverageMarks(Guid teacherId, int timeStampCount = 5)
            {
            if (timeStampCount < 2)
                throw new InvalidDataException("Time stamp count can not be less than 2.");

            var ratings = m_context.Ratings
                                   .Where(r => r.TeacherId.Equals(teacherId))
                                   .OrderBy(o => o.DateCreated)
                                   .AsAsyncEnumerable()
                                   .GetAsyncEnumerator();

            if (!await ratings.MoveNextAsync())
                throw new InvalidDataException("Teacher has no ratings.");

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

        public async Task<int> GetTeacherRatingCount(Guid teacherId, DateTime? minDate = null)
            {
            if (minDate == null)
                minDate = DateTime.MinValue;

            var ratings = await m_context.Ratings.ToListAsync();
            return ratings.Where(r => r.TeacherId.Equals(teacherId) &&
                                         r.DateCreated >= minDate).ToList().Count;
            }

        public async Task<TimeSpan> GetRatingMaxTimeDifference(Guid teacherId)
            {
            var ratings = await m_context.Ratings.Where(x => x.TeacherId.Equals(teacherId)).ToListAsync();

            if (ratings.Count == 0)
                throw new InvalidDataException("Teacher has no ratings.");

            var minDate = ratings.Min(rating => rating.DateCreated);
            var maxDate = ratings.Max(rating => rating.DateCreated);
            return maxDate - minDate;
            }
        }
    }
