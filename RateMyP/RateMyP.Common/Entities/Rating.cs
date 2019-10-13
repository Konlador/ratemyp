using System;
using System.Collections.Generic;

namespace RateMyP.Entities
    {
    public class Rating : IEquatable<Rating>
        {
        public Guid Id { get; set; }
        public Teacher Teacher { get; set; }
        public Student Student { get; set; }
        public Course Course { get; set; }
        public int OverallMark { get; set; }
        public int LevelOfDifficulty { get; set; }
        public bool WouldTakeTeacherAgain { get; set; }
        public List<TeacherTag> Tags { get; set; }
        public DateTime DateCreated { get; set; }
        public string Comment { get; set; }

        public bool Equals(Rating other) =>
            other != null &&
            other.Id.Equals(Id) &&
            other.Teacher.Equals(Teacher) &&
            other.Course.Equals(Course) &&
            other.OverallMark.Equals(OverallMark) &&
            other.LevelOfDifficulty.Equals(LevelOfDifficulty) &&
            other.WouldTakeTeacherAgain.Equals(WouldTakeTeacherAgain) &&
            other.DateCreated.Equals(DateCreated) &&
            other.Comment.Equals(Comment);
        }
    }
