using RateMyP.Db;
using RateMyP.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using static RateMyP.Constants;

namespace RateMyP.Managers
    {
    public interface IStudentManager
        {
        List<Student> GetAllStudents();
        Student GetStudent(Guid studentId);
        void AddStudent(Student student);
        }

    public class StudentManager : IStudentManager
        {
        private readonly ISQLDbConnection m_connection;

        public StudentManager(ISQLDbConnection connection)
            {
            m_connection = connection;
            }

        public List<Student> GetAllStudents()
            {
            var students = new List<Student>();
            using (var reader = m_connection.ExecuteQuery($"SELECT * FROM [{TABLE_STUDENTS}]"))
                {
                while (reader.Read())
                    {
                    var student = new Student
                        {
                        Id = reader.SafeGetGuid(PROPERTY_ID, Guid.Empty),
                        Name = reader.SafeGetString(PROPERTY_NAME),
                        Surname = reader.SafeGetString(PROPERTY_SURNAME),
                        Studies = reader.SafeGetString(PROPERTY_STUDIES),
                        Faculty = reader.SafeGetString(PROPERTY_FACULTY)
                        };
                    students.Add(student);
                    }
                }

            return students;
            }

        public Student GetStudent(Guid studentId)
            {
            return GetAllStudents().First(student => student.Id.Equals(studentId));
            }

        public void AddStudent(Student student)
            {
            m_connection.ExecuteNonQuery($"INSERT INTO [{TABLE_STUDENTS}]", student);
            }
        }
    }
