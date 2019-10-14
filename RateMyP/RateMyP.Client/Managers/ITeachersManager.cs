using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RateMyP.Entities;

namespace RateMyP.Client.Managers
    {
    public interface ITeachersManager
        {
        Task<List<Teacher>> GetAll();
        Task<List<TeacherActivity>> GetTeacherActivities(Guid teacherId);
        }
    }
