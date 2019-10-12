//using NUnit.Framework;
//using NUnit.Framework.Internal;
//using RateMyP.Entities;
//using RateMyP.Managers;
//using System;

//namespace RateMyP.Tests
//    {
//    [TestFixture]
//    public class StudentManagerTests : RateMyPFixture
//        {
//        private StudentManager m_manager;

//        [SetUp]
//        public void SetUp()
//            {
//            PrepareDb();
//            m_manager = new StudentManager();
//            }

//        [Test]
//        public void GetAll_NoStudent()
//            {
//            var students = m_manager.GetAll();
//            Assert.AreEqual(0, students.Count);
//            }
            
//        [Test]
//        public void GetAll_SingleStudent()
//            {
//            var student = new Student
//                {
//                Id = Guid.NewGuid(),
//                Name = "Arnoldas",
//                Surname = "Svarcas",
//                Studies = "Programu sistemos",
//                Faculty = "Mifas",
//                Description = ""
//                };

//            m_manager.Add(student);
//            var students = m_manager.GetAll();
//            Assert.AreEqual(1, students.Count);
//            Assert.AreEqual(student.Id, students[0].Id);
//            Assert.AreEqual(student.Faculty, students[0].Faculty);
//            }

//        [Test]
//        public void GetAll_MultipleStudent()
//            {
//            m_manager.Add(new Student
//                {
//                Id = Guid.NewGuid(),
//                Name = "Arnoldas",
//                Surname = "Svarcas",
//                Studies = "Programu sistemos",
//                Faculty = "Mifas",
//                Description = "mldc"
//                });
//            m_manager.Add(new Student
//                {
//                Id = Guid.NewGuid(),
//                Name = "Meska",
//                Surname = "Ozys",
//                Studies = "IT",
//                Faculty = "Mifas",
//                Description = ""
//                });
//            var students = m_manager.GetAll();
//            Assert.AreEqual(2, students.Count);
//            }
//        }
//    }
