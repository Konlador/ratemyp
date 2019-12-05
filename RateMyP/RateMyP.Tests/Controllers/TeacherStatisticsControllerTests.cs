using NUnit.Framework;
using RateMyP.WebApp;
using RateMyP.WebApp.Controllers;
using RateMyP.WebApp.Models;
using RateMyP.WebApp.Statistics;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Configuration;
using System.Collections.Generic;

namespace RateMyP.Tests.Controllers
    {
    public class TeacherStatisticsControllerTests : TestsBase
        {
        private ITeacherStatisticsAnalyzer m_analyzer;
        private ITeacherStatisticsController m_controller;

        private Teacher m_teacher;

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
            var teacherStatisticResult = await m_controller.GetTeacherStatistics(Guid.NewGuid(), 5);
            Assert.IsFalse(teacherStatisticResult.Result is NotFoundResult);
            Assert.AreEqual(teacherStatisticResult.Value.AverageMark, 0);
            CollectionAssert.IsEmpty(teacherStatisticResult.Value.AverageMarks);
            Assert.AreEqual(teacherStatisticResult.Value.AverageLevelOfDifficulty, 0);
            Assert.AreEqual(teacherStatisticResult.Value.WouldTakeAgainRatio, 0);
            }

        [Test]
        public async Task GetTeacherStatistics_ReturnsValidTeacherStatistic()
            {
            var courseStatisticResult = await m_controller.GetTeacherStatistics(m_teacher.Id, 5);
            Assert.AreEqual(courseStatisticResult.Value.AverageMark, 5);
            CollectionAssert.IsNotEmpty(courseStatisticResult.Value.AverageMarks);
            Assert.AreEqual(courseStatisticResult.Value.AverageLevelOfDifficulty, 6);
            Assert.AreEqual(courseStatisticResult.Value.WouldTakeAgainRatio, 1);
            }

        private void Seed(RateMyPDbContext context)
            {
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
                OverallMark = 5,
                LevelOfDifficulty = 6,
                WouldTakeTeacherAgain = true,
                Tags = new List<RatingTag>(),
                DateCreated = DateTime.Now,
                Comment = "rating comment",
                StudentId = null,
                RatingType = RatingType.Teacher,
                ThumbUps = 3,
                ThumbDowns = 1
                };

            context.Ratings.Add(rating1);

            context.SaveChanges();
            }
        }
    }
