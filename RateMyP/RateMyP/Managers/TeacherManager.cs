using RateMyP.Entities;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;

namespace RateMyP.Managers
    {
    public interface ITeacherManager
        {
        List<Teacher> GetAll();
        Teacher GetById(Guid teacherId);
        void Add(Teacher teacher);
        }

    public class TeacherManager : ITeacherManager
        {
        public List<Teacher> GetAll()
            {
            using (var dbConnection = SQLDbConnection.CreateToDb())
            using (var dataContext = new DataContext(dbConnection.Connection))
                {
                return dataContext.GetTable<Teacher>().ToList();
                }
            }

        public Teacher GetById(Guid teacherId)
            {
            return GetAll().FirstOrDefault(teacher => teacher.Id.Equals(teacherId));
            }

        public void Add(Teacher teacher)
            {
            using (var dbConnection = SQLDbConnection.CreateToDb())
            using (var dataContext = new DataContext(dbConnection.Connection))
                {
                var teachers = dataContext.GetTable<Teacher>();
                teachers.InsertOnSubmit(teacher);
                dataContext.SubmitChanges();
                }
            }
        }
    }
