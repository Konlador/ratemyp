using System;

namespace RateMyP.Entities
    {
    public class Student : IEquatable<Student>
        {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Studies { get; set; }
        public string Faculty { get; set; }
        public string Description { get; set; }

        public bool Equals(Student other) =>
            other != null &&
            other.Id.Equals(Id) &&
            other.FirstName.Equals(FirstName) &&
            other.LastName.Equals(LastName) &&
            other.Studies.Equals(Studies) &&
            other.Faculty.Equals(Faculty) &&
            other.Description.Equals(Description);
        }
    }
