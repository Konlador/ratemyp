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
        private TeacherActivity m_teacherActivity1;
        private TeacherActivity m_courseTeacherActivity1;

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
            Assert.AreEqual(1, teacherActivitiesResult.Value.Count());
            Assert.Contains(m_teacherActivity1, teacherActivitiesResult.Value.ToList());
            }

        [Test]
        public async Task GetCourseTeacherActivities_ReturnsCourseTeacherActivities()
            {
            var courseTeacherActivitiesResult = await m_controller.GetCourseTeacherActivities(m_course.Id);

            Assert.IsNull(courseTeacherActivitiesResult.Result);
            Assert.AreEqual(1, courseTeacherActivitiesResult.Value.Count());
            Assert.Contains(m_courseTeacherActivity1, courseTeacherActivitiesResult.Value.ToList());
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

            m_teacherActivity1 = new TeacherActivity
                {
                Id = Guid.NewGuid(),
                TeacherId = m_teacher.Id,
                CourseId = Guid.NewGuid(),
                DateStarted = DateTime.Now,
                LectureType = LectureType.Practice
                };
            m_courseTeacherActivity1 = new TeacherActivity
                {
                Id = Guid.NewGuid(),
                TeacherId = Guid.NewGuid(),
                CourseId = m_course.Id,
                DateStarted = DateTime.Now,
                LectureType = LectureType.Practice
                };
            context.TeacherActivities.AddRange(m_teacherActivity1, m_courseTeacherActivity1);
            context.SaveChanges();
            }
        }
    }
