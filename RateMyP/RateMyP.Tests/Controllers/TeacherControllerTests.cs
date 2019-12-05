using NUnit.Framework;
using RateMyP.WebApp;
using RateMyP.WebApp.Controllers;
using RateMyP.WebApp.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Configuration;

namespace RateMyP.Tests.Controllers
    {
    public class TeacherControllerTests : TestsBase
        {
        private ITeachersController m_controller;

        private Teacher m_teacher1;
        private Teacher m_teacher2;
        private Teacher m_teacher3;

        [SetUp]
        public new void SetUp()
            {
            Seed(Context);
            m_controller = new TeachersController(Context);
            ConfigurationManager.AppSettings.Set("LoadedTeachersNumber", "20");
            }

        [Test]
        public async Task GetTeachers_ReturnsAllTeachers()
            {
            var teachersResult = await m_controller.GetTeachers();

            Assert.IsNull(teachersResult.Result);
            Assert.AreEqual(3, teachersResult.Value.Count());
            }

        [Test]
        public async Task GetTeacher_InvalidTeacherId_ReturnsNotFound()
            {
            var teacherResult = await m_controller.GetTeacher(Guid.NewGuid());
            Assert.IsTrue(teacherResult.Result is NotFoundResult);
            }

        [Test]
        public async Task GetTeachersIndexed_StartIndexNonZero_SkipsSomeTeachers()
            {
            var teachersResult = await m_controller.GetTeachersIndexed(2);
            Assert.IsNull(teachersResult.Result);
            Assert.AreEqual(1, teachersResult.Value.Count());
            }

        private void Seed(RateMyPDbContext context)
            {
            m_teacher1 = new Teacher
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

            context.Teachers.AddRange(m_teacher1, m_teacher2, m_teacher3);
            context.SaveChanges();
            }
        }
    }
