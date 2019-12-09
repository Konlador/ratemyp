using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace RateMyP.WebApp.Models.Reports
    {
    public class CustomStarReport : Report, IEquatable<CustomStarReport>
        {
        [ForeignKey("CustomStar")]
        public Guid CustomStarId { get; set; }
        public virtual CustomStar CustomStar { get; set; }

        public bool Equals(CustomStarReport other) =>
            other != null &&
            base.Equals(other) &&
            other.CustomStarId.Equals(CustomStarId);

        public override bool Equals(object obj)
            {
            return obj is CustomStarReport report && Equals(report);
            }

        public override int GetHashCode()
            {
            return base.GetHashCode() ^
                   CustomStarId.GetHashCode();
            }
        }

    public class CustomStarReportDto
        {
        public Guid? Id { get; set; }
        public Guid CustomStarId { get; set; }
        public CustomStar CustomStar { get; set; }
        public string StudentId { get; set; }
        public DateTime DateCreated { get; set; }
        public string Reason { get; set; }
        }
    }
