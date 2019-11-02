using System;

namespace RateMyP.WebApp.Models
    {
    public class Student : IEquatable<Student>
        {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Studies { get; set; }

        public bool Equals(Student other) =>
            other != null &&
            other.Id.Equals(Id) &&
            other.FirstName.Equals(FirstName) &&
            other.LastName.Equals(LastName) &&
            other.Studies.Equals(Studies);
        }
    }
