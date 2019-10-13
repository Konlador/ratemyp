using CsvHelper;
using RateMyP.Entities;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace RateMyP.Server.Db
    {
    public class DbDataLoader
        {
        public static void LoadDataToDb()
            {
            LoadTeachersToDb();
            LoadCoursesToDb();
            LoadTeacherActivitiesToDb();
            LoadStudentsToDb();
            }

        private static void LoadTeachersToDb()
            {
            using var context = new RateMyPDbContext();
            var teachers = ParseEntitiesFromCsv<Teacher>("teachers.csv");
            context.Teachers.AddRange(teachers);
            context.SaveChanges();
            }

        private static void LoadCoursesToDb()
            {
            using var context = new RateMyPDbContext();
            var courses = ParseEntitiesFromCsv<Course>("courses.csv");
            context.Courses.AddRange(courses);
            context.SaveChanges();
            }

        private static void LoadTeacherActivitiesToDb()
            {
            using var context = new RateMyPDbContext();
            var teacherActivities = ParseEntitiesFromCsv<TeacherActivity>("teacher_activities.csv");
            teacherActivities.ForEach(x =>
                                          {
                                              x.Teacher = null;
                                              x.Course = null;
                                          });
            context.TeacherActivities.AddRange(teacherActivities);
            context.SaveChanges();
            }

        private static void LoadStudentsToDb()
            {
            using var context = new RateMyPDbContext();
            var students = ParseEntitiesFromCsv<Student>("students.csv");
            context.Students.AddRange(students);
            context.SaveChanges();
            }

        private static List<T> ParseEntitiesFromCsv<T>(string fileName)
            {
            var assembly = Assembly.GetExecutingAssembly();
            using var stream = assembly.GetManifestResourceStream($"RateMyP.Server.Db.SeedData.{fileName}");
            using var reader = new StreamReader(stream);
            var csvReader = new CsvReader(reader);
            csvReader.Configuration.MissingFieldFound = null;
            csvReader.Configuration.HeaderValidated = null;
            return csvReader.GetRecords<T>().ToList();
            }
        }
    }
