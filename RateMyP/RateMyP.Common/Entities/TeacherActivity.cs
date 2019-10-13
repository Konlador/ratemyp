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
        public Teacher Teacher { get; set; }
        public Course Course { get; set; }
        public DateTime DateStarted { get; set; }
        public LectureType LectureType { get; set; }

        [ForeignKey("Course")]
        public Guid CourseId { get; set; }
        [ForeignKey("Teacher")]
        public Guid TeacherId { get; set; }

        public bool Equals(TeacherActivity other) =>
            other != null &&
            other.Id.Equals(Id) &&
            other.Teacher.Equals(Teacher) &&
            other.Course.Equals(Course) &&
            other.DateStarted.Equals(DateStarted) &&
            other.LectureType.Equals(LectureType);
        }
    }
