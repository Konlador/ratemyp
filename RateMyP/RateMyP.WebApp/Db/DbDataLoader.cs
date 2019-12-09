using CsvHelper;
using RateMyP.WebApp.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace RateMyP.WebApp.Db
    {
    public class DbDataLoader
        {
        private readonly RateMyPDbContext m_context;

        public DbDataLoader(RateMyPDbContext context)
            {
            m_context = context;
            }

        public void LoadDataToDb()
            {
            LoadTeachersToDb();
            LoadCoursesToDb();
            LoadTeacherActivitiesToDb();
            LoadStudentsToDb();
            LoadTagsToDb();
            LoadMerchToDb();
            }

        private void LoadTeachersToDb()
            {
            var teachers = ParseEntitiesFromCsv<Teacher>("teachers.csv");
            m_context.Teachers.AddRange(teachers);
            m_context.SaveChanges();
            }

        private void LoadCoursesToDb()
            {
            var courses = ParseEntitiesFromCsv<Course>("courses.csv");
            m_context.Courses.AddRange(courses);
            m_context.SaveChanges();
            }

        private void LoadTeacherActivitiesToDb()
            {
            var teacherActivities = ParseEntitiesFromCsv<TeacherActivity>("teacher_activities.csv");
            m_context.TeacherActivities.AddRange(teacherActivities);
            m_context.SaveChanges();
            }

        private void LoadStudentsToDb()
            {
            var students = ParseEntitiesFromCsv<Student>("students.csv");
            m_context.Students.AddRange(students);
            m_context.SaveChanges();
            }

        private void LoadTagsToDb()
            {
            var tags = ParseEntitiesFromCsv<Tag>("tags.csv");
            m_context.Tags.AddRange(tags);
            m_context.SaveChanges();
            }

        private void LoadMerchToDb()
            {
            var merchandises = ParseEntitiesFromCsv<Merchandise>("merch.csv");
            m_context.Merchandises.AddRange(merchandises);
            m_context.SaveChanges();
            }

        private static List<T> ParseEntitiesFromCsv<T>(string fileName)
            {
            var assembly = Assembly.GetExecutingAssembly();
            using var stream = assembly.GetManifestResourceStream($"RateMyP.WebApp.Db.SeedData.{fileName}");
            using var reader = new StreamReader(stream);
            var csvReader = new CsvReader(reader);
            csvReader.Configuration.MissingFieldFound = null;
            csvReader.Configuration.HeaderValidated = null;
            return csvReader.GetRecords<T>().ToList();
            }
        }
    }
