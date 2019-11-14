using System;

namespace RateMyP.WebApp.Models
    {

    public class CourseLeaderboardPosition
        {
        public Guid Id { get; set; }
        public int AllTimePosition { get; set; }
        public int AllTimeRatingCount { get; set; }
        public double AllTimeAverage { get; set; }
        public int ThisYearPosition { get; set; }
        public int ThisYearRatingCount { get; set; }
        public double ThisYearAverage { get; set; }
        }

    }