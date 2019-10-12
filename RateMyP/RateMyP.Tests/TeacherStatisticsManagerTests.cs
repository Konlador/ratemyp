//using NUnit.Framework;
//using RateMyP.Entities;
//using RateMyP.Client.Managers;
//using System;

//namespace RateMyP.Tests
//    {
//    [TestFixture]
//    public class TeacherStatisticsAnalyzerTests
//        {
//        private TeacherStatisticsAnalyzer m_analyzer;
//        private RatingManager m_ratingManager;

//        [SetUp]
//        public void SetUp()
//            {
//            m_ratingManager = new RatingManager();
//            m_analyzer = new TeacherStatisticsAnalyzer(m_ratingManager);
//            }

//        [Test]
//        public void GetTeacherAverageMark_NoRating()
//            {
//            var averageRating = m_analyzer.GetTeacherAverageMark(Guid.NewGuid());
//            Assert.AreEqual(0, averageRating);
//            }

//        [Test]
//        public void GetTeacherAverageMark_SingleRating()
//            {
//            var rating = new Rating
//                {
//                Id = Guid.NewGuid(),
//                TeacherId = Guid.NewGuid(),
//                StudentId = Guid.NewGuid(),
//                OverallMark = 4,
//                LevelOfDifficulty = 2,
//                WouldTakeTeacherAgain = true,
//                Tags = "Lots of homework",
//                Comment = "Cool guy",
//                CourseId = Guid.NewGuid(),
//                DateCreated = DateTime.Now
//                };

//            m_ratingManager.Add(rating);
//            var averageRating = m_analyzer.GetTeacherAverageMark(rating.TeacherId);
//            Assert.AreEqual(4, averageRating);
//            }
//        }
//    }
