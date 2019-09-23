using System;
using System.Data.Linq.Mapping;
using static RateMyP.Constants;

namespace RateMyP.Entities
    {
    public enum CourseType
        {
        Standard,
        BUS
        }

    [Table(Name = TABLE_COURSES)]
    public class Course
        {
        [Column(IsPrimaryKey = true, Name = "Id")]
        public Guid Id { get; set; }
        [Column(Name = "Name")]
        public string Name { get; set; }
        [Column(Name = "Type")]
        public CourseType Type { get; set; }
        [Column(Name = "Credits")]
        public int Credits { get; set; }
        [Column(Name = "Faculty")]
        public string Faculty { get; set; }
        }
    }
