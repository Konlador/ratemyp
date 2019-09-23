using RateMyP.Entities;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;

namespace RateMyP.Managers
    {
    public interface ICourseManager
        {
        List<Course> GetAll();
        Course GetById(Guid courseId);
        void Add(Course course);
        }

    public class CourseManager : ICourseManager
        {
        public List<Course> GetAll()
            {
            using (var dbConnection = SQLDbConnection.CreateToDb())
            using (var dataContext = new DataContext(dbConnection.Connection))
                {
                return dataContext.GetTable<Course>().ToList();
                }
            }

        public Course GetById(Guid courseId)
            {
            return GetAll().First(course => course.Id.Equals(courseId));
            }

        public void Add(Course course)
            {
            using (var dbConnection = SQLDbConnection.CreateToDb())
            using (var dataContext = new DataContext(dbConnection.Connection))
                {
                var courses = dataContext.GetTable<Course>();
                courses.InsertOnSubmit(course);
                dataContext.SubmitChanges();
                }
            }
        }
    }
