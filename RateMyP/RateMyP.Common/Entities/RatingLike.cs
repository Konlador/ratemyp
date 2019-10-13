using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace RateMyP.Entities
    {
    public class RatingLike : IEquatable<RatingLike>
        {
        public Rating Rating { get; set; }
        public Student Student { get; set; }

        [ForeignKey("Rating")]
        public Guid RatingId { get; set; }
        [ForeignKey("Student")]
        public Guid StudentId { get; set; }

        public bool Equals(RatingLike other) =>
            other != null &&
            other.Rating.Equals(Rating) &&
            other.Student.Equals(Student);
        }
    }
