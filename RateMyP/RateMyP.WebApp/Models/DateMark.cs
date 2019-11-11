using System;
using System.Diagnostics.CodeAnalysis;

namespace RateMyP.WebApp.Models
    {
    public class DateMark : IEquatable<TeacherStatistic>
        {
        public DateTime Date { get; set; }
        public double Mark { get; set; }

        public bool Equals([AllowNull] TeacherStatistic other)
            {
            throw new NotImplementedException();
            }
        }
    }
