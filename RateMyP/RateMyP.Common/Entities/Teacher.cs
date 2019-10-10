using System;
using System.ComponentModel.DataAnnotations;

namespace RateMyP.Entities
    {
    public enum AcademicRank
        {
        Lecturer,
        Professor
        }

    public class Teacher
        {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Description { get; set; }
        public string Rank { get; set; }
        public string Faculty { get; set; }
        }
    }
