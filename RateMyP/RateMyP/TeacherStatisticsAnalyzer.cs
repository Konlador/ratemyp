using System;
using System.Collections.Generic;
using System.Linq;
using RateMyP.Entities;

namespace RateMyP
    {
    public class TeacherStatisticsAnalyzer
        {
        public double GetTeacherAverageMark(Guid teacherId)
            {
            List<Rating> allRatings;
            using (var context = new RateMyPDbContext())
                {
                allRatings = context.Ratings
                    .Where(r => r.Teacher.Id.ToString() == teacherId.ToString()).ToList();
                }
            double sum = 0;
            foreach (var rating in allRatings)
                sum += rating.OverallMark;
            return allRatings.Count > 0 ? sum / allRatings.Count : 0;
            }

        public double GetTeacherAverageMark(Guid teacherId, DateTime startDate, DateTime endDate)
            {
            List<Rating> allRatings;
            using (var context = new RateMyPDbContext())
                {
                allRatings = context.Ratings
                    .Where((r) => r.Teacher.Id.ToString() == teacherId.ToString() && r.DateCreated >= startDate && r.DateCreated <= endDate).ToList();
                }
            double sum = 0;
            foreach (var rating in allRatings)
                sum += rating.OverallMark;
            return allRatings.Count > 0 ? sum / allRatings.Count : 0;
            }

        public List<Double> GetTeacherAverageMarkList(Guid teacherId, DateTime startDate, DateTime endDate, int parts)
            {
            List<Double> teachersAverageMarkList = new List<Double>();

            var difference = endDate - startDate;
            var singlePartInterval = TimeSpan.FromTicks(difference.Ticks / parts);

            DateTime start = startDate;
            DateTime end = startDate + singlePartInterval;

            for (int i = 0; i < parts; i++)
                {
                teachersAverageMarkList.Add(GetTeacherAverageMark(teacherId, start, end));

                start += singlePartInterval;
                end += singlePartInterval;
                }
            return teachersAverageMarkList;
            }

        public double GetTeacherAverageLevelOfDifficultyRating(Guid teacherId)
            {
            List<Rating> allRatings;
            using (var context = new RateMyPDbContext())
                {
                allRatings = context.Ratings
                    .Where((r) => r.Teacher.Id.ToString() == teacherId.ToString()).ToList();
                }
            double sum = 0;
            foreach (var rating in allRatings)
                sum += rating.LevelOfDifficulty;
            return allRatings.Count > 0 ? sum / allRatings.Count : 0;
            }

        public double GetPercentageStudentsWouldTakeTeacherAgain(Guid teacherId)
            {
            List<Rating> allRatings;
            using (var context = new RateMyPDbContext())
                {
                allRatings = context.Ratings
                    .Where((r) => r.Teacher.Id.ToString() == teacherId.ToString()).ToList();
                }

            double wouldTakeCounter = 0;
            double wouldNotTakeCounter = 0;

            foreach (var rating in allRatings)
                {
                if (rating.WouldTakeTeacherAgain) wouldTakeCounter++;

                else if (!rating.WouldTakeTeacherAgain) wouldNotTakeCounter++;
                }

            return allRatings.Count > 0 && (wouldTakeCounter > 0 || wouldNotTakeCounter > 0)
                ? (wouldTakeCounter / (wouldTakeCounter + wouldNotTakeCounter)) * 100 : 0;
            }
        }
    }
