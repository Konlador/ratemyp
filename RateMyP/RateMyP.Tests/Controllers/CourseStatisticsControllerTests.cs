using NUnit.Framework;
using RateMyP.WebApp;
using RateMyP.WebApp.Controllers;
using RateMyP.WebApp.Models;
using RateMyP.WebApp.Statistics;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace RateMyP.Tests.Controllers
    {
    public class CourseStatisticsControllerTests : TestsBase
        {
        private ICourseStatisticsAnalyzer m_analyzer;
        private ICourseStatisticsController m_controller;

        private Course m_course1;

        [SetUp]
        public new void SetUp()
            {
            Seed(Context);
            m_analyzer = new CourseStatisticsAnalyzer(Context);
            m_controller = new CourseStatisticsController(m_analyzer);
            }

        [Test]
        public async Task GetCourseStatistics_InvalidCourseId_ReturnsEmptyCourseStatistic()
            {
            var courseStatisticResult = await m_controller.GetCourseStatistics(Guid.NewGuid(), 5);
            Assert.IsFalse(courseStatisticResult.Result is NotFoundResult);
            Assert.AreEqual(courseStatisticResult.Value.AverageMark, 0);
            CollectionAssert.IsEmpty(courseStatisticResult.Value.AverageMarks);
            Assert.AreEqual(courseStatisticResult.Value.AverageLevelOfDifficulty, 0);
            Assert.AreEqual(courseStatisticResult.Value.WouldTakeAgainRatio, 0);
            }

        [Test]
        public async Task GetCourseStatistics_ReturnsValidCourseStatistic()
            {
            var courseStatisticResult = await m_controller.GetCourseStatistics(m_course1.Id, 5);
            Assert.AreEqual(courseStatisticResult.Value.AverageMark, 5);
            CollectionAssert.IsNotEmpty(courseStatisticResult.Value.AverageMarks);
            Assert.AreEqual(courseStatisticResult.Value.AverageLevelOfDifficulty, 6);
            Assert.AreEqual(courseStatisticResult.Value.WouldTakeAgainRatio, 1);
            }

        private void Seed(RateMyPDbContext context)
            {
            m_course1 = new Course
                {
                Id = Guid.NewGuid(),
                Name = "Komparchas",
                Credits = 5,
                Faculty = "MIF",
                CourseType = CourseType.BUS
                };

            context.Courses.Add(m_course1);

            var rating1 = new Rating
                {
                Id = Guid.NewGuid(),
                TeacherId = Guid.NewGuid(),
                CourseId = m_course1.Id,
                OverallMark = 5,
                LevelOfDifficulty = 6,
                WouldTakeTeacherAgain = true,
                Tags = new List<RatingTag>(),
                DateCreated = DateTime.Now,
                Comment = "rating comment",
                StudentId = null,
                RatingType = RatingType.Course,
                ThumbUps = 3,
                ThumbDowns = 1
                };

            context.Ratings.Add(rating1);

            context.SaveChanges();
            }
        }
    }
