using System;
using System.Collections.Generic;

namespace RateMyP.WebApp.Statistics
    {
    public class CourseStatistics
        {
        public Guid Id { get; set; }
        public Guid CourseId { get; set; }
        public double AverageMark { get; set; }
        public List<DateMark> AverageMarks { get; set; }
        public double AverageLevelOfDifficulty { get; set; }
        public double WouldTakeAgainRatio { get; set; }
        }
    }