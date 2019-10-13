using System;
using System.ComponentModel.DataAnnotations;

namespace RateMyP.Entities
    {
    public class TeacherTag
        {
        public Guid Id { get; set; }
        public Teacher Teacher { get; set; }
        public Tag Tag { get; set; }
        public int Count { get; set; }
        }
    }
