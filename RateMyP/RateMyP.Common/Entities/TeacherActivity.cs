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
        public Guid TeacherId { get; set; }
        public Guid CourseId { get; set; }
        public DateTime DateStarted { get; set; }
        public LectureType LectureType { get; set; }

        [ForeignKey("CourseId")]
        public virtual Course Course { get; set; }

        [ForeignKey("TeacherId")]
        public virtual Teacher Teacher { get; set; }
        }
    }
