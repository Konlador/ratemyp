using RateMyP.Entities;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;

namespace RateMyP.Managers
    {
    public interface ITeacherActivityManager
        {
        List<TeacherActivity> GetAll();
        TeacherActivity GetById(Guid teacherActivityId);
        void Add(TeacherActivity teacherActivity);
        }

    public class TeacherActivityManager : ITeacherActivityManager
        {
        public List<TeacherActivity> GetAll()
            {
            using (var dbConnection = SQLDbConnection.CreateToDb())
            using (var dataContext = new DataContext(dbConnection.Connection))
                {
                return dataContext.GetTable<TeacherActivity>().ToList();
                }
            }

        public TeacherActivity GetById(Guid teacherActivityId)
            {
            return GetAll().First(teacherActivity => teacherActivity.Id.Equals(teacherActivityId));
            }

        public void Add(TeacherActivity teacherActivity)
            {
            using (var dbConnection = SQLDbConnection.CreateToDb())
            using (var dataContext = new DataContext(dbConnection.Connection))
                {
                var teacherActivities = dataContext.GetTable<TeacherActivity>();
                teacherActivities.InsertOnSubmit(teacherActivity);
                dataContext.SubmitChanges();
                }
            }
        }
    }
