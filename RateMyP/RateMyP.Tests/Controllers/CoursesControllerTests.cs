using NUnit.Framework;
using RateMyP.WebApp;
using RateMyP.WebApp.Controllers;
using RateMyP.WebApp.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace RateMyP.Tests.Controllers
    {
    public class CoursesControllerTests : TestsBase
        {
        private ICoursesController m_controller;

        private Course m_course1;
        private Course m_course2;
        private Teacher m_teacher;

        [SetUp]
        public new void SetUp()
            {
            Seed(Context);
            m_controller = new CoursesController(Context);
            }

        [Test]
        public async Task GetCourses_ReturnsAllCourses()
            {
            var coursesResult = await m_controller.GetCourses();

            Assert.IsNull(coursesResult.Result);
            Assert.AreEqual(3, coursesResult.Value.Count());
            }

        [Test]
        public async Task GetCourse_InvalidCourseId_ReturnsNotFound()
            {
            var courseResult = await m_controller.GetCourse(Guid.NewGuid());
            Assert.IsTrue(courseResult.Result is NotFoundResult);
            }

        [Test]
        public async Task GetCoursesIndexed_StartIndexNonZero_SkipsSomeCourses()
            {
            var coursesResult = await m_controller.GetCoursesIndexed(2);
            Assert.IsNull(coursesResult.Result);
            Assert.AreEqual(1, coursesResult.Value.Count());
            }

        [Test]
        public async Task GetTeacherCourses_TeacherWithCourses_ReturnCoursesAssignedToTeacher()
            {
            var teacherCoursesResult = await m_controller.GetTeacherCourses(m_teacher.Id);
            Assert.IsNull(teacherCoursesResult.Result);
            Assert.AreEqual(2, teacherCoursesResult.Value.Count());
            Assert.Contains(m_course1, teacherCoursesResult.Value.ToList());
            Assert.Contains(m_course2, teacherCoursesResult.Value.ToList());
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

            m_course2 = new Course
                {
                Id = Guid.NewGuid(),
                Name = "Duomenu strukturos",
                Credits = 10,
                Faculty = "MIF",
                CourseType = CourseType.Complimentary
                };

            var course3 = new Course
                {
                Id = Guid.NewGuid(),
                Name = "malkos",
                Credits = 6,
                Faculty = "Zap",
                CourseType = CourseType.BUS
                };

            context.Courses.AddRange(m_course1, m_course2, course3);

            m_teacher = new Teacher
                {
                Id = Guid.NewGuid(),
                FirstName = "AAA",
                LastName = "aaa",
                Description = "a desc",
                Rank = "Professor",
                Faculty = "MIF"
                };
            var teacherSingleCourse = new Teacher
                {
                Id = Guid.NewGuid(),
                FirstName = "BBB",
                LastName = "bbb",
                Description = "b desc",
                Rank = "Professor",
                Faculty = "MIF"
                };
            context.Teachers.AddRange(m_teacher, teacherSingleCourse);

            var teacherActivity1 = new TeacherActivity
                {
                Id = Guid.NewGuid(),
                TeacherId = m_teacher.Id,
                CourseId = m_course1.Id,
                DateStarted = DateTime.Now,
                LectureType = LectureType.Practice
                };
            var teacherActivity2 = new TeacherActivity
                {
                Id = Guid.NewGuid(),
                TeacherId = m_teacher.Id,
                CourseId = m_course2.Id,
                DateStarted = DateTime.Now,
                LectureType = LectureType.Practice
                };
            var teacherSingleCourseActivity = new TeacherActivity
                {
                Id = Guid.NewGuid(),
                TeacherId = teacherSingleCourse.Id,
                CourseId = course3.Id,
                DateStarted = DateTime.Now,
                LectureType = LectureType.Seminar
                };
            context.TeacherActivities.AddRange(teacherActivity1, teacherActivity2, teacherSingleCourseActivity);
            context.SaveChanges();
            }
        }
    }
