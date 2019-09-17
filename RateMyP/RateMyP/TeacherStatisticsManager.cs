using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RateMyP
    {
    public class TeacherStatisticsManager
        {
        private DatabaseConnection m_database;

        public TeacherStatisticsManager (DatabaseConnection databaseConnection)
            {
            m_database = databaseConnection;
            }

        public double GetTeacherAverageRating (Guid teacherGuid)
            {
            var allRatings = m_database.GetRatings();
            var ratings = allRatings.Where ((r) => r.TeacherGuid.ToString () == teacherGuid.ToString()).ToList ();
            double sum = 0;
            foreach (var rating in ratings)
                sum += rating.TeacherMark;
            return ratings.Count > 0 ? sum / ratings.Count : 0;
            }

        public void AddRating(Rating rating)
            {
            m_database.SaveRating (rating);
            }
        }
    }
