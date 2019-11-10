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
            public double AverageMarkByDate { get; set; }
            public List<Tuple<DateTime, double>> AverageMarkList { get; set; }
            public double AverageLevelOfDifficulty { get; set; }
            public double AverageWouldTakeAgainRatio { get; set; }
            public bool Equals([AllowNull] TeacherStatistic other)
            {
            throw new NotImplementedException();
            }
        }
    }
