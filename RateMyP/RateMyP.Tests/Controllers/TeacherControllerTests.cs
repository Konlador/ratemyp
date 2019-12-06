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
    public class TeacherControllerTests : TestsBase
        {
        private ITeachersController m_controller;

        private Teacher m_teacher1;
        private Teacher m_teacher2;
        private Teacher m_teacher3;
        private TeacherActivity m_teacherActivity1;
        private TeacherActivity m_teacherActivity2;
        private TeacherActivity m_teacherActivity3;

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
            Assert.Contains(m_teacher1, teachersResult.Value.ToList());
            Assert.Contains(m_teacher2, teachersResult.Value.ToList());
            Assert.Contains(m_teacher3, teachersResult.Value.ToList());
            }

        [Test]
        public async Task GetTeacher_InvalidTeacherId_ReturnsNotFound()
            {
            var teacherResult = await m_controller.GetTeacher(Guid.NewGuid());
            Assert.IsTrue(teacherResult.Result is NotFoundResult);
            }

        [Test]
        public async Task GetTeacher_ValidTeacherId_ReturnsNotFound()
            {
            var teacherResult = await m_controller.GetTeacher(m_teacher1.Id);
            Assert.IsNull(teacherResult.Result);
            Assert.AreEqual(m_teacher1, teacherResult.Value);
            }

        [Test]
        public async Task GetTeachersIndexed_StartIndexNonZero_SkipsSomeTeachers()
            {
            var teachersResult = await m_controller.GetTeachersIndexed(2);
            Assert.IsNull(teachersResult.Result);
            Assert.AreEqual(1, teachersResult.Value.Count());
            Assert.Contains(m_teacher3, teachersResult.Value.ToList());
            }

        [Test]
        public async Task GetTeacherWithActivities_TeacherWithActivities_ReturnsTeacherWithActivities()
            {
            var teacherResult = await m_controller.GetTeacher(m_teacher1.Id);
            Assert.IsNull(teacherResult.Result);
            Assert.IsNotNull(teacherResult.Value);
            Assert.Contains(m_teacherActivity1, teacherResult.Value.Activities.ToList());
            Assert.Contains(m_teacherActivity2, teacherResult.Value.Activities.ToList());
            Assert.Contains(m_teacherActivity3, teacherResult.Value.Activities.ToList());
            }

        private void Seed(RateMyPDbContext context)
            {
            var teacherActivities = new List<TeacherActivity>();

            m_teacher1 = new Teacher
                {
                Id = Guid.NewGuid(),
                FirstName = "AAA",
                LastName = "aaa",
                Description = "a desc",
                Rank = "Professor",
                Faculty = "MIF",
                Activities = teacherActivities
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

            m_teacherActivity1 = new TeacherActivity
                {
                Id = Guid.NewGuid(),
                TeacherId = m_teacher1.Id,
                CourseId = Guid.NewGuid(),
                DateStarted = DateTime.Now,
                LectureType = LectureType.Practice
                };

            m_teacherActivity2 = new TeacherActivity
                {
                Id = Guid.NewGuid(),
                TeacherId = m_teacher1.Id,
                CourseId = Guid.NewGuid(),
                DateStarted = DateTime.Now,
                LectureType = LectureType.Lecture
                };

            m_teacherActivity3 = new TeacherActivity
                {
                Id = Guid.NewGuid(),
                TeacherId = m_teacher1.Id,
                CourseId = Guid.NewGuid(),
                DateStarted = DateTime.Now,
                LectureType = LectureType.Seminar
                };

            teacherActivities.AddRange(new List<TeacherActivity>(){m_teacherActivity1, m_teacherActivity2, m_teacherActivity3});

            context.Teachers.AddRange(m_teacher1, m_teacher2, m_teacher3);
            context.SaveChanges();
            }
        }
    }
