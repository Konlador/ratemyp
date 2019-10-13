using System;

namespace RateMyP.Entities
    {
    public class TeacherTag : IEquatable<TeacherTag>
        {
        public Guid Id { get; set; }
        public Guid TeacherId { get; set; }
        public Tag Tag { get; set; }
        public int Count { get; set; }

        public bool Equals(TeacherTag other) =>
            other != null &&
            other.Id.Equals(Id) &&
            other.TeacherId.Equals(TeacherId) &&
            other.Tag.Equals(Tag) &&
            other.Count.Equals(Count);
        }
    }
