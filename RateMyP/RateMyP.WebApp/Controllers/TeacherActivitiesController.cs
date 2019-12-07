using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RateMyP.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RateMyP.WebApp.Controllers
    {
    public interface ITeacherActivitiesController
        {
        Task<ActionResult<IEnumerable<TeacherActivity>>> GetTeacherActivities(Guid teacherId);
        Task<ActionResult<IEnumerable<TeacherActivity>>> GetCourseTeacherActivities(Guid courseId);
        }

    [Route("api/teacheractivities")]
    [ApiController]
    public class TeacherActivitiesController : ControllerBase, ITeacherActivitiesController
        {
        private readonly RateMyPDbContext m_context;

        public TeacherActivitiesController(RateMyPDbContext context)
            {
            m_context = context;
            }

        [HttpGet("teacher={teacherId}")]
        public async Task<ActionResult<IEnumerable<TeacherActivity>>> GetTeacherActivities(Guid teacherId)
            {
            return await m_context.TeacherActivities
                                  .Where(x => x.TeacherId.Equals(teacherId))
                                  .ToListAsync();
            }

        [HttpGet("course={courseId}")]
        public async Task<ActionResult<IEnumerable<TeacherActivity>>> GetCourseTeacherActivities(Guid courseId)
            {
            return await m_context.TeacherActivities
                                  .Where(x => x.CourseId.Equals(courseId))
                                  .ToListAsync();
            }
        }
    }
