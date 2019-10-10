//using System;
//using System.Collections.Generic;
//using System.Linq;
//using RateMyP.Entities;

//namespace RateMyP
//    {
//    public class TeacherStatisticsAnalyzer
//        {
//        public double GetTeacherAverageMark (Guid teacherId)
//            {
//            List<Rating> allRatings;
//            using (var context = new RateMyPDbContext())
//                {
//                allRatings = context.Ratings.ToList();
//                }
//            var ratings = allRatings.Where ((r) => r.Teacher.Id.ToString() == teacherId.ToString()).ToList();
//            double sum = 0;
//            foreach (var rating in ratings)
//                sum += rating.OverallMark;
//            return ratings.Count > 0 ? sum / ratings.Count : 0;
//            }
//        }
//    }
