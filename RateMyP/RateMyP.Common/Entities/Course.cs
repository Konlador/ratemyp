﻿using System;
using System.ComponentModel.DataAnnotations;

namespace RateMyP.Entities
    {
    public enum CourseType
        {
        None,
        Optional,
        Compulsory,
        Complimentary,
        BUS
        }

    public class Course
        {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public CourseType CourseType { get; set; }
        public int Credits { get; set; }
        public string Faculty { get; set; }
        }
    }