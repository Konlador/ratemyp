using NUnit.Framework;
using RateMyP.WebApp;
using RateMyP.WebApp.Models;
using RateMyP.WebApp.Statistics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace RateMyP.Tests
    {
    public class TeacherStatisticsAnalyzerTests : TestsBase
        {
        private TeacherStatisticsAnalyzer m_analyzer;

        private Teacher m_teacherSingleRating;
        private Teacher m_teacherMultipleRatings;

        [SetUp]
        public new void SetUp()
            {
            Seed(Context);
            m_analyzer = new TeacherStatisticsAnalyzer(Context);
            }

        [Test]
        public void GetTeacherAverageMark_NoRating()
            {
            Assert.ThrowsAsync<InvalidDataException>(async () =>
                await m_analyzer.GetTeacherAverageMark(Guid.NewGuid()));
            }

        [Test]
        public async Task GetTeacherAverageMark_SingleRating()
            {
            var teacherId = m_teacherSingleRating.Id;
            var mark = await m_analyzer.GetTeacherAverageMark(teacherId);
            Assert.AreEqual(4, mark);
            }

        [Test]
        public async Task GetTeacherAverageMark_MultipleRatings()
            {
            var teacherId = m_teacherMultipleRatings.Id;
            var mark = await m_analyzer.GetTeacherAverageMark(teacherId);
            Assert.AreEqual(6.25, mark);
            }

        [Test]
        public void GetTeacherAverageMarks_NoRating()
            {
            Assert.ThrowsAsync<InvalidDataException>(async () =>
                await m_analyzer.GetTeacherAverageMarks(Guid.NewGuid(), 5));
            }

        [Test]
        public async Task GetTeacherAverageMarks_SingleRating()
            {
            var teacherId = m_teacherSingleRating.Id;
            var dateMarks = await m_analyzer.GetTeacherAverageMarks(teacherId, 5);
            Assert.IsTrue(dateMarks.TrueForAll(dateMark => dateMark.Mark.Equals(4.0)));
            }

        [Test]
        public async Task GetTeacherAverageMarks_MultipleRatings()
            {
            var teacherId = m_teacherMultipleRatings.Id;
            var dateMarks = await m_analyzer.GetTeacherAverageMarks(teacherId, 4);

            Assert.AreEqual(4, dateMarks[0].Mark);
            // this function returns different data depending from the current time
            //Assert.AreEqual(0, dateMarks[1].Mark);
            //Assert.AreEqual(2, dateMarks[2].Mark);
            //Assert.AreEqual(9, dateMarks[3].Mark);
            }

        [Test]
        public void GetTeacherAverageLevelOfDifficulty_NoRating()
            {
            Assert.ThrowsAsync<InvalidDataException>(async () =>
                await m_analyzer.GetTeacherAverageLevelOfDifficulty(Guid.NewGuid()));
            }

        [Test]
        public async Task GetTeacherAverageLevelOfDifficulty_SingleRating()
            {
            var teacherId = m_teacherSingleRating.Id;
            var levelOfDifficulty = await m_analyzer.GetTeacherAverageLevelOfDifficulty(teacherId);
            Assert.AreEqual(2, levelOfDifficulty);
            }

        [Test]
        public async Task GetTeacherAverageLevelOfDifficulty_MultipleRatings()
            {
            var teacherId = m_teacherMultipleRatings.Id;
            var levelOfDifficulty = await m_analyzer.GetTeacherAverageLevelOfDifficulty(teacherId);
            Assert.AreEqual(6.5, levelOfDifficulty);
            }

        [Test]
        public void GetTeacherWouldTakeTeacherAgainRatio_NoRating()
            {
            Assert.ThrowsAsync<InvalidDataException>(async () =>
                await m_analyzer.GetTeacherWouldTakeTeacherAgainRatio(Guid.NewGuid()));
            }

        [Test]
        public async Task GetTeacherWouldTakeTeacherAgainRatio_SingleRating()
            {
            var teacherId = m_teacherSingleRating.Id;
            var value = await m_analyzer.GetTeacherWouldTakeTeacherAgainRatio(teacherId);
            Assert.AreEqual(1, value);
            }

        [Test]
        public async Task GetTeacherWouldTakeTeacherAgainRatio_MultipleRatings()
            {
            var teacherId = m_teacherMultipleRatings.Id;
            var value = await m_analyzer.GetTeacherWouldTakeTeacherAgainRatio(teacherId);
            Assert.AreEqual(0.75, value);
            }

        private void Seed(RateMyPDbContext context)
            {
            m_teacherSingleRating = new Teacher
                {
                Id = Guid.NewGuid(),
                FirstName = "AAA",
                LastName = "aaa",
                Description = "a desc",
                Rank = "Professor",
                Faculty = "MIF"
                };
            m_teacherMultipleRatings = new Teacher
                {
                Id = Guid.NewGuid(),
                FirstName = "BBB",
                LastName = "bbb",
                Description = "b desc",
                Rank = "Professor",
                Faculty = "MIF"
                };
            context.Teachers.AddRange(m_teacherSingleRating, m_teacherMultipleRatings);

            var course = new Course
                {
                Id = Guid.NewGuid(),
                Name = "Taikomasis objektinis programavimas",
                CourseType = CourseType.Compulsory,
                Credits = 5,
                Faculty = "MIF"
                };
            context.Courses.Add(course);

            var ratings = new[]
                              {
                              new Rating
                                  {
                                  Id = Guid.NewGuid(),
                                  TeacherId = m_teacherSingleRating.Id,
                                  CourseId = course.Id,
                                  OverallMark = 4,
                                  LevelOfDifficulty = 2,
                                  WouldTakeTeacherAgain = true,
                                  Tags = new List<RatingTag>(),
                                  DateCreated = new DateTime(2010, 01, 02),
                                  Comment = "Cool guy"
                                  },
                              new Rating
                                  {
                                  Id = Guid.NewGuid(),
                                  TeacherId = m_teacherMultipleRatings.Id,
                                  CourseId = course.Id,
                                  OverallMark = 10,
                                  LevelOfDifficulty = 2,
                                  WouldTakeTeacherAgain = true,
                                  Tags = new List<RatingTag>(),
                                  DateCreated = new DateTime(2019, 01, 02),
                                  Comment = "Cool guy"
                                  },
                              new Rating
                                  {
                                  Id = Guid.NewGuid(),
                                  TeacherId = m_teacherMultipleRatings.Id,
                                  CourseId = course.Id,
                                  OverallMark = 9,
                                  LevelOfDifficulty = 10,
                                  WouldTakeTeacherAgain = true,
                                  Tags = new List<RatingTag>(),
                                  DateCreated = new DateTime(2019, 03, 21),
                                  Comment = "Cool guy"
                                  },
                              new Rating
                                  {
                                  Id = Guid.NewGuid(),
                                  TeacherId = m_teacherMultipleRatings.Id,
                                  CourseId = course.Id,
                                  OverallMark = 2,
                                  LevelOfDifficulty = 6,
                                  WouldTakeTeacherAgain = false,
                                  Tags = new List<RatingTag>(),
                                  DateCreated = new DateTime(2019, 02, 11),
                                  Comment = "Cool guy"
                                  },
                              new Rating
                                  {
                                  Id = Guid.NewGuid(),
                                  TeacherId = m_teacherMultipleRatings.Id,
                                  CourseId = course.Id,
                                  OverallMark = 4,
                                  LevelOfDifficulty = 8,
                                  WouldTakeTeacherAgain = true,
                                  Tags = new List<RatingTag>(),
                                  DateCreated = new DateTime(2019, 01, 01),
                                  Comment = "Cool guy"
                                  }
                              };
            context.Ratings.AddRange(ratings);

            context.SaveChanges();
            }
        }
    }
