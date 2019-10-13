
//using NUnit.Framework;
//using NUnit.Framework.Internal;
//using RateMyP.Entities;
//using RateMyP.Managers;
//using System;

//namespace RateMyP.Tests
//    {
//    [TestFixture]
//    public class TeacherManagerTests: RateMyPFixture
//        {
//        private TeacherManager m_manager;

//        [SetUp]
//        public void SetUp()
//            {
//            PrepareDb();
//            m_manager = new TeacherManager();
//            }

//        [Test]
//        public void GetAllTeachers_NoTeacher()
//            {
//            var teachers = m_manager.GetAll();
//            Assert.AreEqual(0, teachers.Count);
//            }
            
//        [Test]
//        public void GetAllTeachers_SingleTeacher()
//            {
//            var teacher = new Teacher
//                {
//                Id = Guid.NewGuid(),
//                Name = "Kestis",
//                Surname = "Morka",
//                Rank = AcademicRank.Professor
//                };

//            m_manager.Add(teacher);
//            var teachers = m_manager.GetAll();
//            Assert.AreEqual(1, teachers.Count);
//            Assert.AreEqual (teacher.Id, teachers[0].Id);
//            Assert.AreEqual (teacher.Name, teachers[0].Name);
//            }

//        [Test]
//        public void GetAllTeachers_MultipleTeachers()
//            {
//            m_manager.Add(new Teacher
//                {
//                Id = Guid.NewGuid(),
//                Name = "Kestis",
//                Surname = "Morka",
//                Rank = AcademicRank.Professor
//                });
//            m_manager.Add(new Teacher
//                {
//                Id = Guid.NewGuid(),
//                Name = "Coupe",
//                Surname = "BMW",
//                Rank = AcademicRank.Lecturer
//                });

//            var teachers = m_manager.GetAll();
//            Assert.AreEqual(2, teachers.Count);
//            }
//        }
//    }
    