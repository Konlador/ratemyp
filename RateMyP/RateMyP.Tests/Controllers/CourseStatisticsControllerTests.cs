using NUnit.Framework;
using RateMyP.WebApp;
using RateMyP.WebApp.Controllers;
using RateMyP.WebApp.Models;
using RateMyP.WebApp.Statistics;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RateMyP.Tests.Controllers
    {
    public class CourseStatisticsControllerTests : TestsBase
        {
        private ICourseStatisticsAnalyzer m_analyzer;
        private ICourseStatisticsController m_controller;

        private Course m_course;
        private List<DateMark> m_averageMarks;
        private DateMark m_dateMark1;
        private CourseStatistics m_courseStatistics;

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
            Assert.AreEqual(courseStatisticResult.Value.AverageMark, 0);
            CollectionAssert.IsEmpty(courseStatisticResult.Value.AverageMarks);
            Assert.AreEqual(courseStatisticResult.Value.AverageLevelOfDifficulty, 0);
            Assert.AreEqual(courseStatisticResult.Value.WouldTakeAgainRatio, 0);
            }

        [Test]
        public async Task GetCourseStatistics_ReturnsValidCourseStatistic()
            {
            var courseStatisticResult = await m_controller.GetCourseStatistics(m_course.Id, 3);
            Assert.AreEqual(courseStatisticResult.Value.AverageMark, m_courseStatistics.AverageMark);
            CollectionAssert.IsNotEmpty(courseStatisticResult.Value.AverageMarks);
            CollectionAssert.Contains(courseStatisticResult.Value.AverageMarks, m_dateMark1);
            Assert.AreEqual(courseStatisticResult.Value.CourseId, m_courseStatistics.CourseId);
            Assert.AreEqual(courseStatisticResult.Value.AverageLevelOfDifficulty, m_courseStatistics.AverageLevelOfDifficulty);
            Assert.AreEqual(courseStatisticResult.Value.WouldTakeAgainRatio, m_courseStatistics.WouldTakeAgainRatio);
            }

        private void Seed(RateMyPDbContext context)
            {
            m_dateMark1 = new DateMark
                {
                Date = DateTime.Now.AddDays(-5),
                Mark = 6
                };

            m_averageMarks = new List<DateMark>();
            m_averageMarks.Add(m_dateMark1);

            m_course = new Course
                {
                Id = Guid.NewGuid(),
                Name = "Komparchas",
                Credits = 5,
                Faculty = "MIF",
                CourseType = CourseType.BUS
                };

            context.Courses.Add(m_course);

            var rating1 = new Rating
                {
                Id = Guid.NewGuid(),
                TeacherId = Guid.NewGuid(),
                CourseId = m_course.Id,
                OverallMark = 6,
                LevelOfDifficulty = 6,
                WouldTakeTeacherAgain = true,
                Tags = new List<RatingTag>(),
                DateCreated = m_dateMark1.Date,
                Comment = "rating comment",
                StudentId = null,
                RatingType = RatingType.Course,
                ThumbUps = 3,
                ThumbDowns = 1
                };

            context.Ratings.AddRange(rating1);

            m_courseStatistics = new CourseStatistics
                {
                Id = Guid.NewGuid(),
                CourseId = m_course.Id,
                AverageMark = 6,
                AverageMarks = m_averageMarks,
                AverageLevelOfDifficulty = 6,
                WouldTakeAgainRatio = 1

                };

            context.SaveChanges();
            }
        }
    }
