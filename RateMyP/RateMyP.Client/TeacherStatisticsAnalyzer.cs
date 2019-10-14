using RateMyP.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RateMyP
    {
    public class TeacherStatisticsAnalyzer
        {
        private IRateMyPClient m_client;

        public TeacherStatisticsAnalyzer(IRateMyPClient client)
            {
            m_client = client;
            }

        public async Task<double> GetTeacherAverageMark(Guid teacherId)
            {
            var allRatings = await m_client.Ratings.GetAll();
            var teacherRatings = allRatings.Where(r => r.Teacher.Id.Equals(teacherId)).ToList();
            double sum = 0;
            foreach (var rating in teacherRatings)
                sum += rating.OverallMark;
            return teacherRatings.Count > 0 ? sum / teacherRatings.Count : 0;
            }

        public async Task<double> GetTeacherAverageMark(Guid teacherId, DateTime startDate, DateTime endDate)
            {
            var allRatings = await m_client.Ratings.GetAll();
            var teacherRatings = allRatings.Where(r => r.Teacher.Id.Equals(teacherId) && r.DateCreated >= startDate && r.DateCreated <= endDate).ToList();
            double sum = 0;
            foreach (var rating in teacherRatings)
                sum += rating.OverallMark;
            return teacherRatings.Count > 0 ? sum / teacherRatings.Count : 0;
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
            var allRatings = await m_client.Ratings.GetAll();
            var teacherRatings = allRatings.Where(r => r.Teacher.Id.Equals(teacherId)).ToList();
            double sum = 0;
            foreach (var rating in teacherRatings)
                sum += rating.LevelOfDifficulty;
            return teacherRatings.Count > 0 ? sum / teacherRatings.Count : 0;
            }

        public async Task<double> GetTeachersWouldTakeTeacherAgainRatio(Guid teacherId)
            {
            var allRatings = await m_client.Ratings.GetAll();
            var teacherRatings = allRatings.Where(r => r.Teacher.Id.Equals(teacherId)).ToList();
            double wouldTakeCount = 0;

            foreach (var rating in teacherRatings)
                if (rating.WouldTakeTeacherAgain) wouldTakeCount++;

            return teacherRatings.Count > 0 ? wouldTakeCount / teacherRatings.Count : 0;
            }
        }
    }
