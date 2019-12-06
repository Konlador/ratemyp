using NUnit.Framework;
using RateMyP.WebApp;
using RateMyP.WebApp.Controllers;
using RateMyP.WebApp.Models;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Collections.Generic;

namespace RateMyP.Tests.Controllers
    {
    public class CustomStarControllerTests : TestsBase
        {
        private ICustomStarController m_controller;
        private readonly IHttpClientFactory m_clientFactory;

        private Teacher m_teacher;
        private CustomStar m_customStar;

        [SetUp]
        public new void SetUp()
            {
            Seed(Context);
            m_controller = new CustomStarController(Context, m_clientFactory);
            }

        [Test]
        public async Task GetCustomStarAsync_InvalidCustomStarId_ReturnsNotFound()
            {
            var customStarResult = await m_controller.GetCustomStarAsync(Guid.NewGuid());
            Assert.IsTrue(customStarResult is NotFoundResult);
            }

        [Test]
        public async Task GetCustomStarAsync_ValidCustomStarId_ReturnsCustomStar()
            {
            var customStarResult = await m_controller.GetCustomStarAsync(m_customStar.Id);
            var customStar = (customStarResult as OkObjectResult).Value as CustomStar;
            Assert.AreEqual(customStar, m_customStar);
            }

        [Test]
        public async Task GetTeacherCustomStarsAsync_InvalidTeacherId_ReturnsEmptyList()
            {
            var customTeacherStarResult = await m_controller.GetTeacherCustomStarsAsync(Guid.NewGuid());
            var customTeacherStars = (customTeacherStarResult as OkObjectResult).Value as List<CustomStar>;
            CollectionAssert.IsEmpty(customTeacherStars);
            }

        [Test]
        public async Task GetTeacherCustomStarsAsync_ValidTeacherId_ReturnsCustomStars()
            {
            var customTeacherStarResult = await m_controller.GetTeacherCustomStarsAsync(m_teacher.Id);
            var customTeacherStars = (customTeacherStarResult as OkObjectResult).Value as List<CustomStar>;
            CollectionAssert.IsNotEmpty(customTeacherStars);
            CollectionAssert.Contains(customTeacherStars, m_customStar);
            }

        [Test]
        public async Task GetCustomStarImageAsync_InvalidTeacherId_ReturnsNotFound()
            {
            var customTeacherStarImageResult = await m_controller.GetCustomStarImageAsync(Guid.NewGuid());
            Assert.IsTrue(customTeacherStarImageResult is NotFoundResult);
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

            context.Teachers.Add(m_teacher);

            m_customStar = new CustomStar
                {
                Id = Guid.NewGuid(),
                TeacherId = m_teacher.Id,
                DateCreated = DateTime.Now,
                StudentId = null,
                ThumbUps = 5,
                ThumbDowns = 2
                };

            context.CustomStars.Add(m_customStar);
            context.SaveChanges();
            }
        }
    }
