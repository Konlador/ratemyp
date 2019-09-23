using RateMyP.Managers;
using System;
using System.Linq;

namespace RateMyP
    {
    public class TeacherStatisticsAnalyzer
        {
        private RatingManager m_ratingManger;

        public TeacherStatisticsAnalyzer (RatingManager ratingManager)
            {
            m_ratingManger = ratingManager;
            }

        public double GetTeacherAverageMark (Guid teacherId)
            {
            var allRatings = m_ratingManger.GetAll();
            var ratings = allRatings.Where ((r) => r.TeacherId.ToString() == teacherId.ToString()).ToList();
            double sum = 0;
            foreach (var rating in ratings)
                sum += rating.OverallMark;
            return ratings.Count > 0 ? sum / ratings.Count : 0;
            }
        }
    }
