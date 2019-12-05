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
    public class StudentsControllerTests : TestsBase
        {
        private IStudentsController m_controller;

        private Student m_student1;
        private Student m_student2;
        private Student m_student3;

        [SetUp]
        public new void SetUp()
            {
            Seed(Context);
            m_controller = new StudentsController(Context);
            }

        [Test]
        public async Task GetStudents_ReturnsAllStudents()
            {
            var studentsResult = await m_controller.GetStudents();

            Assert.IsNull(studentsResult.Result);
            Assert.AreEqual(3, studentsResult.Value.Count());
            }

        [Test]
        public async Task GetStudent_WithStudentId_ReturnsStudent()
            {
            var studentResult = await m_controller.GetStudent("1954658");
            Assert.IsNull(studentResult.Result);
            }

        private void Seed(RateMyPDbContext context)
            {
            m_student1 = new Student
                {
                Id = "1954658",
                Studies = null
                };

            m_student2 = new Student
                {
                Id = "2131564",
                Studies = null

                };

            m_student3 = new Student
                {
                Id = "1233546",
                Studies = null
                };

            context.Students.AddRange(m_student1, m_student2, m_student3);
            context.SaveChanges();
            }
        }
    }
