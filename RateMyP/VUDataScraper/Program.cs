using OpenQA.Selenium.Firefox;
using System.Collections.Generic;
namespace VUDataScraper
    {
    class Program
        {
        static void Main()
            {
            var directory = "C:\\Users\\jmal\\Desktop\\";
            var teachers = new List<Teacher>();
            var courses = new List<Course>();
            var teacherActivities = new List<TeacherActivity>();

            var options = new FirefoxOptions();
            options.AddArgument("--headless");
            var driver = new FirefoxDriver(options);
            var scraper = new Scraper(driver);

            scraper.SetLanguage("EN", driver);

            foreach (var faculty in Faculties)
                {
                var teacherList = scraper.GetTeachers(faculty);
                var courseList = scraper.GetCourses(faculty);

                //set third GetTeacherActivities parameter to a low number if a faster test is required. Otherwise the runtime will take more than an hour.
                teacherActivities.AddRange(scraper.GetTeacherActivities(teacherList, courseList, 10));

                teachers.AddRange(teacherList);
                courses.AddRange(courseList);
                }
            driver.Close();

            var writer = new CSVWriter(directory);
            writer.WriteTeachers(teachers);
            writer.WriteCourses(courses);
            writer.WriteTeacherActivities(teacherActivities);
            }
        public static string siteUrl = "https://tvarkarasciai.vu.lt/mif/employees/";
        public static string[] Faculties { get; } = { "MIF", "EF", "FSF", "FF", "CHGF", "VM", "FLF", "KF", "600000" };

        }
    }
