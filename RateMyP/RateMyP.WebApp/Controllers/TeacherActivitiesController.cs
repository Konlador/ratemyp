using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RateMyP.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RateMyP.WebApp.Controllers
    {
    [Route("api/teacheractivities")]
    [ApiController]
    public class TeacherActivitiesController : ControllerBase
        {
        private readonly RateMyPDbContext m_context;

        public TeacherActivitiesController(RateMyPDbContext context)
            {
            m_context = context;
            }

        [HttpGet("{teacherId}")]
        public async Task<ActionResult<IEnumerable<TeacherActivity>>> GetTeacherActivities(Guid teacherId)
            {
            return await m_context.TeacherActivities
                                  .Where(x => x.TeacherId.Equals(teacherId))
                                  .ToListAsync();
            }
        }
    }
