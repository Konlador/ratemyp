using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace RateMyP.WebApp.Statistics
    {
    public interface ICourseStatisticsAnalyzer
        {
        Task<double> GetCourseAverageMark(Guid courseId);
        Task<double> GetCourseAverageLevelOfDifficulty(Guid courseId);
        Task<double> GetCourseWouldTakeTeacherAgainRatio(Guid courseId);
        Task<int> GetCoursePositiveRatingCount(Guid courseId);
        Task<int> GetCoursePositiveRatingCountInYear(Guid courseId, int year);
        Task<double> GetCourseAverageMarkInYear(Guid courseId, int year);
        Task<List<DateMark>> GetCourseAverageMarks(Guid courseId, int timeStampCount = 5);
        Task<int> GetCourseRatingCount(Guid courseId, DateTime? date = null);
        Task<TimeSpan> GetRatingMaxTimeDifference(Guid courseId);
        }

    public class CourseStatisticsAnalyzer : ICourseStatisticsAnalyzer
        {
        private readonly RateMyPDbContext m_context;

        public CourseStatisticsAnalyzer(RateMyPDbContext context)
            {
            m_context = context;
            }

        public async Task<double> GetCourseAverageMark(Guid courseId)
            {
            var ratings = await m_context.Ratings.Where(r => r.CourseId.Equals(courseId)).ToListAsync();

            if (ratings.Count == 0)
                throw new InvalidDataException("Course has no ratings.");

            return ratings.Average(rating => rating.OverallMark);
            }

        public async Task<int> GetCoursePositiveRatingCount(Guid courseId)
            {
            var ratings = await m_context.Ratings.Where(r => r.CourseId.Equals(courseId) &&
                                                             r.OverallMark >= 3)
                                                 .ToListAsync();
            return ratings.Count;
            }

        public async Task<int> GetCoursePositiveRatingCountInYear(Guid courseId, int year)
            {
            var startDate = new DateTime(year, 9, 1);
            var endDate = new DateTime(year + 1, 9, 1);
            var ratings = await m_context.Ratings
                                                .Where(r => r.CourseId.Equals(courseId) &&
                                                            r.DateCreated >= startDate &&
                                                            r.DateCreated < endDate &&
                                                            r.OverallMark >= 3)
                                                .ToListAsync();
            return ratings.Count;
            }

        public async Task<double> GetCourseAverageLevelOfDifficulty(Guid courseId)
            {
            var ratings = await m_context.Ratings.Where(r => r.CourseId.Equals(courseId)).ToListAsync();

            if (ratings.Count == 0)
                throw new InvalidDataException("Course has no ratings.");

            return ratings.Average(rating => rating.LevelOfDifficulty);
            }

        public async Task<double> GetCourseWouldTakeTeacherAgainRatio(Guid courseId)
            {
            var ratings = await m_context.Ratings.Where(r => r.CourseId.Equals(courseId)).ToListAsync();

            if (ratings.Count == 0)
                throw new InvalidDataException("Course has no ratings.");

            return ratings.Average(rating => rating.WouldTakeTeacherAgain ? 1 : 0);
            }

        public async Task<double> GetCourseAverageMarkInYear(Guid courseId, int year)
            {
            var startDate = new DateTime(year, 9, 1);
            var endDate = new DateTime(year + 1, 9, 1);
            var ratings = await m_context.Ratings
                                         .Where(r => r.CourseId.Equals(courseId) &&
                                                     r.DateCreated >= startDate &&
                                                     r.DateCreated < endDate)
                                         .ToListAsync();

            if (ratings.Count == 0)
                throw new InvalidDataException("Course has no ratings.");

            return ratings.Average(rating => rating.OverallMark);
            }

        public async Task<List<DateMark>> GetCourseAverageMarks(Guid courseId, int timeStampCount = 5)
            {
            if (timeStampCount < 2)
                throw new InvalidDataException("Time stamp count can not be less than 2.");

            var ratings = m_context.Ratings
                                   .Where(r => r.CourseId.Equals(courseId))
                                   .OrderBy(o => o.DateCreated)
                                   .AsAsyncEnumerable()
                                   .GetAsyncEnumerator();

            if (!await ratings.MoveNextAsync())
                throw new InvalidDataException("Teacher has no ratings.");

            var firstDate = ratings.Current.DateCreated;
            var lastDate = DateTime.Now;

            var timeStamps = TeacherStatisticsAnalyzer.GetTimeStamps(firstDate, lastDate, timeStampCount);

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

        public async Task<int> GetCourseRatingCount(Guid courseId, DateTime? minDate = null)
            {
            if (minDate == null)
                minDate = DateTime.MinValue;

            var ratings = await m_context.Ratings.ToListAsync();
            return ratings.Where(r => r.CourseId.Equals(courseId) &&
                                         r.DateCreated >= minDate).ToList().Count;
            }

        public async Task<TimeSpan> GetRatingMaxTimeDifference(Guid courseId)
            {
            var ratings = await m_context.Ratings.Where(x => x.CourseId.Equals(courseId)).ToListAsync();

            if (ratings.Count == 0)
                throw new InvalidDataException("Teacher has no ratings.");

            var minDate = ratings.Min(rating => rating.DateCreated);
            var maxDate = ratings.Max(rating => rating.DateCreated);
            return maxDate - minDate;
            }
        }
    }
