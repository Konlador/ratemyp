using NUnit.Framework;
using RateMyP.WebApp;
using RateMyP.WebApp.Controllers;
using RateMyP.WebApp.Models;
using System;
using System.Threading.Tasks;

namespace RateMyP.Tests.Controllers
    {
    public class CoursesControllerTests : TestsBase
        {
        private ICoursesController m_controller;

        [SetUp]
        public new void SetUp()
            {
            Seed(Context);
            m_controller = new CoursesController(Context);
            }

        [Test]
        public async Task GetCourses_ReturnsAllCourses()
            {
            var courses = await m_controller.GetCourses();
            }

        private static void Seed(RateMyPDbContext context)
            {
            context.Courses.Add(new Course
                {
                Id = Guid.NewGuid(),
                Name = "Komparchas",
                Credits = 5,
                Faculty = "MIF",
                CourseType = CourseType.BUS
                });

            context.Courses.Add(new Course
                {
                Id = Guid.NewGuid(),
                Name = "Duomenu strukturos",
                Credits = 10,
                Faculty = "MIF",
                CourseType = CourseType.Complimentary
                });

            context.SaveChanges();
            }
        }
    }
