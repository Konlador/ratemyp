using System;
using System.Collections.Generic;

namespace RateMyP.WebApp.Statistics
    {
    public struct DateMark
        {
        public DateTime Date;
        public double Mark;
        }

    public class TeacherStatistics
        {
        public Guid Id { get; set; }
        public Guid TeacherId { get; set; }
        public double AverageMark { get; set; }
        public List<DateMark> AverageMarks { get; set; }
        public double AverageLevelOfDifficulty { get; set; }
        public double WouldTakeAgainRatio { get; set; }
        }
    }
