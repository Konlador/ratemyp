using System;
using System.Data.Linq.Mapping;
using static RateMyP.Constants;

namespace RateMyP.Entities
    {
    [Table(Name = TABLE_RATINGS)]
    public class Rating
        {
        [Column(IsPrimaryKey = true, Name = "Id")]
        public Guid Id { get; set; }
        [Column(Name = "TeacherId")]
        public Guid TeacherId { get; set; }
        [Column(Name = "StudentId")]
        public Guid StudentId { get; set; }
        [Column(Name = "CourseId")]
        public Guid CourseId { get; set; }
        [Column(Name = "OverallMark")]
        public int OverallMark { get; set; }
        [Column(Name = "LevelOfDifficulty")]
        public int LevelOfDifficulty { get; set; }
        [Column(Name = "WouldTakeTeacherAgain")]
        public bool WouldTakeTeacherAgain { get; set; }
        [Column(Name = "Tags")]
        public string Tags { get; set; }
        [Column(Name = "DateCreated")]
        public DateTime DateCreated { get; set; }
        [Column(Name = "Comment")]
        public string Comment { get; set; }
        }
    }
