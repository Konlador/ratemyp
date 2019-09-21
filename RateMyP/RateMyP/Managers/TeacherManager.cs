using RateMyP.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using RateMyP.Db;
using static RateMyP.Constants;

namespace RateMyP.Managers
    {
    public interface ITeacherManager
        {
        List<Teacher> GetAllTeachers();
        Teacher GetTeacher(Guid teacherId);
        void AddTeacher(Teacher teacher);
        }

    public class TeacherManager : ITeacherManager
        {
        private readonly ISQLDbConnection m_connection;

        public TeacherManager(ISQLDbConnection connection)
            {
            m_connection = connection;
            }

        public List<Teacher> GetAllTeachers()
            {
            var teachers = new List<Teacher>();
            using (var reader = m_connection.ExecuteQuery($"SELECT * FROM [{TABLE_TEACHERS}]"))
                {
                while (reader.Read())
                    {
                    var teacher = new Teacher
                    {
                        Id = reader.SafeGetGuid(PROPERTY_ID, Guid.Empty),
                        Name = reader.SafeGetString(PROPERTY_NAME),
                        Surname = reader.SafeGetString(PROPERTY_SURNAME),
                        Rank = reader.SafeGetEnum(PROPERTY_RANK, AcademicRank.Lecturer),
                        Faculty = reader.SafeGetString(PROPERTY_FACULTY)
                        };
                    teachers.Add(teacher);
                    }
                }

            return teachers;
            }

        public Teacher GetTeacher(Guid teacherId)
            {
            return GetAllTeachers().First(teacher => teacher.Id.Equals(teacherId));
            }

        public void AddTeacher(Teacher teacher)
            {
            m_connection.ExecuteNonQuery($"INSERT INTO [{TABLE_TEACHERS}]", teacher);
            }
        }
    }
