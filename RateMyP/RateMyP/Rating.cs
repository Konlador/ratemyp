using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RateMyP
    {
    public class Rating
        {
        public Guid TeacherGuid { get; set; }
        public int TeacherMark { get; set; }
        public int LevelOfDifficulty { get; set; }
        public bool WouldTakeTeacherAgain { get; set; }
        public List<string> Tags { get; set; }
        public string Comment { get; set; }
        }
    }
