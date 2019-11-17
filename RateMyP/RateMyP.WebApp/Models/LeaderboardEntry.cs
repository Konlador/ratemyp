using System;
using System.ComponentModel.DataAnnotations;

namespace RateMyP.WebApp.Models
    {

    public enum EntryType
        {
        Teacher,
        Course
        }

    public class LeaderboardEntry
        {
        public Guid Id { get; set; }
        public EntryType EntryType { get; set; }
        public int AllTimePosition { get; set; }
        public int AllTimeRatingCount { get; set; }
        public double AllTimeAverage { get; set; }
        public int ThisYearPosition { get; set; }
        public int ThisYearRatingCount { get; set; }
        public double ThisYearAverage { get; set; }
        }

    }
