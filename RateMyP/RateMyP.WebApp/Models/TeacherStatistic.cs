using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace RateMyP.WebApp.Models
    {
    public class TeacherStatistic : IEquatable<TeacherStatistic>
        {
            public Guid Id { get; set; }
            public Guid TeacherId { get; set; }
            public double AverageMark { get; set; }
            public List<DateMark> AverageMarkList { get; set; }
            public double AverageLevelOfDifficulty { get; set; }
            public double WouldTakeAgainRatio { get; set; }
        public bool Equals(TeacherStatistic other) =>
        other != null &&
        other.Id.Equals(Id) &&
        other.TeacherId.Equals(TeacherId) &&
        other.AverageMark.Equals(AverageMark) &&
        other.AverageLevelOfDifficulty.Equals(AverageLevelOfDifficulty) &&
        other.WouldTakeAgainRatio.Equals(WouldTakeAgainRatio);
        }
    }
