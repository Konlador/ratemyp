using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RateMyP.WebApp.Models
    {
    public class Student : IEquatable<Student>
        {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; }
        public string? Studies { get; set; }

        public bool Equals(Student other) =>
            other != null &&
            other.Id.Equals(Id) &&
            other.Studies.Equals(Studies);
        }
    }
