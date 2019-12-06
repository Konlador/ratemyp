using NUnit.Framework;
using RateMyP.WebApp;
using RateMyP.WebApp.Controllers;
using RateMyP.WebApp.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace RateMyP.Tests.Controllers
    {
    public class LeaderboardControllerTests : TestsBase
        {
        private ILeaderboardController m_controller;

        private Teacher m_teacher1;
        private Teacher m_teacher2;
        private Course m_course1;
        private Course m_course2;

        [SetUp]
        public new void SetUp()
            {
            Seed(Context);
            m_controller = new LeaderboardController(Context);
            }

        [Test]
        public async Task GetTeacherEntriesAllTime_ReturnsAllTeacherEntriesAllTime()
            {
            var teacherAllTimeEntriesResult = await m_controller.GetTeacherEntriesAllTime();
            Assert.IsNull(teacherAllTimeEntriesResult.Result);
            Assert.AreEqual(2, teacherAllTimeEntriesResult.Value.Count());
            }

        [Test]
        public async Task GetTeacherEntriesThisYear_ReturnsAllTeacherEntriesThisYear()
            {
            var teacherEntriesThisYearResult = await m_controller.GetTeacherEntriesThisYear();
            Assert.IsNull(teacherEntriesThisYearResult.Result);
            Assert.AreEqual(2, teacherEntriesThisYearResult.Value.Count());
            }

        [Test]
        public async Task GetTeacherEntry_InvalidEntryId_ReturnsNotFound()
            {
            var teacherEntryResult = await m_controller.GetTeacherEntry(Guid.NewGuid());
            Assert.IsTrue(teacherEntryResult.Result is NotFoundResult);
            }

        [Test]
        public async Task GetCourseEntriesAllTime_ReturnsAllCourseEntriesAllTime()
            {
            var courseAllTimeEntriesResult = await m_controller.GetCourseEntriesAllTime();
            Assert.IsNull(courseAllTimeEntriesResult.Result);
            Assert.AreEqual(2, courseAllTimeEntriesResult.Value.Count());
            }

        [Test]
        public async Task GetCourseEntriesThisYear_ReturnsAllCourseEntriesThisYear()
            {
            var courseEntriesThisYearResult = await m_controller.GetCourseEntriesThisYear();
            Assert.IsNull(courseEntriesThisYearResult.Result);
            Assert.AreEqual(2, courseEntriesThisYearResult.Value.Count());
            }

        [Test]
        public async Task GetCourseEntry_InvalidEntryId_ReturnsNotFound()
            {
            var courseEntryResult = await m_controller.GetCourseEntry(Guid.NewGuid());
            Assert.IsTrue(courseEntryResult.Result is NotFoundResult);
            }

        private void Seed(RateMyPDbContext context)
            {
            m_teacher1 = new Teacher
                {
                Id = Guid.NewGuid(),
                FirstName = "AAA",
                LastName = "aaa",
                Description = "a desc",
                Rank = "Professor",
                Faculty = "MIF"
                };

            m_teacher2 = new Teacher
                {
                Id = Guid.NewGuid(),
                FirstName = "BBB",
                LastName = "bbb",
                Description = "b desc",
                Rank = "Professor",
                Faculty = "MIF"
                };

            context.Teachers.AddRange(m_teacher1, m_teacher2);

            m_course1 = new Course
                {
                Id = Guid.NewGuid(),
                Name = "Komparchas",
                Credits = 5,
                Faculty = "MIF",
                CourseType = CourseType.BUS
                };

            m_course2 = new Course
                {
                Id = Guid.NewGuid(),
                Name = "Duomenu strukturos",
                Credits = 10,
                Faculty = "MIF",
                CourseType = CourseType.Complimentary
                };

            context.Courses.AddRange(m_course1, m_course2);

            var leaderboardEntry1 = new LeaderboardEntry
                {
                Id = m_teacher1.Id,
                EntryType = EntryType.Teacher,
                AllTimePosition = 1,
                AllTimeRatingCount = 5,
                AllTimeAverage = 6,
                AllTimeScore = 7,
                ThisYearPosition = 1,
                ThisYearRatingCount = 5,
                ThisYearAverage = 6,
                ThisYearScore = 7
                };

            var leaderboardEntry2 = new LeaderboardEntry
                {
                Id = m_teacher2.Id,
                EntryType = EntryType.Teacher,
                AllTimePosition = 2,
                AllTimeRatingCount = 4,
                AllTimeAverage = 5,
                AllTimeScore = 6,
                ThisYearPosition = 2,
                ThisYearRatingCount = 4,
                ThisYearAverage = 5,
                ThisYearScore = 6
                };

            var leaderboardEntry3 = new LeaderboardEntry
                {
                Id = m_course1.Id,
                EntryType = EntryType.Course,
                AllTimePosition = 1,
                AllTimeRatingCount = 5,
                AllTimeAverage = 6,
                AllTimeScore = 7,
                ThisYearPosition = 1,
                ThisYearRatingCount = 5,
                ThisYearAverage = 6,
                ThisYearScore = 7
                };

            var leaderboardEntry4 = new LeaderboardEntry
                {
                Id = m_course2.Id,
                EntryType = EntryType.Course,
                AllTimePosition = 2,
                AllTimeRatingCount = 4,
                AllTimeAverage = 5,
                AllTimeScore = 6,
                ThisYearPosition = 2,
                ThisYearRatingCount = 4,
                ThisYearAverage = 5,
                ThisYearScore = 6
                };

            context.Leaderboard.AddRange(leaderboardEntry1, leaderboardEntry2, leaderboardEntry3, leaderboardEntry4);

            context.SaveChanges();
            }
        }
    }
