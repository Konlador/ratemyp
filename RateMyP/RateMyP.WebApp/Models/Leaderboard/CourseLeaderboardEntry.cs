using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RateMyP.WebApp.Models.Leaderboard
    {
    public class CourseLeaderboardEntry : LeaderboardEntry
        {
        [Key]
        [ForeignKey("Course")]
        public Guid CourseId { get; set; }
        public Course Course { get; set; }
        }
    }
