using NUnit.Framework;
using RateMyP.Entities;
using RateMyP.Managers;
using System;

namespace RateMyP.Tests
    {
    [TestFixture]
    public class TeacherStatisticsAnalyzerTests
        {
        private TeacherStatisticsAnalyzer m_manager;
        private RatingManager m_ratingManager;

        [SetUp]
        public void SetUp ()
            {
            var databaseConnection = new SQLDbConnection();
            databaseConnection.Clear();
            m_ratingManager = new RatingManager(databaseConnection);
            m_manager = new TeacherStatisticsAnalyzer(m_ratingManager);
            }

        [Test]
        public void GetTeacherAverageMark_NoRating()
            {
            var averageRating = m_manager.GetTeacherAverageMark(Guid.NewGuid ());
            Assert.AreEqual(0, averageRating);
            }

        [Test]
        public void GetTeacherAverageMark_SingleRating()
            {
            var rating = new Rating
                {
                Id = Guid.NewGuid(),
                TeacherId = Guid.NewGuid(),
                StudentId = Guid.NewGuid(),
                OverallMark = 4,
                LevelOfDifficulty = 2,
                WouldTakeTeacherAgain = true,
                Tags = "Lots of homework",
                Comment = "Cool guy"
                };

            m_ratingManager.AddRating(rating);
            var averageRating = m_manager.GetTeacherAverageMark(rating.TeacherId);
            Assert.AreEqual (4, averageRating);
            }
        }
    }
