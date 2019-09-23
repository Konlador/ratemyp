using RateMyP.Entities;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;

namespace RateMyP.Managers
    {
    public interface IStudentManager
        {
        List<Student> GetAll();
        Student GetById(Guid studentId);
        void Add(Student student);
        }

    public class StudentManager : IStudentManager
        {
        public List<Student> GetAll()
            {
            using (var dbConnection = SQLDbConnection.CreateToDb())
            using (var dataContext = new DataContext(dbConnection.Connection))
                {
                return dataContext.GetTable<Student>().ToList();
                }
            }

        public Student GetById(Guid studentId)
            {
            return GetAll().First(student => student.Id.Equals(studentId));
            }

        public void Add(Student student)
            {
            using (var dbConnection = SQLDbConnection.CreateToDb())
            using (var dataContext = new DataContext(dbConnection.Connection))
                {
                var students = dataContext.GetTable<Student>();
                students.InsertOnSubmit(student);
                dataContext.SubmitChanges();
                }
            }
        }
    }
