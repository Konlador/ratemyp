using System;

namespace RateMyP.WebApp.Models
    {
    public class CustomStarReport : IEquatable<CustomStarReport>
        {
        public Guid Id { get; set; }
        public Guid CustomStarId { get; set; }
        public string StudentId { get; set; }
        public DateTime DateCreated { get; set; }
        public string Reason { get; set; }

        public bool Equals(CustomStarReport other) =>
            other != null &&
            other.Id.Equals(Id) &&
            other.CustomStarId.Equals(CustomStarId) &&
            other.StudentId.Equals(StudentId) &&
            other.DateCreated.Equals(DateCreated) &&
            other.Reason.Equals(Reason);

        public override bool Equals(object obj)
            {
            return obj is CustomStarReport report && Equals(report);
            }

        public override int GetHashCode()
            {
            return Id.GetHashCode() ^
                   CustomStarId.GetHashCode() ^
                   StudentId.GetHashCode() ^
                   DateCreated.GetHashCode() ^
                   Reason.GetHashCode();
            }
        }
    }
