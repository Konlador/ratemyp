using System;
using System.ComponentModel.DataAnnotations;

namespace RateMyP.Entities
    {
    public enum LectureType
        {
        Practice,
        Seminar,
        Lecture
        }

    public class TeacherActivity
        {
        [Key]
        public Guid Id { get; set; }
        public Teacher Teacher { get; set; }
        public Course Course { get; set; }
        public DateTime DateStarted { get; set; }
        public LectureType Type { get; set; }
        }
    }
