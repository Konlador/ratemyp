using NUnit.Framework;
using RateMyP.WebApp;
using RateMyP.WebApp.Controllers;
using RateMyP.WebApp.Models;
using RateMyP.WebApp.Statistics;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace RateMyP.Tests.Controllers
    {
    public class RatingsControllerTests : TestsBase
        {
        private IRatingsController m_controller;
        private Mock<ILeaderboardManager> m_leaderboardManagerMock;

        private Rating m_rating1;
        private Rating m_rating2;
        private Rating m_rating3;
        private Teacher m_teacher;
        private Course m_course;
        private RatingTag m_ratingTag1;
        private RatingTag m_ratingTag2;
        private Tag m_tag1;
        private Tag m_tag2;

        [SetUp]
        public new void SetUp()
            {
            Seed(Context);
            m_leaderboardManagerMock = new Mock<ILeaderboardManager>();
            m_leaderboardManagerMock.Setup(x => x.Update(It.IsAny<Guid>(), It.IsAny<EntryType>()));
            m_controller = new RatingsController(Context, m_leaderboardManagerMock.Object);
            }

        [Test]
        public async Task GetRatings_ReturnsAllRatings()
            {
            var ratingsResult = await m_controller.GetRatings();
            var ratingsResultValue = (ratingsResult as OkObjectResult).Value;
            var ratings = DeserializeRatings(ratingsResultValue.ToString());
            Assert.AreEqual(ratings.Count(), 3);
            Assert.Contains(m_rating1, ratings);
            Assert.Contains(m_rating2, ratings);
            Assert.Contains(m_rating3, ratings);
            }

        [Test]
        public async Task GetTeacherRatings_ReturnsTeacherRatings()
            {
            var teacherRatingsResult = await m_controller.GetTeacherRatings(m_teacher.Id);
            var teacherRatingsResultValue = (teacherRatingsResult as OkObjectResult).Value;
            var ratings = DeserializeRatings(teacherRatingsResultValue.ToString());
            Assert.AreEqual(ratings.Count(), 2);
            Assert.Contains(m_rating1, ratings);
            Assert.Contains(m_rating2, ratings);
            }

        [Test]
        public async Task GetCourseRatings_ReturnsCourseRatings()
            {
            var courseRatingsResult = await m_controller.GetCourseRatings(m_course.Id);
            var courseRatingsResultValue = (courseRatingsResult as OkObjectResult).Value;
            var ratings = DeserializeRatings(courseRatingsResultValue.ToString());
            Assert.AreEqual(ratings.Count(), 1);
            Assert.Contains(m_rating3, ratings);
            }

        [Test]
        public async Task GetRating_ValidRatingId_ReturnsRating()
            {
            var ratingResult = await m_controller.GetRating(m_rating1.Id);
            var ratingResultValue = (ratingResult as OkObjectResult).Value;
            var rating = DeserializeRating(ratingResultValue.ToString());
            Assert.AreEqual(rating, m_rating1);
            }

        [Test]
        public async Task GetRating_RatingWithRatingTags_ReturnsRating()
            {
            var ratingResult = await m_controller.GetRating(m_rating1.Id);
            var ratingResultValue = (ratingResult as OkObjectResult).Value;
            var ratingTags = DeserializeTagsFromRating(ratingResultValue.ToString());
            Assert.Contains(m_tag1, ratingTags);
            Assert.Contains(m_tag2, ratingTags);
            }

        [Test]
        public async Task GetRating_InvalidRatingId_ReturnsNotFound()
            {
            var ratingResult = await m_controller.GetRating(Guid.NewGuid());
            Assert.IsTrue(ratingResult is NotFoundResult);
            }

        private void Seed(RateMyPDbContext context)
            {
            m_course = new Course
                {
                Id = Guid.NewGuid(),
                Name = "Komparchas",
                Credits = 5,
                Faculty = "MIF",
                CourseType = CourseType.BUS
                };

            context.Courses.AddRange(m_course);

            m_teacher = new Teacher
                {
                Id = Guid.NewGuid(),
                FirstName = "AAA",
                LastName = "aaa",
                Description = "a desc",
                Rank = "Professor",
                Faculty = "MIF"
                };

            context.Teachers.Add(m_teacher);

            m_tag1 = new Tag
                {
                Id = Guid.NewGuid(),
                Text = "A TAG",
                Type = TagTypes.Teacher
                };

            m_tag2 = new Tag
                {
                Id = Guid.NewGuid(),
                Text = "B TAG",
                Type = TagTypes.Teacher
                };

            m_ratingTag1 = new RatingTag
                {
                TagId = m_tag1.Id,
                Tag = m_tag1
                };

            m_ratingTag2 = new RatingTag
                {
                TagId = m_tag2.Id,
                Tag = m_tag2
                };

            var ratingTags = new List<RatingTag>() { m_ratingTag1, m_ratingTag2 };

            m_rating1 = new Rating
                {
                Id = Guid.NewGuid(),
                TeacherId = m_teacher.Id,
                CourseId = Guid.NewGuid(),
                OverallMark = 5,
                LevelOfDifficulty = 6,
                WouldTakeTeacherAgain = true,
                Tags = ratingTags,
                DateCreated = DateTime.Now,
                Comment = "rating comment",
                StudentId = "159",
                RatingType = RatingType.Teacher,
                ThumbUps = 3,
                ThumbDowns = 1
                };

            m_ratingTag1.RatingId = m_rating1.Id;
            m_ratingTag2.RatingId = m_rating1.Id;

            m_rating2 = new Rating
                {
                Id = Guid.NewGuid(),
                TeacherId = m_teacher.Id,
                CourseId = Guid.NewGuid(),
                OverallMark = 7,
                LevelOfDifficulty = 9,
                WouldTakeTeacherAgain = true,
                Tags = new List<RatingTag>(),
                DateCreated = DateTime.Now,
                Comment = "rating comment",
                StudentId = "357",
                RatingType = RatingType.Teacher,
                ThumbUps = 4,
                ThumbDowns = 9
                };

            m_rating3 = new Rating
                {
                Id = Guid.NewGuid(),
                TeacherId = Guid.NewGuid(),
                CourseId = m_course.Id,
                OverallMark = 2,
                LevelOfDifficulty = 7,
                WouldTakeTeacherAgain = true,
                Tags = new List<RatingTag>(),
                DateCreated = DateTime.Now,
                Comment = "rating comment",
                StudentId = "456",
                RatingType = RatingType.Course,
                ThumbUps = 2,
                ThumbDowns = 1
                };

            context.Ratings.AddRange(m_rating1, m_rating2, m_rating3);

            context.SaveChanges();
            }

        private static Rating DeserializeRating(string rating)
            {
            Rating deserializedRating = JsonConvert.DeserializeObject<Rating>(rating);
            return deserializedRating;
            }

        private static List<Tag> DeserializeTagsFromRating(string rating)
            {
            var parsedObject = JObject.Parse(rating);
            var tagsJson = parsedObject["tags"].ToString();
            var deserializedTags = JsonConvert.DeserializeObject<List<Tag>>(tagsJson);
            return deserializedTags;
            }

        private static List<Rating> DeserializeRatings(string ratings)
            {
            List<Rating> deserializedRatings = JsonConvert.DeserializeObject<List<Rating>>(ratings);
            return deserializedRatings;
            }
        }

    
    }
