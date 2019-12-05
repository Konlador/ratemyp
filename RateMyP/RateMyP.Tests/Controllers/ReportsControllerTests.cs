using NUnit.Framework;
using RateMyP.WebApp;
using RateMyP.WebApp.Controllers;
using RateMyP.WebApp.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Configuration;
using System.Collections.Generic;

namespace RateMyP.Tests.Controllers
    {
    public class ReportsControllerTests : TestsBase
        {
        private IReportsController m_controller;

        private Rating m_rating1;
        private CustomStar m_customStar1;

        [SetUp]
        public new void SetUp()
            {
            Seed(Context);
            m_controller = new ReportsController(Context);
            }

        [Test]
        public async Task GetRatingsReports_ReturnsAllRatingsReports()
            {
            var ratingsReportsResult = await m_controller.GetRatingsReports();

            Assert.IsNull(ratingsReportsResult.Result);
            Assert.AreEqual(2, ratingsReportsResult.Value.Count());
            }

        [Test]
        public async Task GetCustomStarsReports_ReturnsAllCustomStarReports()
            {
            var customStarsReportsResult = await m_controller.GetCustomStarsReports();

            Assert.IsNull(customStarsReportsResult.Result);
            Assert.AreEqual(2, customStarsReportsResult.Value.Count());
            }

        [Test]
        public async Task GetCustomStarReports_WithCustomStarId_ReturnsCustomStarReports()
            {
            var customStarReportsResult = await m_controller.GetCustomStarReports(m_customStar1.Id);

            Assert.IsNull(customStarReportsResult.Result);
            Assert.AreEqual(1, customStarReportsResult.Value.Count());
            }

        [Test]
        public async Task GetRatingReports_WithRatingId_ReturnsRatingReports()
            {
            var ratingReportsResult = await m_controller.GetRatingReports(m_rating1.Id);

            Assert.IsNull(ratingReportsResult.Result);
            Assert.AreEqual(1, ratingReportsResult.Value.Count());
            }

        [Test]
        public async Task GetReport_InvalidReportId_ReturnsNotFound()
            {
            var reportResult = await m_controller.GetReport(Guid.NewGuid());
            Assert.IsTrue(reportResult is NotFoundResult);
            }

        private void Seed(RateMyPDbContext context)
            {
            m_rating1 = new Rating
                {
                Id = Guid.NewGuid(),
                TeacherId = Guid.NewGuid(),
                CourseId = Guid.NewGuid(),
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

            var ratingReport1 = new RatingReport
                {
                Id = Guid.NewGuid(),
                RatingId = m_rating1.Id,
                StudentId = "undefined",
                DateCreated = DateTime.Now,
                Reason = "verbal abuse"
                };

            var ratingReport2 = new RatingReport
                {
                Id = Guid.NewGuid(),
                RatingId = Guid.NewGuid(),
                StudentId = "undefined",
                DateCreated = DateTime.Now,
                Reason = "verbal abuse"
                };

            context.RatingReports.AddRange(ratingReport1, ratingReport2);

            m_customStar1 = new CustomStar
                {
                Id = Guid.NewGuid(),
                TeacherId = Guid.NewGuid(),
                DateCreated = DateTime.Now,
                StudentId = null,
                ThumbUps = 5,
                ThumbDowns = 2,
                };

            var customStarReport1 = new CustomStarReport
                {
                Id = Guid.NewGuid(),
                CustomStarId = m_customStar1.Id,
                StudentId = "undefined",
                DateCreated = DateTime.Now,
                Reason = "verbal abuse"
                };

            var customStarReport2 = new CustomStarReport
                {
                Id = Guid.NewGuid(),
                CustomStarId = Guid.NewGuid(),
                StudentId = "undefined",
                DateCreated = DateTime.Now,
                Reason = "verbal abuse"
                };

            context.CustomStarReports.AddRange(customStarReport1, customStarReport2);

            /*m_teacher1 = new Teacher
                {
                Id = Guid.NewGuid(),
                FirstName = "AAA",
                LastName = "aaa",
                Description = "a desc",
                Rank = "Professor",
                Faculty = "MIF"
                };

            m_teacher2 = new Teacher
                {
                Id = Guid.NewGuid(),
                FirstName = "BBB",
                LastName = "bbb",
                Description = "b desc",
                Rank = "Professor",
                Faculty = "MIF"
                };

            m_teacher3 = new Teacher
                {
                Id = Guid.NewGuid(),
                FirstName = "CCC",
                LastName = "ccc",
                Description = "c desc",
                Rank = "Professor",
                Faculty = "MIF"
                };

            context.Teachers.AddRange(m_teacher1, m_teacher2, m_teacher3);*/
            context.SaveChanges();
            }
        }
    }
