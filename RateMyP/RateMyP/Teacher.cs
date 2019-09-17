using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RateMyP
    {
    public enum AcademicRank
        {
        Lecturer,
        Professor
        }

    public class Teacher
        {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public AcademicRank Rank { get; set; }
        }
    }
