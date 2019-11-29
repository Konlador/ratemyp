using System;

namespace RateMyP.WebApp.Models
    {
    public class CustomStarThumb : IEquatable<CustomStarThumb>
        {
        public Guid CustomStarId { get; set; }
        public string StudentId { get; set; }
        public bool ThumbUp { get; set; }

        public bool Equals(CustomStarThumb other) =>
            other != null &&
            other.CustomStarId.Equals(CustomStarId) &&
            other.StudentId.Equals(StudentId) &&
            other.ThumbUp.Equals(ThumbUp);
        }
    }
