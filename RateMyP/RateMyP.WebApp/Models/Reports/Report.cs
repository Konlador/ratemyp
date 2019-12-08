using System;

namespace RateMyP.WebApp.Models.Reports
    {
    public abstract class Report : IEquatable<Report>
        {
        public Guid Id { get; set; }
        public string StudentId { get; set; }
        public DateTime DateCreated { get; set; }
        public string Reason { get; set; }

        public bool Equals(Report other) =>
            other != null &&
            other.Id.Equals(Id) &&
            other.StudentId.Equals(StudentId) &&
            other.DateCreated.Equals(DateCreated) &&
            other.Reason.Equals(Reason);

        public override bool Equals(object obj)
            {
            return obj is Report report && Equals(report);
            }

        public override int GetHashCode()
            {
            return Id.GetHashCode() ^
                   StudentId.GetHashCode() ^
                   DateCreated.GetHashCode() ^
                   Reason.GetHashCode();
            }
        }
    }
