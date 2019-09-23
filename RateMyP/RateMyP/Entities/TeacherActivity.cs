using System;
using System.Data.Linq.Mapping;
using static RateMyP.Constants;

namespace RateMyP.Entities
    {
    public enum LectureType
        {
        Practice,
        Seminar,
        Lecture
        }

    [Table(Name = TABLE_TEACHER_ACTIVITIES)]
    public class TeacherActivity
        {
        [Column(IsPrimaryKey = true, Name = "Id")]
        public Guid Id { get; set; }
        [Column(Name = "DateStarted")]
        public DateTime DateStarted { get; set; }
        [Column(Name = "CourseId")]
        public Guid CourseId { get; set; }
        [Column(Name = "Type")]
        public LectureType Type { get; set; }
        }
    }
