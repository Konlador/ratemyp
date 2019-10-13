using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace RateMyP.Entities
    {
    public enum LectureType
        {
        Lecture,
        Practice,
        Seminar
        }

    public class TeacherActivity : IEquatable<TeacherActivity>
        {
        public Guid Id { get; set; }
        public Guid TeacherId { get; set; }
        public Guid CourseId { get; set; }
        public DateTime DateStarted { get; set; }
        public LectureType LectureType { get; set; }

        public bool Equals(TeacherActivity other) =>
            other != null &&
            other.Id.Equals(Id) &&
            other.TeacherId.Equals(TeacherId) &&
            other.CourseId.Equals(CourseId) &&
            other.DateStarted.Equals(DateStarted) &&
            other.LectureType.Equals(LectureType);
        }
    }
