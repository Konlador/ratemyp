using System;
using System.Diagnostics.CodeAnalysis;

namespace RateMyP.WebApp.Models
    {
    public class DateMark : IEquatable<DateMark>
        {
        public DateTime Date { get; set; }
        public double Mark { get; set; }

        public bool Equals(DateMark other) =>
        other != null &&
        other.Date.Equals(Date) &&
        other.Mark.Equals(Mark);
        }
    }
