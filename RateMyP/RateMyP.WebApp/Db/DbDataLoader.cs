using System;
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
            //LoadBadgesToDb();
            //LoadTeachersToDb();
            //LoadCoursesToDb();
            //LoadTeacherActivitiesToDb();
            //LoadStudentsToDb();
            //LoadTagsToDb();
            //LoadMerchToDb();
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

        private void LoadBadgesToDb()
            {
            var badges = new List<string> {"The Very Best", "Runner-Up", "Strong Contender", "The Best", "Beloved", "Full of Stars", "Hardmode", "Leader", "On Fire!"};
            var files = new List<string>
                {
                "the_very_best.png", "runner_up.png", "strong_contender.png", "the_best.png", "beloved.png", "full_of_stars.png", "hardmode.png",
                "leader.png", "on_fire.png"
                };

            for (int i = 0; i < badges.Count; i++)
                {
                using var stream =
                    File.OpenRead($"C:\\Uni\\ratemyp\\RateMyP\\RateMyP.WebApp\\Db\\SeedData\\badges\\{files[i]}");
                if (stream != null)
                    {
                    Upload(stream, badges[i], "", stream.Length, "png");
                    }
                }
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

        public void Upload(Stream fileStream, string name, string description, long size, string type)
            {
            try
                {
                var imageData = new byte[fileStream.Length];
                fileStream.Read(imageData, 0, imageData.Length);

                var badge = new Badge
                    {
                    Id = new Guid(),
                    Data = imageData,
                    Description = description,
                    Image = name,
                    Size = size,
                    Type = type,
                    };

                    m_context.Badges.Add(badge);
                    m_context.SaveChanges();
                }
            catch (Exception ex)
                {
                Console.WriteLine("Shit's fucked: " + ex.Message);
                }
            }
        }
    }
