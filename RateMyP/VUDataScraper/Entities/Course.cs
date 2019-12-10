using System;

namespace VUDataScraper
    {
    internal class Course
        {
        public Guid Id { get; set; }
        public string Link { get; set; }
        public string Name { get; set; }
        public string CourseType { get; set; }
        public string Faculty { get; set; }
        }
    }