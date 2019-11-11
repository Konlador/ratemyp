using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RateMyP.WebApp.Models
    {
    public enum RatingType
    {
        Teacher,
        Course
    }
    public class Rating : IEquatable<Rating>
        {
        public Guid Id { get; set; }
        public Guid TeacherId { get; set; }
        public Guid CourseId { get; set; }
        [Range(1, 5, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public int OverallMark { get; set; }
        [Range(1, 5, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public int LevelOfDifficulty { get; set; }
        public bool WouldTakeTeacherAgain { get; set; }
        public List<RatingTag> Tags { get; set; }
        public DateTime DateCreated { get; set; }
        public string Comment { get; set; }
        public string? StudentId { get; set; }
        public RatingType RatingType { get; set; }
        public int ThumbUps { get; set; }
        public int ThumbDowns { get; set; }

        public bool Equals(Rating other) =>
            other != null &&
            other.Id.Equals(Id) &&
            other.TeacherId.Equals(TeacherId) &&
            other.CourseId.Equals(CourseId) &&
            other.StudentId.Equals(StudentId) &&
            other.OverallMark.Equals(OverallMark) &&
            other.LevelOfDifficulty.Equals(LevelOfDifficulty) &&
            other.WouldTakeTeacherAgain.Equals(WouldTakeTeacherAgain) &&
            other.DateCreated.Equals(DateCreated) &&
            other.Comment.Equals(Comment) &&
            other.RatingType.Equals(RatingType);
    }
}
