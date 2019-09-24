using System;
using System.Data.Linq.Mapping;
using static RateMyP.Constants;

namespace RateMyP.Entities
    {
    public enum AcademicRank
        {
        Lecturer,
        Professor
        }

    [Table(Name = TABLE_TEACHERS)]
    public class Teacher
        {
        [Column(IsPrimaryKey = true, Name = "Id")]
        public Guid Id { get; set; }
        [Column(Name = "Name")]
        public string Name { get; set; }
        [Column(Name = "Surname")]
        public string Surname { get; set; }
        [Column(Name = "Description")]
        public string Description { get; set; }
        [Column(Name = "Rank")]
        public AcademicRank Rank { get; set; }
        public string Faculty { get; set; }
        public string Studies { get; set; }
        public string Description { get; set; }

        }
    }
