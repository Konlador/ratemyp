﻿using System;
using System.Data.Linq.Mapping;
using static RateMyP.Constants;

namespace RateMyP.Entities
    {
    [Table(Name = TABLE_STUDENTS)]
    public class Student
        {
        [Column(IsPrimaryKey = true, Name = "Id")]
        public Guid Id { get; set; }
        [Column(Name = "Name")]
        public string Name { get; set; }
        [Column(Name = "Surname")]
        public string Surname { get; set; }
        [Column(Name = "Studies")]
        public string Studies { get; set; }
        [Column(Name = "Faculty")]
        public string Faculty { get; set; }
        [Column(Name = "Description")]
        public string Description { get; set; }
        }
    }
