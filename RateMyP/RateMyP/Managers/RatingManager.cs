using RateMyP.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using RateMyP.Db;
using static RateMyP.Constants;

namespace RateMyP.Managers
    {
    public interface IRatingManager
        {
        List<Rating> GetAllRatings();
        Rating GetRating(Guid ratingId);
        void AddRating(Rating rating);
        }

    public class RatingManager : IRatingManager
        {
        private readonly ISQLDbConnection m_connection;

        public RatingManager(ISQLDbConnection connection)
            {
            m_connection = connection;
            }

        public List<Rating> GetAllRatings()
            {
            var ratings = new List<Rating>();
            using (var reader = m_connection.ExecuteQuery($"SELECT * FROM [{TABLE_RATINGS}]"))
                {
                while (reader.Read())
                    {
                    var rating = new Rating
                        {
                        Id = reader.SafeGetGuid(PROPERTY_ID, Guid.Empty),
                        TeacherId = reader.SafeGetGuid(PROPERTY_TEACHER_ID, Guid.Empty),
                        StudentId = reader.SafeGetGuid(PROPERTY_STUDENT_ID, Guid.Empty),
                        OverallMark = reader.SafeGetInt(PROPERTY_OVERALL_MARK),
                        LevelOfDifficulty = reader.SafeGetInt(PROPERTY_LEVEL_OF_DIFFICULTY),
                        WouldTakeTeacherAgain = reader.SafeGetBool(PROPERTY_WOULD_TAKE_TEACHER_AGAIN),
                        Tags = reader.SafeGetString(PROPERTY_TAGS),
                        Comment = reader.SafeGetString(PROPERTY_COMMENT)
                        };
                    ratings.Add(rating);
                    }
                }

            return ratings;
            }

        public Rating GetRating(Guid ratingId)
            {
            return GetAllRatings().First(rating => rating.Id.Equals(ratingId));
            }

        public void AddRating(Rating rating)
            {
            m_connection.ExecuteNonQuery($"INSERT INTO [{TABLE_RATINGS}]", rating);
            }
        }
    }
