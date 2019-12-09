using System;

namespace RateMyP.WebApp.Models
    {
    public class TeacherBadge
        {
        public Guid Id { get; set; }
        public Guid TeacherId { get; set; }
        public Guid BadgeId { get; set; }
        public DateTime DateObtained { get; set; }

        }
    }