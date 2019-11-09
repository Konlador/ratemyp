using System;

namespace RateMyP.WebApp.Models
    {
    public class RatingThumb : IEquatable<RatingThumb>
        {
        public Guid RatingId { get; set; }
        public string StudentId { get; set; }
        public bool ThumbUp { get; set; }

        public bool Equals(RatingThumb other) =>
            other != null &&
            other.RatingId.Equals(RatingId) &&
            other.StudentId.Equals(StudentId) &&
            other.ThumbUp.Equals(ThumbUp);
        }
    }
