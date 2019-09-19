using System;

namespace RateMyP.Entities
    {
    [Serializable]
    public class Student
        {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Studies { get; set; }
        public string Faculty { get; set; }
        }
    }
