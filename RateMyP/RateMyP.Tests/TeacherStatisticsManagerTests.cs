using NUnit.Framework;
using RateMyP.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RateMyP.Tests
    {
    [TestFixture]
    public class TeacherStatisticsAnalyzerTests : RateMyPFixture
        {
        private TeacherStatisticsAnalyzer m_analyzer;
        //private RatingManager m_ratingManager;

        [SetUp]
        public void SetUp()
            {
            m_analyzer = new TeacherStatisticsAnalyzer();
            }

        [Test]
        public void GetTeacherAverageMark_NoRating()
            {
            var averageRating = m_analyzer.GetTeacherAverageMark(Guid.NewGuid());
            Assert.AreEqual(0, averageRating);
            }

        [Test]
        public void GetTeacherAverageMark_SingleRating()
            {
            var teacher = new Teacher()
                {
                Id = Guid.NewGuid(),
                FirstName = "Vardenis",
                LastName = "Pavardenis",
                Description = "desc",
                Rank = "Professor",
                Faculty = "MIF"
                };

            var student = new Student()
                {
                Id = Guid.NewGuid(),
                FirstName = "Studentas",
                LastName = "Studentelis",
                Studies = "Programu sistemos",
                Faculty = "MIF",
                Description = "desc"
                };

            var course = new Course()
                {
                Id = Guid.NewGuid(),
                Name = "Taikomasis objektinis programavimas",
                Type = CourseType.Compulsory,
                Credits = 5,
                Faculty = "MIF"
                };

            var rating = new Rating()
                {
                Id = Guid.NewGuid(),
                Teacher = teacher,
                Student = student,
                Course = course,
                OverallMark = 4,
                LevelOfDifficulty = 2,
                WouldTakeTeacherAgain = true,
                Tags = "Lots of homework",
                DateCreated = DateTime.Now,
                Comment = "Cool guy"
                };

            List<Rating> ratings = new List<Rating>();

            ratings.Add(rating);

            using (var context = new RateMyPDbContext())
                {
                context.Ratings.AddRange(ratings);
                context.SaveChanges();
                }

            var averageRating = m_analyzer.GetTeacherAverageMark(teacher.Id);
            Assert.AreEqual(4, averageRating);
            }

        [Test]
        public void GetTeacherAverageMark_MultipleRating()
            {
            var teacher = new Teacher()
                {
                Id = Guid.NewGuid(),
                FirstName = "Vardenis",
                LastName = "Pavardenis",
                Description = "desc",
                Rank = "Professor",
                Faculty = "MIF"
                };

            var student = new Student()
                {
                Id = Guid.NewGuid(),
                FirstName = "Studentas",
                LastName = "Studentelis",
                Studies = "Programu sistemos",
                Faculty = "MIF",
                Description = "desc"
                };

            var course = new Course()
                {
                Id = Guid.NewGuid(),
                Name = "Taikomasis objektinis programavimas",
                Type = CourseType.Compulsory,
                Credits = 5,
                Faculty = "MIF"
                };

            var rating1 = new Rating()
                {
                Id = Guid.NewGuid(),
                Teacher = teacher,
                Student = student,
                Course = course,
                OverallMark = 4,
                LevelOfDifficulty = 2,
                WouldTakeTeacherAgain = true,
                Tags = "Lots of homework",
                DateCreated = DateTime.Now,
                Comment = "Cool guy"
                };

            var rating2 = new Rating()
                {
                Id = Guid.NewGuid(),
                Teacher = teacher,
                Student = student,
                Course = course,
                OverallMark = 9,
                LevelOfDifficulty = 2,
                WouldTakeTeacherAgain = true,
                Tags = "Lots of homework",
                DateCreated = DateTime.Now,
                Comment = "Cool guy"
                };

            var rating3 = new Rating()
                {
                Id = Guid.NewGuid(),
                Teacher = teacher,
                Student = student,
                Course = course,
                OverallMark = 2,
                LevelOfDifficulty = 2,
                WouldTakeTeacherAgain = true,
                Tags = "Lots of homework",
                DateCreated = DateTime.Now,
                Comment = "Cool guy"
                };

            List<Rating> ratings = new List<Rating>();

            ratings.Add(rating1);
            ratings.Add(rating2);
            ratings.Add(rating3);

            using (var context = new RateMyPDbContext())
                {
                context.Ratings.AddRange(ratings);
                context.SaveChanges();
                }

            var averageRating = m_analyzer.GetTeacherAverageMark(teacher.Id);
            Assert.AreEqual(5, averageRating);
            }

        [Test]
        public void GetTeacherAverageMark_ByDate()
            {
            var teacher = new Teacher()
                {
                Id = Guid.NewGuid(),
                FirstName = "Vardenis",
                LastName = "Pavardenis",
                Description = "desc",
                Rank = "Professor",
                Faculty = "MIF"
                };

            var student = new Student()
                {
                Id = Guid.NewGuid(),
                FirstName = "Studentas",
                LastName = "Studentelis",
                Studies = "Programu sistemos",
                Faculty = "MIF",
                Description = "desc"
                };

            var course = new Course()
                {
                Id = Guid.NewGuid(),
                Name = "Taikomasis objektinis programavimas",
                Type = CourseType.Compulsory,
                Credits = 5,
                Faculty = "MIF"
                };

            var rating1 = new Rating()
                {
                Id = Guid.NewGuid(),
                Teacher = teacher,
                Student = student,
                Course = course,
                OverallMark = 10,
                LevelOfDifficulty = 2,
                WouldTakeTeacherAgain = true,
                Tags = "Lots of homework",
                DateCreated = new DateTime(2019, 01, 02),
                Comment = "Cool guy"
                };

            var rating2 = new Rating()
                {
                Id = Guid.NewGuid(),
                Teacher = teacher,
                Student = student,
                Course = course,
                OverallMark = 9,
                LevelOfDifficulty = 2,
                WouldTakeTeacherAgain = true,
                Tags = "Lots of homework",
                DateCreated = new DateTime(2019, 03, 21),
                Comment = "Cool guy"
                };

            var rating3 = new Rating()
                {
                Id = Guid.NewGuid(),
                Teacher = teacher,
                Student = student,
                Course = course,
                OverallMark = 2,
                LevelOfDifficulty = 2,
                WouldTakeTeacherAgain = true,
                Tags = "Lots of homework",
                DateCreated = new DateTime(2019, 02, 11),
                Comment = "Cool guy"
                };

            var rating4 = new Rating()
                {
                Id = Guid.NewGuid(),
                Teacher = teacher,
                Student = student,
                Course = course,
                OverallMark = 5,
                LevelOfDifficulty = 2,
                WouldTakeTeacherAgain = true,
                Tags = "Lots of homework",
                DateCreated = new DateTime(2019, 01, 01),
                Comment = "Cool guy"
                };

            List<Rating> ratings = new List<Rating>();

            ratings.Add(rating1);
            ratings.Add(rating2);
            ratings.Add(rating3);
            ratings.Add(rating4);

            using (var context = new RateMyPDbContext())
                {
                context.Ratings.AddRange(ratings);
                context.SaveChanges();
                }

            var averageRating = m_analyzer.GetTeacherAverageMark(teacher.Id, new DateTime(2019, 01, 02), new DateTime(2019, 03, 21));
            Assert.AreEqual(7, averageRating);
            }

        [Test]
        public void GetTeacherAverageMarkList_NoRating()
            {
            int parts = 5;
            var list = m_analyzer.GetTeacherAverageMarkList(Guid.NewGuid(), new DateTime(2020, 12, 12),
                new DateTime(2020, 12, 12), parts);

            for (int i = 0; i < parts; i++)
                Assert.AreEqual(0, list[i]);
            }

        [Test]
        public void GetTeacherAverageMarkList_SingleRating()
            {
            var teacher = new Teacher()
                {
                Id = Guid.NewGuid(),
                FirstName = "Vardenis",
                LastName = "Pavardenis",
                Description = "desc",
                Rank = "Professor",
                Faculty = "MIF"
                };

            var student = new Student()
                {
                Id = Guid.NewGuid(),
                FirstName = "Studentas",
                LastName = "Studentelis",
                Studies = "Programu sistemos",
                Faculty = "MIF",
                Description = "desc"
                };

            var course = new Course()
                {
                Id = Guid.NewGuid(),
                Name = "Taikomasis objektinis programavimas",
                Type = CourseType.Compulsory,
                Credits = 5,
                Faculty = "MIF"
                };

            var rating = new Rating()
                {
                Id = Guid.NewGuid(),
                Teacher = teacher,
                Student = student,
                Course = course,
                OverallMark = 4,
                LevelOfDifficulty = 2,
                WouldTakeTeacherAgain = true,
                Tags = "Lots of homework",
                DateCreated = new DateTime(2010, 10, 10),
                Comment = "Cool guy"
                };

            List<Rating> ratings = new List<Rating>();

            ratings.Add(rating);

            using (var context = new RateMyPDbContext())
                {
                context.Ratings.AddRange(ratings);
                context.SaveChanges();
                }

            int parts = 5;

            var list = m_analyzer.GetTeacherAverageMarkList(teacher.Id, new DateTime(2010, 10, 10),
                new DateTime(2010, 12, 12), parts);

            Assert.Contains(4, list);
            }

        [Test]
        public void GetTeacherAverageMarkList_MultipleRating()
            {
            var teacher = new Teacher()
                {
                Id = Guid.NewGuid(),
                FirstName = "Vardenis",
                LastName = "Pavardenis",
                Description = "desc",
                Rank = "Professor",
                Faculty = "MIF"
                };

            var student = new Student()
                {
                Id = Guid.NewGuid(),
                FirstName = "Studentas",
                LastName = "Studentelis",
                Studies = "Programu sistemos",
                Faculty = "MIF",
                Description = "desc"
                };

            var course = new Course()
                {
                Id = Guid.NewGuid(),
                Name = "Taikomasis objektinis programavimas",
                Type = CourseType.Compulsory,
                Credits = 5,
                Faculty = "MIF"
                };

            var rating1 = new Rating()
                {
                Id = Guid.NewGuid(),
                Teacher = teacher,
                Student = student,
                Course = course,
                OverallMark = 4,
                LevelOfDifficulty = 2,
                WouldTakeTeacherAgain = true,
                Tags = "Lots of homework",
                DateCreated = new DateTime(2010, 10, 01),
                Comment = "Cool guy"
                };

            var rating2 = new Rating()
                {
                Id = Guid.NewGuid(),
                Teacher = teacher,
                Student = student,
                Course = course,
                OverallMark = 6,
                LevelOfDifficulty = 2,
                WouldTakeTeacherAgain = true,
                Tags = "Lots of homework",
                DateCreated = new DateTime(2010, 11, 01),
                Comment = "Cool guy"
                };

            var rating3 = new Rating()
                {
                Id = Guid.NewGuid(),
                Teacher = teacher,
                Student = student,
                Course = course,
                OverallMark = 5,
                LevelOfDifficulty = 2,
                WouldTakeTeacherAgain = true,
                Tags = "Lots of homework",
                DateCreated = new DateTime(2010, 12, 02),
                Comment = "Cool guy"
                };

            var rating4 = new Rating()
                {
                Id = Guid.NewGuid(),
                Teacher = teacher,
                Student = student,
                Course = course,
                OverallMark = 3,
                LevelOfDifficulty = 2,
                WouldTakeTeacherAgain = true,
                Tags = "Lots of homework",
                DateCreated = new DateTime(2011, 01, 02),
                Comment = "Cool guy"
                };

            var rating5 = new Rating()
                {
                Id = Guid.NewGuid(),
                Teacher = teacher,
                Student = student,
                Course = course,
                OverallMark = 10,
                LevelOfDifficulty = 2,
                WouldTakeTeacherAgain = true,
                Tags = "Lots of homework",
                DateCreated = new DateTime(2010, 10, 31),
                Comment = "Cool guy"
                };


            List<Rating> ratings = new List<Rating>();

            ratings.Add(rating1);
            ratings.Add(rating2);
            ratings.Add(rating3);
            ratings.Add(rating4);
            ratings.Add(rating5);

            using (var context = new RateMyPDbContext())
                {
                context.Ratings.AddRange(ratings);
                context.SaveChanges();
                }

            int parts = 4;
            var list = m_analyzer.GetTeacherAverageMarkList(teacher.Id, new DateTime(2010, 10, 01),
                new DateTime(2011, 02, 01), parts);

            Assert.Contains(7, list);
            Assert.Contains(6, list);
            Assert.Contains(5, list);
            Assert.Contains(3, list);
            }

        [Test]
        public void GetTeacherAverageLevelOfDifficultyRating_NoRating()
            {
            var averageRating = m_analyzer.GetTeacherAverageLevelOfDifficultyRating(Guid.NewGuid());
            Assert.AreEqual(0, averageRating);
            }

        [Test]
        public void GetTeacherAverageLevelOfDifficultyRating_SingleRating()
            {
            var teacher = new Teacher()
                {
                Id = Guid.NewGuid(),
                FirstName = "Vardenis",
                LastName = "Pavardenis",
                Description = "desc",
                Rank = "Professor",
                Faculty = "MIF"
                };

            var student = new Student()
                {
                Id = Guid.NewGuid(),
                FirstName = "Studentas",
                LastName = "Studentelis",
                Studies = "Programu sistemos",
                Faculty = "MIF",
                Description = "desc"
                };

            var course = new Course()
                {
                Id = Guid.NewGuid(),
                Name = "Taikomasis objektinis programavimas",
                Type = CourseType.Compulsory,
                Credits = 5,
                Faculty = "MIF"
                };

            var rating = new Rating()
                {
                Id = Guid.NewGuid(),
                Teacher = teacher,
                Student = student,
                Course = course,
                OverallMark = 4,
                LevelOfDifficulty = 2,
                WouldTakeTeacherAgain = true,
                Tags = "Lots of homework",
                DateCreated = DateTime.Now,
                Comment = "Cool guy"
                };

            List<Rating> ratings = new List<Rating>();

            ratings.Add(rating);

            using (var context = new RateMyPDbContext())
                {
                context.Ratings.AddRange(ratings);
                context.SaveChanges();
                }

            var averageRating = m_analyzer.GetTeacherAverageLevelOfDifficultyRating(teacher.Id);
            Assert.AreEqual(2, averageRating);
            }

        [Test]
        public void GetTeacherAverageLevelOfDifficultyRating_MultipleRatings()
            {
            var teacher = new Teacher()
                {
                Id = Guid.NewGuid(),
                FirstName = "Vardenis",
                LastName = "Pavardenis",
                Description = "desc",
                Rank = "Professor",
                Faculty = "MIF"
                };

            var student = new Student()
                {
                Id = Guid.NewGuid(),
                FirstName = "Studentas",
                LastName = "Studentelis",
                Studies = "Programu sistemos",
                Faculty = "MIF",
                Description = "desc"
                };

            var course = new Course()
                {
                Id = Guid.NewGuid(),
                Name = "Taikomasis objektinis programavimas",
                Type = CourseType.Compulsory,
                Credits = 5,
                Faculty = "MIF"
                };

            var rating1 = new Rating()
                {
                Id = Guid.NewGuid(),
                Teacher = teacher,
                Student = student,
                Course = course,
                OverallMark = 4,
                LevelOfDifficulty = 5,
                WouldTakeTeacherAgain = true,
                Tags = "Lots of homework",
                DateCreated = DateTime.Now,
                Comment = "Cool guy"
                };

            var rating2 = new Rating()
                {
                Id = Guid.NewGuid(),
                Teacher = teacher,
                Student = student,
                Course = course,
                OverallMark = 9,
                LevelOfDifficulty = 6,
                WouldTakeTeacherAgain = true,
                Tags = "Lots of homework",
                DateCreated = DateTime.Now,
                Comment = "Cool guy"
                };

            var rating3 = new Rating()
                {
                Id = Guid.NewGuid(),
                Teacher = teacher,
                Student = student,
                Course = course,
                OverallMark = 2,
                LevelOfDifficulty = 7,
                WouldTakeTeacherAgain = true,
                Tags = "Lots of homework",
                DateCreated = DateTime.Now,
                Comment = "Cool guy"
                };

            List<Rating> ratings = new List<Rating>();

            ratings.Add(rating1);
            ratings.Add(rating2);
            ratings.Add(rating3);

            using (var context = new RateMyPDbContext())
                {
                context.Ratings.AddRange(ratings);
                context.SaveChanges();
                }

            var averageRating = m_analyzer.GetTeacherAverageLevelOfDifficultyRating(teacher.Id);
            Assert.AreEqual(6, averageRating);
            }

        [Test]
        public void GetPercentageStudentsWouldTakeTeacherAgain_NoRating()
            {
            var averageRating = m_analyzer.GetPercentageStudentsWouldTakeTeacherAgain(Guid.NewGuid());
            Assert.AreEqual(0, averageRating);
            }

        [Test]
        public void GetPercentageStudentsWouldTakeTeacherAgain_SingleRating()
            {
            var teacher = new Teacher()
                {
                Id = Guid.NewGuid(),
                FirstName = "Vardenis",
                LastName = "Pavardenis",
                Description = "desc",
                Rank = "Professor",
                Faculty = "MIF"
                };

            var student = new Student()
                {
                Id = Guid.NewGuid(),
                FirstName = "Studentas",
                LastName = "Studentelis",
                Studies = "Programu sistemos",
                Faculty = "MIF",
                Description = "desc"
                };

            var course = new Course()
                {
                Id = Guid.NewGuid(),
                Name = "Taikomasis objektinis programavimas",
                Type = CourseType.Compulsory,
                Credits = 5,
                Faculty = "MIF"
                };

            var rating = new Rating()
                {
                Id = Guid.NewGuid(),
                Teacher = teacher,
                Student = student,
                Course = course,
                OverallMark = 4,
                LevelOfDifficulty = 2,
                WouldTakeTeacherAgain = true,
                Tags = "Lots of homework",
                DateCreated = DateTime.Now,
                Comment = "Cool guy"
                };

            List<Rating> ratings = new List<Rating>();

            ratings.Add(rating);

            using (var context = new RateMyPDbContext())
                {
                context.Ratings.AddRange(ratings);
                context.SaveChanges();
                }

            var averageRating = m_analyzer.GetPercentageStudentsWouldTakeTeacherAgain(teacher.Id);
            Assert.AreEqual(100, averageRating);
            }

        [Test]
        public void GetPercentageStudentsWouldTakeTeacherAgain_MultipleRatings()
            {
            var teacher = new Teacher()
                {
                Id = Guid.NewGuid(),
                FirstName = "Vardenis",
                LastName = "Pavardenis",
                Description = "desc",
                Rank = "Professor",
                Faculty = "MIF"
                };

            var student = new Student()
                {
                Id = Guid.NewGuid(),
                FirstName = "Studentas",
                LastName = "Studentelis",
                Studies = "Programu sistemos",
                Faculty = "MIF",
                Description = "desc"
                };

            var course = new Course()
                {
                Id = Guid.NewGuid(),
                Name = "Taikomasis objektinis programavimas",
                Type = CourseType.Compulsory,
                Credits = 5,
                Faculty = "MIF"
                };

            var rating1 = new Rating()
                {
                Id = Guid.NewGuid(),
                Teacher = teacher,
                Student = student,
                Course = course,
                OverallMark = 4,
                LevelOfDifficulty = 5,
                WouldTakeTeacherAgain = false,
                Tags = "Lots of homework",
                DateCreated = DateTime.Now,
                Comment = "Cool guy"
                };

            var rating2 = new Rating()
                {
                Id = Guid.NewGuid(),
                Teacher = teacher,
                Student = student,
                Course = course,
                OverallMark = 9,
                LevelOfDifficulty = 6,
                WouldTakeTeacherAgain = false,
                Tags = "Lots of homework",
                DateCreated = DateTime.Now,
                Comment = "Cool guy"
                };

            var rating3 = new Rating()
                {
                Id = Guid.NewGuid(),
                Teacher = teacher,
                Student = student,
                Course = course,
                OverallMark = 2,
                LevelOfDifficulty = 7,
                WouldTakeTeacherAgain = false,
                Tags = "Lots of homework",
                DateCreated = DateTime.Now,
                Comment = "Cool guy"
                };

            var rating4 = new Rating()
                {
                Id = Guid.NewGuid(),
                Teacher = teacher,
                Student = student,
                Course = course,
                OverallMark = 2,
                LevelOfDifficulty = 7,
                WouldTakeTeacherAgain = true,
                Tags = "Lots of homework",
                DateCreated = DateTime.Now,
                Comment = "Cool guy"
                };

            List<Rating> ratings = new List<Rating>();

            ratings.Add(rating1);
            ratings.Add(rating2);
            ratings.Add(rating3);
            ratings.Add(rating4);

            using (var context = new RateMyPDbContext())
                {
                context.Ratings.AddRange(ratings);
                context.SaveChanges();
                }

            var averageRating = m_analyzer.GetPercentageStudentsWouldTakeTeacherAgain(teacher.Id);
            Assert.AreEqual(25, averageRating);
            }
        }
    }
