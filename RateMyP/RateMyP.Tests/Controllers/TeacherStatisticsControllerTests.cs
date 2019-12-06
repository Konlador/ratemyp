using NUnit.Framework;
using RateMyP.WebApp;
using RateMyP.WebApp.Controllers;
using RateMyP.WebApp.Models;
using RateMyP.WebApp.Statistics;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace RateMyP.Tests.Controllers
    {
    public class TeacherStatisticsControllerTests : TestsBase
        {
        private ITeacherStatisticsAnalyzer m_analyzer;
        private ITeacherStatisticsController m_controller;

        private Teacher m_teacher;
        private List<DateMark> m_averageMarks;
        private DateMark m_dateMark;
        private TeacherStatistics m_teacherStatistics;

        [SetUp]
        public new void SetUp()
            {
            Seed(Context);
            m_analyzer = new TeacherStatisticsAnalyzer(Context);
            m_controller = new TeacherStatisticsController(m_analyzer);
            }

        [Test]
        public async Task GetTeacherStatistics_InvalidTeacherId_ReturnsEmptyTeacherStatistic()
            {
            var teacherStatisticResult = await m_controller.GetTeacherStatistics(Guid.NewGuid(), 3);
            Assert.IsFalse(teacherStatisticResult.Result is NotFoundResult);
            Assert.AreEqual(teacherStatisticResult.Value.AverageMark, 0);
            CollectionAssert.IsEmpty(teacherStatisticResult.Value.AverageMarks);
            Assert.AreEqual(teacherStatisticResult.Value.AverageLevelOfDifficulty, 0);
            Assert.AreEqual(teacherStatisticResult.Value.WouldTakeAgainRatio, 0);
            }

        [Test]
        public async Task GetTeacherStatistics_ReturnsValidTeacherStatistic()
            {
            var teacherStatisticResult = await m_controller.GetTeacherStatistics(m_teacher.Id, 3);
            Assert.AreEqual(teacherStatisticResult.Value.AverageMark, m_teacherStatistics.AverageMark);
            CollectionAssert.IsNotEmpty(teacherStatisticResult.Value.AverageMarks);
            Assert.Contains(m_dateMark, teacherStatisticResult.Value.AverageMarks);
            Assert.AreEqual(teacherStatisticResult.Value.TeacherId, m_teacherStatistics.TeacherId);
            Assert.AreEqual(teacherStatisticResult.Value.AverageLevelOfDifficulty, m_teacherStatistics.AverageLevelOfDifficulty);
            Assert.AreEqual(teacherStatisticResult.Value.WouldTakeAgainRatio, m_teacherStatistics.WouldTakeAgainRatio);
            }

        private void Seed(RateMyPDbContext context)
            {
            m_dateMark = new DateMark
                {
                Date = DateTime.Now.AddDays(-5),
                Mark = 6
                };

            m_averageMarks = new List<DateMark>();
            m_averageMarks.Add(m_dateMark);

            m_teacher = new Teacher
                {
                Id = Guid.NewGuid(),
                FirstName = "AAA",
                LastName = "aaa",
                Description = "a desc",
                Rank = "Professor",
                Faculty = "MIF"
                };

            context.Teachers.AddRange(m_teacher);

            var rating1 = new Rating
                {
                Id = Guid.NewGuid(),
                TeacherId = m_teacher.Id,
                CourseId = Guid.NewGuid(),
                OverallMark = 6,
                LevelOfDifficulty = 6,
                WouldTakeTeacherAgain = true,
                Tags = new List<RatingTag>(),
                DateCreated = m_dateMark.Date,
                Comment = "rating comment",
                StudentId = null,
                RatingType = RatingType.Teacher,
                ThumbUps = 3,
                ThumbDowns = 1
                };

            context.Ratings.Add(rating1);

            m_teacherStatistics = new TeacherStatistics
                {
                Id = Guid.NewGuid(),
                TeacherId = m_teacher.Id,
                AverageMark = 6,
                AverageMarks = m_averageMarks,
                AverageLevelOfDifficulty = 6,
                WouldTakeAgainRatio = 1

                };

            context.SaveChanges();
            }
        }
    }
