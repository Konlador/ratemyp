using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RateMyP
    {
    public class TeacherManager
        {
        private DatabaseConnection m_database;

        public TeacherManager(DatabaseConnection databaseConnection)
            {
            m_database = databaseConnection;
            }

        public List<Teacher> GetTeachers()
            {
            return m_database.GetTeachers ();
            }

        public void AddTeacher(Teacher teacher)
            {
            m_database.SaveTeacher(teacher);
            }
        }
    }
