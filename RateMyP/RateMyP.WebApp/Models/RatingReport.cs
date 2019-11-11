using System;

namespace RateMyP.WebApp.Models
    {
    public class RatingReport : IEquatable<RatingReport>
        {
        public Guid Id { get; set; }
        public Guid RatingId { get; set; }
        public Guid StudentId { get; set; }
        public DateTime DateCreated { get; set; }
        public string Reason { get; set; }

        public bool Equals(RatingReport other) =>
            other != null &&
            other.Id.Equals(Id) &&
            other.RatingId.Equals(RatingId) &&
            other.StudentId.Equals(StudentId) &&
            other.DateCreated.Equals(DateCreated) &&
            other.Reason.Equals(Reason);

        public override bool Equals(object obj)
            {
            if ((obj == null) || !(obj is RatingReport))
                {
                return false;
                }
            else
                {
                RatingReport r = (RatingReport)obj;
                return Equals(r);
                }
            }

        public override int GetHashCode()
            {
            return Id.GetHashCode() ^ RatingId.GetHashCode() ^ StudentId.GetHashCode() ^ DateCreated.GetHashCode() ^ Reason.GetHashCode();
            }
        }
    }
