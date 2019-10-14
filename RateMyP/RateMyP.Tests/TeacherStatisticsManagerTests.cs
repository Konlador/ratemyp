using NUnit.Framework;
using RateMyP.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RateMyP.Client;
using Moq;
using RateMyP.Client.Managers;

namespace RateMyP.Tests
    {
    [TestFixture]
    public class TeacherStatisticsAnalyzerTests
        {
        private TeacherStatisticsAnalyzer m_analyzer;
        private Mock<IRateMyPClient> m_clientMock;
        private Teacher teacher_SingleRating;
        private Teacher teacher_MultipleRatings;

        [SetUp]
        public void SetUp()
            {
            var ratingManagerMock = new Mock<IRatingsManager>();

            List<Rating> testRatingList = new List<Rating>();

            teacher_SingleRating = new Teacher()
                {
                Id = Guid.NewGuid(),
                FirstName = "Vardenis",
                LastName = "Pavardenis",
                Description = "desc",
                Rank = "Professor",
                Faculty = "MIF"
                };

            teacher_MultipleRatings = new Teacher()
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

            ratingManagerMock
                .Setup(x => x.GetAll())
                .ReturnsAsync(testRatingList);

            m_clientMock = new Mock<IRateMyPClient>();
            m_clientMock.Setup(x => x.Ratings).Returns(ratingManagerMock.Object);

            m_analyzer = new TeacherStatisticsAnalyzer(m_clientMock.Object);
            }

        [Test]
        public async Task GetTeacherAverageMark_NoRating()
            {
            var value = await m_analyzer.GetTeacherAverageMark(Guid.NewGuid());

            Assert.AreEqual(0, value);
            }

        [Test]
        public async Task GetTeacherAverageMark_SingleRating()
            {
            var teacherId = teacher_SingleRating.Id;

            var value = await m_analyzer.GetTeacherAverageMark(teacherId);

            Assert.AreEqual(4, value);
            }

        [Test]
        public async Task GetTeacherAverageMark_MultipleRatings()
            {
            var teacherId = teacher_MultipleRatings.Id;

            var value = await m_analyzer.GetTeacherAverageMark(teacherId);

            Assert.AreEqual(6.25, value);
            }

        [Test]
        public async Task GetTeacherAverageMark_ByDate()
            {
            var teacherId = teacher_MultipleRatings.Id;

            var value = await m_analyzer.GetTeacherAverageMark(teacherId,
                new DateTime(2019, 01, 02),
                new DateTime(2019, 03, 21));

            Assert.AreEqual(7, value);
            }

        [Test]
        public async Task GetTeacherAverageMarkList_NoRating()
            {
            int parts = 5;

            var list = await m_analyzer.GetTeacherAverageMarkList(Guid.NewGuid(),
                new DateTime(2020, 12, 12),
                new DateTime(2020, 12, 12), parts);

            for (int i = 0; i < parts; i++)
                Assert.AreEqual(0, list[i]);
            }

        [Test]
        public async Task GetTeacherAverageMarkList_SingleRating()
            {
            var teacherId = teacher_SingleRating.Id;
            var parts = 5;

            var list = await m_analyzer.GetTeacherAverageMarkList(teacherId,
                new DateTime(2010, 01, 01),
                new DateTime(2019, 03, 21), parts);

            Assert.Contains(4, list);
            }

        [Test]
        public async Task GetTeacherAverageMarkList_MultipleRatings()
            {
            var teacherId = teacher_MultipleRatings.Id;
            var parts = 4;

            var list = await m_analyzer.GetTeacherAverageMarkList
                (teacherId,
                new DateTime(2019, 01, 01),
                new DateTime(2019, 03, 21), parts);

            Assert.AreEqual(7, list[0]);
            Assert.AreEqual(0, list[1]);
            Assert.AreEqual(2, list[2]);
            Assert.AreEqual(9, list[3]);
            }

        [Test]
        public async Task GetTeacherAverageLevelOfDifficultyRating_NoRating()
            {
            var averageRating = await m_analyzer.GetTeachersAverageLevelOfDifficultyRating(Guid.NewGuid());

            Assert.AreEqual(0, averageRating);
            }

        [Test]
        public async Task GetTeacherAverageLevelOfDifficultyRating_SingleRating()
            {
            var teacherId = teacher_SingleRating.Id;

            var value = await m_analyzer.GetTeachersAverageLevelOfDifficultyRating(teacherId);

            Assert.AreEqual(2, value);
            }

        [Test]
        public async Task GetTeacherAverageLevelOfDifficultyRating_MultipleRatings()
            {
            var teacherId = teacher_MultipleRatings.Id;

            var value = await m_analyzer.GetTeachersAverageLevelOfDifficultyRating(teacherId);

            Assert.AreEqual(6.5, value);
            }

        [Test]
        public async Task GetTeachersWouldTakeTeacherAgainRatio_NoRating()
            {
            var averageRating = await m_analyzer.GetTeachersWouldTakeTeacherAgainRatio(Guid.NewGuid());

            Assert.AreEqual(0, averageRating);
            }

        [Test]
        public async Task GetTeachersWouldTakeTeacherAgainRatio_SingleRating()
            {
            var teacherId = teacher_SingleRating.Id;

            var value = await m_analyzer.GetTeachersWouldTakeTeacherAgainRatio(teacherId);

            Assert.AreEqual(1, value);
            }

        [Test]
        public async Task GetTeachersWouldTakeTeacherAgainRatio_MultipleRatings()
            {
            var teacherId = teacher_MultipleRatings.Id;

            var value = await m_analyzer.GetTeachersWouldTakeTeacherAgainRatio(teacherId);

            Assert.AreEqual(0.75, value);
            }
        }
    }
