using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RateMyP.Entities
    {
    public enum LectureType
        {
        Lecture,
        Practice,
        Seminar
        }

    public class TeacherActivity
        {
        public Guid Id { get; set; }
        [ForeignKey("Teacher")]
        public Guid TeacherId { get; set; }
        [ForeignKey("Course")]
        public Guid CourseId { get; set; }
        public DateTime DateStarted { get; set; }
        public LectureType LectureType { get; set; }

        public Teacher Teacher { get; set; }
        public Course Course { get; set; }
        }
    }
