using System;
using System.ComponentModel.DataAnnotations;

namespace RateMyP.Entities
    {
    public class Student
        {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Studies { get; set; }
        public string Faculty { get; set; }
        public string Description { get; set; }
        }
    }
