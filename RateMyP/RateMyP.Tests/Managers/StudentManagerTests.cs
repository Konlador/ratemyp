using NUnit.Framework;
using NUnit.Framework.Internal;
using RateMyP.Entities;
using RateMyP.Managers;
using System;

namespace RateMyP.Tests
    {
    [TestFixture]
    public class StudentManagerTests
        {
        private StudentManager m_manager;

        [SetUp]
        public void SetUp()
            {
            var databaseConnection = new SQLDbConnection();
            databaseConnection.Clear ();
            m_manager = new StudentManager(databaseConnection);
            }

        [Test]
        public void GetAllStudents_NoStudent()
            {
            var teachers = m_manager.GetAllStudents();
            Assert.AreEqual(0, teachers.Count);
            }
            
        [Test]
        public void GetAllStudents_SingleStudent()
            {
            var student = new Student
                {
                Id = Guid.NewGuid(),
                Name = "Arnoldas",
                Surname = "Svarcas",
                Studies = "Programu sistemos",
                Faculty = "Mifas"
                };

            m_manager.AddStudent(student);
            var students = m_manager.GetAllStudents();
            Assert.AreEqual(1, students.Count);
            Assert.AreEqual(student.Id, students[0].Id);
            Assert.AreEqual(student.Faculty, students[0].Faculty);
            }

        [Test]
        public void GetAllStudents_MultipleStudent()
            {
            m_manager.AddStudent(new Student
                {
                Id = Guid.NewGuid(),
                Name = "Arnoldas",
                Surname = "Svarcas",
                Studies = "Programu sistemos",
                Faculty = "Mifas"
                });
            m_manager.AddStudent(new Student
                {
                Id = Guid.NewGuid(),
                Name = "Meska",
                Surname = "Ozys",
                Studies = "IT",
                Faculty = "Mifas"
                });
            var students = m_manager.GetAllStudents();
            Assert.AreEqual(2, students.Count);
            }
        }
    }
