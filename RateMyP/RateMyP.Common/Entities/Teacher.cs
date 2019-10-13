using System;

namespace RateMyP.Entities
    {
    public enum AcademicRank
        {
        Lecturer,
        Professor
        }

    public class Teacher : IEquatable<Teacher>
        {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Description { get; set; }
        public string Rank { get; set; }
        public string Faculty { get; set; }

        public bool Equals(Teacher other) =>
            other != null &&
            other.Id.Equals(Id) &&
            other.FirstName.Equals(FirstName) &&
            other.LastName.Equals(LastName) &&
            other.Description.Equals(Description) &&
            other.Rank.Equals(Rank) &&
            other.FirstName.Equals(FirstName);
        }
    }
