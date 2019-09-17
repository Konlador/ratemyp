using NUnit.Framework;
using NUnit.Framework.Internal;
using System;
using System.IO;

namespace RateMyP.Tests
    {
    [TestFixture]
    public class TeacherManagerTests
        {
        private TeacherManager m_manager;

        [SetUp]
        public void SetUp()
            {
            var assemblyDirectory = Path.GetDirectoryName (GetType ().Assembly.Location);
            var databasePath = Path.Combine(assemblyDirectory, "database");
            var ratingsFileName = Path.Combine(databasePath, "ratings.csv");
            var teachersFileName = Path.Combine(databasePath, "teachers.csv");

            var databaseConnection = new DatabaseConnection(ratingsFileName, teachersFileName);
            databaseConnection.Clear ();
            m_manager = new TeacherManager(databaseConnection);
            }

        [Test]
        public void GetTeachers_NoTeacher()
            {
            var teachers = m_manager.GetTeachers();
            Assert.AreEqual(0, teachers.Count);
            }

        [Test]
        public void GetTeachers_SingleTeacher()
            {
            var teacher = new Teacher
                {
                Id = Guid.NewGuid(),
                Name = "Romualdas",
                Surname = "Kašuba",
                Rank = AcademicRank.Professor
                };

            m_manager.AddTeacher(teacher);
            var teachers = m_manager.GetTeachers();
            Assert.AreEqual(1, teachers.Count);
            Assert.AreEqual (teacher.Id, teachers[0].Id);
            Assert.AreEqual ("Romualdas", teachers[0].Name);
            }
        }
    }
