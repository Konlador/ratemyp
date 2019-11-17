using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace RateMyP.Entities
    {
    public class RatingTag : IEquatable<RatingTag>
        {
        public Guid RatingId { get; set; }
        public Guid TagId { get; set; }

        [ForeignKey("TagId")]
        public Tag Tag { get; set; }

        public bool Equals(RatingTag other) =>
            other != null &&
            other.RatingId.Equals(RatingId) &&
            other.Tag.Equals(Tag);
        }
    }
