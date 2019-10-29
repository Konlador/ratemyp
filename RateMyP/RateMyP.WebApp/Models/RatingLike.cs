using System;

namespace RateMyP.WebApp.Models
    {
    public class RatingLike : IEquatable<RatingLike>
        {
        public Guid RatingId { get; set; }
        public Guid StudentId { get; set; }

        public bool Equals(RatingLike other) =>
            other != null &&
            other.RatingId.Equals(RatingId) &&
            other.StudentId.Equals(StudentId);
        }
    }
