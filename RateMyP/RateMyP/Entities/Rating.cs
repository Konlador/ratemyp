using System;
using System.ComponentModel.DataAnnotations;

namespace RateMyP.Entities
    {
    public class Rating
        {
        [Key]
        public Guid Id { get; set; }
        public Teacher Teacher { get; set; }
        public Student Student { get; set; }
        public Course Course { get; set; }
        public int OverallMark { get; set; }
        public int LevelOfDifficulty { get; set; }
        public bool WouldTakeTeacherAgain { get; set; }
        public string Tags { get; set; }
        public DateTime DateCreated { get; set; }
        public string Comment { get; set; }
        }
    }
