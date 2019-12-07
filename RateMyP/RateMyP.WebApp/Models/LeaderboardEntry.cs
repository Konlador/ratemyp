using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace RateMyP.WebApp.Models
    {

    public enum EntryType
        {
        Teacher,
        Course
        }

    public class LeaderboardEntry : IEquatable<LeaderboardEntry>
        {
        public Guid Id { get; set; }
        public EntryType EntryType { get; set; }
        public int AllTimePosition { get; set; }
        public int AllTimeRatingCount { get; set; }
        public double AllTimeAverage { get; set; }
        public double AllTimeScore { get; set; }
        public int ThisYearPosition { get; set; }
        public int ThisYearRatingCount { get; set; }
        public double ThisYearAverage { get; set; }
        public double ThisYearScore { get; set; }

        public bool Equals(LeaderboardEntry other) =>
        other != null &&
        other.Id.Equals(Id) &&
        other.EntryType.Equals(EntryType) &&
        other.AllTimePosition.Equals(AllTimePosition) &&
        other.AllTimeRatingCount.Equals(AllTimeRatingCount) &&
        other.AllTimeAverage.Equals(AllTimeAverage) &&
        other.AllTimeScore.Equals(AllTimeScore) &&
        other.ThisYearPosition.Equals(ThisYearPosition) &&
        other.ThisYearRatingCount.Equals(ThisYearRatingCount) &&
        other.ThisYearAverage.Equals(ThisYearAverage) &&
        other.ThisYearScore.Equals(ThisYearScore);
        }
    }

