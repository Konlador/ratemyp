using System;
using System.ComponentModel.DataAnnotations;

namespace RateMyP.Entities
    {
    public enum CourseType
        {
        Standard,
        BUS
        }

    public class Course
        {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public CourseType Type { get; set; }
        public int Credits { get; set; }
        public string Faculty { get; set; }
        }
    }
