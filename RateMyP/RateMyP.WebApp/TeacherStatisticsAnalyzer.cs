using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RateMyP.WebApp.Models;

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
            var teacherRatings = allRatings.Where(r => r.TeacherId.Equals(teacherId) && TruncateToMinutes(r.DateCreated) >= TruncateToMinutes(startDate) && TruncateToMinutes(r.DateCreated) <= TruncateToMinutes(endDate)).ToList();
            double sum = 0;
            foreach (var rating in teacherRatings)
                sum += rating.OverallMark;
            return teacherRatings.Count > 0 ? sum / teacherRatings.Count : 0;
            }

        public async Task<List<DateMark>> GetTeacherAverageMarkList(Guid teacherId, int parts)
            {
            var teachersAverageMarks = new List<DateMark>();
            var allRatings = await m_context.Ratings.ToListAsync();
            var teacherRatings = allRatings.Where(r => r.TeacherId.Equals(teacherId)).ToList();
            var orderedTeacherRatings = teacherRatings.OrderBy(o => o.DateCreated).ToList();

            var startDate = orderedTeacherRatings.FirstOrDefault().DateCreated;
            var endDate = orderedTeacherRatings.Last().DateCreated;

            var difference = endDate - startDate;
            var singlePartInterval = TimeSpan.FromTicks(difference.Ticks / parts);

            var start = startDate - singlePartInterval;
            var end = startDate;

            while (!(DateTime.Compare(TruncateToMinutes(end), TruncateToMinutes(endDate)) == 0))
                {
                start += singlePartInterval;
                end += singlePartInterval;
                teachersAverageMarks.Add(new DateMark()
                    {
                    Date = start,
                    Mark = await GetTeacherAverageMark(teacherId, start, end)
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
        static DateTime TruncateToMinutes(DateTime dt)
            {
            return dt.Date.AddMinutes((int)dt.TimeOfDay.TotalMinutes);
            }
        }
    }
