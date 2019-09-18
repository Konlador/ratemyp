using System;

namespace RateMyP.Entities
    {
    public enum AcademicRank
        {
        Lecturer,
        Professor
        }

    [Serializable]
    public class Teacher
        {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public AcademicRank Rank { get; set; }
        }
    }
