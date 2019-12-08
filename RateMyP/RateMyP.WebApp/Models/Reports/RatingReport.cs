using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace RateMyP.WebApp.Models.Reports
    {
    public class RatingReport : Report, IEquatable<RatingReport>
        {
        [ForeignKey("Rating")]
        public Guid RatingId { get; set; }
        public virtual Rating Rating { get; set; }

        public bool Equals(RatingReport other) =>
            other != null &&
            base.Equals(other) &&
            other.RatingId.Equals(RatingId);

        public override bool Equals(object obj)
            {
            return obj is RatingReport report && Equals(report);
            }

        public override int GetHashCode()
            {
            return base.GetHashCode() ^
                   RatingId.GetHashCode();
            }
        }

    public class RatingReportDto
        {
        public Guid? Id { get; set; }
        public Guid RatingId { get; set; }
        public RatingDto Rating { get; set; }
        public string StudentId { get; set; }
        public DateTime DateCreated { get; set; }
        public string Reason { get; set; }
        }
    }
