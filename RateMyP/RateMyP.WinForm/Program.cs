using CsvHelper;
using RateMyP.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace RateMyP.WinForm
    {
    static class Program
        {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
            {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Forms.RateMyProfessor());
            }

        private static void LoadTeachersToDb()
            {
            using (var context = new RateMyPDbContext())
                {
                var teachers = ParseTeachersFromCsv();
                context.Teachers.AddRange(teachers);
                context.SaveChanges();
                }
            }

        private static void LoadCoursesToDb()
            {
            using (var context = new RateMyPDbContext())
                {
                var courses = ParseCoursesFromCsv();
                context.Courses.AddRange(courses);
                context.SaveChanges();
                }
            }

        private static void LoadTeacherActivitiesToDb()
            {
            using (var context = new RateMyPDbContext())
                {
                var teacherActivities = ParseTeacherActivitiesFromCsv();
                context.TeacherActivities.AddRange(teacherActivities);
                context.SaveChanges();
                }
            }

        private static void LoadStudentsToDb()
            {
            using (var context = new RateMyPDbContext())
                {
                var students = ParseStudentsFromCsv();
                context.Students.AddRange(students);
                context.SaveChanges();
                }
            }

        private static List<Teacher> ParseTeachersFromCsv()
            {
            var assembly = typeof(Program).Assembly;
            var path = Path.Combine(Path.GetFullPath(assembly.Location + @"..\..\..\..\..\.."), @"DbData\teachers.csv");

            var reader = new StreamReader(path);
            var csvReader = new CsvReader(reader);
            csvReader.Configuration.HasHeaderRecord = true;
            csvReader.Configuration.HeaderValidated = null;
            csvReader.Configuration.MissingFieldFound = null;


            return csvReader.GetRecords<Teacher>().ToList();
            }

        private static List<Course> ParseCoursesFromCsv()
            {
            var assembly = typeof(Program).Assembly;
            var path = Path.Combine(Path.GetFullPath(assembly.Location + @"..\..\..\..\..\.."), @"DbData\courses.csv");

            var reader = new StreamReader(path);
            var csvReader = new CsvReader(reader);
            csvReader.Configuration.HasHeaderRecord = true;

            return csvReader.GetRecords<Course>().ToList();
            }

        private static List<TeacherActivity> ParseTeacherActivitiesFromCsv()
            {
            var assembly = typeof(Program).Assembly;
            var path = Path.Combine(Path.GetFullPath(assembly.Location + @"..\..\..\..\..\.."), @"DbData\Teacher_Activity.csv");

            var reader = new StreamReader(path);
            var csvReader = new CsvReader(reader);
            csvReader.Configuration.HasHeaderRecord = true;
            csvReader.Configuration.HeaderValidated = null;
            csvReader.Configuration.MissingFieldFound = null;
            
            List<TeacherActivity> aList = new List<TeacherActivity>();

            foreach (var item in csvReader.GetRecords<TeacherActivityPeriferal>().ToList()) 
                {
                TeacherActivity ta = new TeacherActivity
                    {
                    Id = item.Id,
                    TeacherId = item.TeacherId,
                    CourseId = item.CourseId,
                    DateStarted = item.DateStarted,
                    LectureType = item.LectureType
                };
                aList.Add(ta);
                }
            return aList;
            }

        private static List<Student> ParseStudentsFromCsv()
            {
            var assembly = typeof(Program).Assembly;
            var path = Path.Combine(Path.GetFullPath(assembly.Location + @"..\..\..\..\..\.."), @"DbData\students.csv");

            var reader = new StreamReader(path);
            var csvReader = new CsvReader(reader);
            csvReader.Configuration.HasHeaderRecord = true;

            return csvReader.GetRecords<Student>().ToList();
            }
    }
    }
