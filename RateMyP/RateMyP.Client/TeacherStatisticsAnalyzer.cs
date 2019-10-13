using RateMyP.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RateMyP
    {
    public class TeacherStatisticsAnalyzer
        {
        public async Task<double> GetTeacherAverageMark(Guid teacherId)
            {
            var allRatings = await RateMyPClient.Client.Ratings.GetAll();
            var teacherRatings = allRatings.Where(r => r.Teacher.Id.ToString() == teacherId.ToString()).ToList();
            double sum = 0;
            foreach (var rating in teacherRatings)
                sum += rating.OverallMark;
            return allRatings.Count > 0 ? sum / allRatings.Count : 0;
            }

        public async Task<double> GetTeacherAverageMark(Guid teacherId, DateTime startDate, DateTime endDate)
            {
            var allRatings = await RateMyPClient.Client.Ratings.GetAll();
            var teacherRatings = allRatings.Where(r => r.Teacher.Id.ToString() == teacherId.ToString() && r.DateCreated >= startDate && r.DateCreated <= endDate).ToList();
            double sum = 0;
            foreach (var rating in allRatings)
                sum += rating.OverallMark;
            return allRatings.Count > 0 ? sum / allRatings.Count : 0;
            }

        public async Task<List<double>> GetTeacherAverageMarkList(Guid teacherId, DateTime startDate, DateTime endDate, int parts)
            {
            var teachersAverageMarks = new List<double>();

            var difference = endDate - startDate;
            var singlePartInterval = TimeSpan.FromTicks(difference.Ticks / parts);

            var start = startDate;
            var end = startDate + singlePartInterval;

            for (var i = 0; i < parts; i++)
                {
                teachersAverageMarks.Add(await GetTeacherAverageMark(teacherId, start, end));

                start += singlePartInterval;
                end += singlePartInterval;
                }
            return teachersAverageMarks;
            }

        public async Task<double> GetTeachersAverageLevelOfDifficultyRating(Guid teacherId)
            {
            var allRatings = await RateMyPClient.Client.Ratings.GetAll();
            var ratings = allRatings.Where(r => r.Teacher.Id.ToString() == teacherId.ToString());
            double sum = 0;
            foreach (var rating in ratings)
                sum += rating.LevelOfDifficulty;
            return allRatings.Count > 0 ? sum / allRatings.Count : 0;
            }

        public async Task<double> GetTeachersWouldTakeTeacherAgainRation(Guid teacherId)
            {
            var allRatings = await RateMyPClient.Client.Ratings.GetAll();
            var ratings = allRatings.Where(r => r.Teacher.Id.Equals(teacherId));
            double wouldTakeCount = 0;

            foreach (var rating in ratings)
                if (rating.WouldTakeTeacherAgain) wouldTakeCount++;

            return allRatings.Count > 0 ? wouldTakeCount / allRatings.Count : 0;
            }
        }
    }
