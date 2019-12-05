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
    public class TeacherActivitiesControllerTests : TestsBase
        {
        private ITeacherActivitiesController m_controller;

        private Course m_course;
        private Teacher m_teacher;

        [SetUp]
        public new void SetUp()
            {
            Seed(Context);
            m_controller = new TeacherActivitiesController(Context);
            }

        [Test]
        public async Task GetTeacherActivities_ReturnsTeacherActivities()
            {
            var teacherActivitiesResult = await m_controller.GetTeacherActivities(m_teacher.Id);

            Assert.IsNull(teacherActivitiesResult.Result);
            Assert.AreEqual(2, teacherActivitiesResult.Value.Count());
            }

        [Test]
        public async Task GetCourseTeacherActivities_ReturnsCourseTeacherActivities()
            {
            var courseTeacherActivitiesResult = await m_controller.GetCourseTeacherActivities(m_course.Id);

            Assert.IsNull(courseTeacherActivitiesResult.Result);
            Assert.AreEqual(2, courseTeacherActivitiesResult.Value.Count());
            }

        private void Seed(RateMyPDbContext context)
            {
            m_course = new Course
                {
                Id = Guid.NewGuid(),
                Name = "Komparchas",
                Credits = 5,
                Faculty = "MIF",
                CourseType = CourseType.BUS
                };

            context.Courses.AddRange(m_course);

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

            var teacherActivity1 = new TeacherActivity
                {
                Id = Guid.NewGuid(),
                TeacherId = m_teacher.Id,
                CourseId = m_course.Id,
                DateStarted = DateTime.Now,
                LectureType = LectureType.Practice
                };
            var teacherActivity2 = new TeacherActivity
                {
                Id = Guid.NewGuid(),
                TeacherId = m_teacher.Id,
                CourseId = m_course.Id,
                DateStarted = DateTime.Now,
                LectureType = LectureType.Practice
                };
            context.TeacherActivities.AddRange(teacherActivity1, teacherActivity2);
            context.SaveChanges();
            }
        }
    }
