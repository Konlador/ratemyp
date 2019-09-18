using System;
using System.Collections.Generic;

namespace RateMyP.Entities
    {
    [Serializable]
    public class Rating
        {
        public Guid Id { get; set; }
        public Guid TeacherId { get; set; }
        public Guid StudentId { get; set; }
        public int OverallMark { get; set; }
        public int LevelOfDifficulty { get; set; }
        public bool WouldTakeTeacherAgain { get; set; }
        public List<string> Tags { get; set; }
        public string Comment { get; set; }
        }
    }
