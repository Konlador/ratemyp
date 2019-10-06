using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RateMyP.Entities
{
    public class TeacherActivityPeriferal
    {
        public Guid Id { get; set; }
        public Guid TeacherId { get; set; }
        public Guid CourseId { get; set; }
        public DateTime DateStarted { get; set; }
        public LectureType Type { get; set; }
    }
}
