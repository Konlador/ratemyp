using System;

namespace RateMyP.WebApp.Models
    {
    public enum CourseType
        {
        None,
        Optional,
        Compulsory,
        Complimentary,
        BUS
        }

    public class Course : IEquatable<Course>
        {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public CourseType CourseType { get; set; }
        public int Credits { get; set; }
        public string Faculty { get; set; }

        public bool Equals(Course other) =>
            other != null &&
            other.Id.Equals(Id) &&
            other.Name.Equals(Name) &&
            other.CourseType.Equals(CourseType) &&
            other.Credits.Equals(Credits) &&
            other.Faculty.Equals(Faculty);
        }
    }
