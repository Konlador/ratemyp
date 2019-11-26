using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RateMyP.WebApp.Models
    {
    public class CustomStarRating
        {
        public Guid Id { get; set; }
        public Guid TeacherId { get; set; }
        public DateTime DateCreated { get; set; }
        public string? StudentId { get; set; }
        public int ThumbUps { get; set; }
        public int ThumbDowns { get; set; }

        public bool Equals(Rating other) =>
            other != null &&
            other.Id.Equals(Id) &&
            other.TeacherId.Equals(TeacherId) &&
            other.StudentId.Equals(StudentId) &&
            other.DateCreated.Equals(DateCreated);
        }
    }
