using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RateMyP.WebApp.Models.Leaderboard
    {
    public class TeacherLeaderboardEntry : LeaderboardEntry
        {
        [Key]
        [ForeignKey("Teacher")]
        public Guid TeacherId { get; set; }
        public Teacher Teacher { get; set; }
        }
    }
