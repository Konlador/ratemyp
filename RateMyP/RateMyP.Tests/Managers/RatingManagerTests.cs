using NUnit.Framework;
using NUnit.Framework.Internal;
using RateMyP.Entities;
using RateMyP.Managers;
using System;

namespace RateMyP.Tests
    {
    [TestFixture]
    public class RatingManagerTests
        {
        private RatingManager m_manager;

        [SetUp]
        public void SetUp()
            {
            var databaseConnection = new SQLDbConnection();
            databaseConnection.Clear ();
            m_manager = new RatingManager(databaseConnection);
            }

        [Test]
        public void GetAllRatings_NoRating()
            {
            var teachers = m_manager.GetAllRatings();
            Assert.AreEqual(0, teachers.Count);
            }
            
        [Test]
        public void GetAllRatings_SingleRating()
            {
            var rating = new Rating
                {
                Id = Guid.NewGuid(),
                TeacherId = Guid.NewGuid(),
                StudentId = Guid.NewGuid(),
                OverallMark = 2,
                LevelOfDifficulty = 5,
                WouldTakeTeacherAgain = true,
                Tags = "nice|smart",
                Comment = "good guy"
            };

            m_manager.AddRating(rating);
            var ratings = m_manager.GetAllRatings();
            Assert.AreEqual(1, ratings.Count);
            Assert.AreEqual (rating.Id, ratings[0].Id);
            Assert.AreEqual (rating.Comment, "good guy");
            }

        [Test]
        public void GetAllRatings_MultipleRatings()
            {
            m_manager.AddRating(new Rating
                {
                Id = Guid.NewGuid(),
                TeacherId = Guid.NewGuid(),
                StudentId = Guid.NewGuid(),
                OverallMark = 2,
                LevelOfDifficulty = 5,
                WouldTakeTeacherAgain = true,
                Tags = "nice|smart",
                Comment = "good guy"
                });

            m_manager.AddRating(new Rating
                {
                Id = Guid.NewGuid(),
                TeacherId = Guid.NewGuid(),
                StudentId = Guid.NewGuid(),
                OverallMark = 6,
                LevelOfDifficulty = 9,
                WouldTakeTeacherAgain = false,
                Tags = "bad|smells",
                Comment = "meh"
                });

            var ratings = m_manager.GetAllRatings();
            Assert.AreEqual(2, ratings.Count);
            }
        }
    }
