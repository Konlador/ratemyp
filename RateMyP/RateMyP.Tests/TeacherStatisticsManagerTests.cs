using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;

namespace RateMyP.Tests
    {
    [TestFixture]
    public class TeacherStatisticsManagerTests
        {
        private TeacherStatisticsManager m_manager;

        [SetUp]
        public void SetUp ()
            {
            var assemblyDirectory = Path.GetDirectoryName(GetType().Assembly.Location);
            var databasePath = Path.Combine(assemblyDirectory, "database");
            var ratingsFileName = Path.Combine (databasePath, "ratings.csv");
            var teachersFileName = Path.Combine (databasePath, "teachers.csv");

            var databaseConnection = new DatabaseConnection (ratingsFileName, teachersFileName);
            databaseConnection.Clear ();
            m_manager = new TeacherStatisticsManager (databaseConnection);
            }

        [Test]
        public void GetTeacherAverageRating_NoRating ()
            {
            var averageRating = m_manager.GetTeacherAverageRating(Guid.NewGuid ());
            Assert.AreEqual(0, averageRating);
            }

        [Test]
        public void GetTeacherAverageRating_SingleRating()
            {
            var teacherGuid = Guid.NewGuid ();
            var rating = new Rating
                {
                TeacherGuid = teacherGuid,
                TeacherMark = 4,
                LevelOfDifficulty = 2,
                WouldTakeTeacherAgain = true,
                Tags = new List<string> {"Lots of homework"},
                Comment = "Cool guy"
                };

            m_manager.AddRating (rating);
            var averageRating = m_manager.GetTeacherAverageRating (teacherGuid);
            Assert.AreEqual (4, averageRating);
            }
        }
    }
