using NUnit.Framework;
using RateMyP.Entities;
using System;
using System.Collections.Generic;
using RateMyP.Client;
using Moq;
using RateMyP.Client.Managers;

namespace RateMyP.Tests
    {
    [TestFixture]
    public class TeacherStatisticsAnalyzerTests
        {
        private TeacherStatisticsAnalyzer m_analyzer;
        //private RatingManager m_ratingManager;
        private Mock<IRateMyPClient> m_clientMock;



        [SetUp]
        public void SetUp()
            {
            var teacherManagerMock = new Mock<ITeachersManager>();
            var ratingManagerMock = new Mock<IRatingsManager>();

            List<Teacher> testTeacherList = new List<Teacher>();
            List<Rating> testRatingList = new List<Rating>();

            var teacher_SingleRating = new Teacher()
                {
                Id = Guid.NewGuid(),
                FirstName = "Vardenis",
                LastName = "Pavardenis",
                Description = "desc",
                Rank = "Professor",
                Faculty = "MIF"
                };

            var teacher_MultipleRatings = new Teacher()
                {
                Id = Guid.NewGuid(),
                FirstName = "Vardenis",
                LastName = "Pavardenis",
                Description = "desc",
                Rank = "Professor",
                Faculty = "MIF"
                };

            var student = new Student
                {
                Id = Guid.NewGuid(),
                FirstName = "Studentas",
                LastName = "Studentelis",
                Studies = "Programu sistemos",
                Faculty = "MIF",
                Description = "desc"
                };

            var course = new Course
                {
                Id = Guid.NewGuid(),
                Name = "Taikomasis objektinis programavimas",
                CourseType = CourseType.Compulsory,
                Credits = 5,
                Faculty = "MIF"
                };

            var rating_SingleRating = new Rating()
                {
                Id = Guid.NewGuid(),
                Teacher = teacher_SingleRating,
                Student = student,
                Course = course,
                OverallMark = 4,
                LevelOfDifficulty = 2,
                WouldTakeTeacherAgain = true,
                Tags = "Lots of homework",
                DateCreated = new DateTime(2010, 01, 02),
                Comment = "Cool guy"
                };

            var rating1_MultipleRatings = new Rating()
                {
                Id = Guid.NewGuid(),
                Teacher = teacher_MultipleRatings,
                Student = student,
                Course = course,
                OverallMark = 10,
                LevelOfDifficulty = 2,
                WouldTakeTeacherAgain = true,
                Tags = "Lots of homework",
                DateCreated = new DateTime(2019, 01, 02),
                Comment = "Cool guy"
                };

            var rating2_MultipleRatings = new Rating()
                {
                Id = Guid.NewGuid(),
                Teacher = teacher_MultipleRatings,
                Student = student,
                Course = course,
                OverallMark = 9,
                LevelOfDifficulty = 10,
                WouldTakeTeacherAgain = true,
                Tags = "Lots of homework",
                DateCreated = new DateTime(2019, 03, 21),
                Comment = "Cool guy"
                };

            var rating3_MultipleRatings = new Rating()
                {
                Id = Guid.NewGuid(),
                Teacher = teacher_MultipleRatings,
                Student = student,
                Course = course,
                OverallMark = 2,
                LevelOfDifficulty = 6,
                WouldTakeTeacherAgain = false,
                Tags = "Lots of homework",
                DateCreated = new DateTime(2019, 02, 11),
                Comment = "Cool guy"
                };

            var rating4_MultipleRatings = new Rating()
                {
                Id = Guid.NewGuid(),
                Teacher = teacher_MultipleRatings,
                Student = student,
                Course = course,
                OverallMark = 4,
                LevelOfDifficulty = 8,
                WouldTakeTeacherAgain = true,
                Tags = "Lots of homework",
                DateCreated = new DateTime(2019, 01, 01),
                Comment = "Cool guy"
                };


            testRatingList.AddRange(new List<Rating>() { rating_SingleRating, rating1_MultipleRatings, rating2_MultipleRatings, rating3_MultipleRatings, rating4_MultipleRatings });
            testTeacherList.AddRange(new List<Teacher>() { teacher_SingleRating, teacher_MultipleRatings });

            ratingManagerMock
                .Setup(x => x.GetAll())
                .ReturnsAsync(testRatingList);

            teacherManagerMock
                .Setup(x => x.GetAll())
                .ReturnsAsync(testTeacherList);

            m_clientMock = new Mock<IRateMyPClient>();
            m_clientMock.Setup(x => x.Ratings).Returns(ratingManagerMock.Object);
            m_clientMock.Setup(x => x.Teachers).Returns(teacherManagerMock.Object);

            m_analyzer = new TeacherStatisticsAnalyzer(m_clientMock.Object);
            }

        [Test]
        public void GetTeacherAverageMark_NoRating()
            {
            var value = m_analyzer.GetTeacherAverageMark(Guid.NewGuid()).Result;

            Assert.AreEqual(0, value);
            }

        [Test]
        public void GetTeacherAverageMark_SingleRating()
            {
            var teachers = m_clientMock.Object.Teachers.GetAll().Result;
            var teacherId = teachers[0].Id;

            var value = m_analyzer.GetTeacherAverageMark(teacherId).Result;

            Assert.AreEqual(4, value);
            }

        [Test]
        public void GetTeacherAverageMark_MultipleRating()
            {
            var teachers = m_clientMock.Object.Teachers.GetAll().Result;
            var teacherId = teachers[1].Id;

            var value = m_analyzer.GetTeacherAverageMark(teacherId).Result;

            Assert.AreEqual(6.25, value);
            }

        [Test]
        public void GetTeacherAverageMark_ByDate()
            {
            var teachers = m_clientMock.Object.Teachers.GetAll().Result;
            var teacherId = teachers[1].Id;

            var value = m_analyzer.GetTeacherAverageMark(teacherId,
                new DateTime(2019, 01, 02),
                new DateTime(2019, 03, 21))
                .Result;

            Assert.AreEqual(7, value);
            }

        [Test]
        public void GetTeacherAverageMarkList_NoRating()
            {
            int parts = 5;

            var list = m_analyzer.GetTeacherAverageMarkList(Guid.NewGuid(),
                new DateTime(2020, 12, 12),
                new DateTime(2020, 12, 12), parts)
                .Result;

            for (int i = 0; i < parts; i++)
                Assert.AreEqual(0, list[i]);
            }

        [Test]
        public void GetTeacherAverageMarkList_SingleRating()
            {
            var teachers = m_clientMock.Object.Teachers.GetAll().Result;
            var teacherId = teachers[0].Id;
            var parts = 5;

            var list = m_analyzer.GetTeacherAverageMarkList(teacherId,
                new DateTime(2010, 01, 01),
                new DateTime(2019, 03, 21), parts)
                .Result;

            Assert.Contains(4, list);
            }

        [Test]
        public void GetTeacherAverageMarkList_MultipleRating()
            {
            var teachers = m_clientMock.Object.Teachers.GetAll().Result;
            var teacherId = teachers[1].Id;
            var parts = 4;

            var list = m_analyzer.GetTeacherAverageMarkList
                (teacherId,
                new DateTime(2019, 01, 01),
                new DateTime(2019, 03, 21), parts)
                .Result;

            Assert.Contains(7, list);
            Assert.Contains(2, list);
            Assert.Contains(9, list);
            }

        [Test]
        public void GetTeacherAverageLevelOfDifficultyRating_NoRating()
            {
            var averageRating = m_analyzer.GetTeachersAverageLevelOfDifficultyRating(Guid.NewGuid()).Result;

            Assert.AreEqual(0, averageRating);
            }

        [Test]
        public void GetTeacherAverageLevelOfDifficultyRating_SingleRating()
            {
            var teachers = m_clientMock.Object.Teachers.GetAll().Result;
            var teacherId = teachers[0].Id;

            var value = m_analyzer.GetTeachersAverageLevelOfDifficultyRating(teacherId).Result;

            Assert.AreEqual(2, value);
            }

        [Test]
        public void GetTeacherAverageLevelOfDifficultyRating_MultipleRatings()
            {
            var teachers = m_clientMock.Object.Teachers.GetAll().Result;
            var teacherId = teachers[1].Id;

            var value = m_analyzer.GetTeachersAverageLevelOfDifficultyRating(teacherId).Result;

            Assert.AreEqual(6.5, value);
            }

        [Test]
        public void GetTeachersWouldTakeTeacherAgainRatio_NoRating()
            {
            var averageRating = m_analyzer.GetTeachersWouldTakeTeacherAgainRatio(Guid.NewGuid()).Result;

            Assert.AreEqual(0, averageRating);
            }

        [Test]
        public void GetTeachersWouldTakeTeacherAgainRatio_SingleRating()
            {
            var teachers = m_clientMock.Object.Teachers.GetAll().Result;
            var teacherId = teachers[0].Id;

            var value = m_analyzer.GetTeachersWouldTakeTeacherAgainRatio(teacherId).Result;

            Assert.AreEqual(1, value);
            }

        [Test]
        public void GetTeachersWouldTakeTeacherAgainRatio_MultipleRatings()
            {
            var teachers = m_clientMock.Object.Teachers.GetAll().Result;
            var teacherId = teachers[1].Id;

            var value = m_analyzer.GetTeachersWouldTakeTeacherAgainRatio(teacherId).Result;

            Assert.AreEqual(0.75, value);
            }
        }
    }
