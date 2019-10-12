using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RateMyP.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RateMyP.Server.Controllers
    {
    [Route("api/teachers")]
    [ApiController]
    public class TeachersController : ControllerBase
        {
        private readonly RateMyPDbContext m_context;

        public TeachersController(RateMyPDbContext context)
            {
            m_context = context;
            }

        // GET: api/Teachers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Teacher>>> GetTeachers()
            {
            return await m_context.Teachers.ToListAsync();
            }

        // GET: api/Teachers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Teacher>> GetTeacher(Guid id)
            {
            var teacher = await m_context.Teachers.FindAsync(id);

            if (teacher == null)
                return NotFound();

            return teacher;
            }

        // GET: api/Teachers
        [HttpGet("{id}/select=TeacherActivities")]
        public async Task<ActionResult<IEnumerable<TeacherActivity>>> GetTeacherActivities(Guid teacherId)
            {
            return await m_context.TeacherActivities.Where(x => x.TeacherId == teacherId).ToListAsync();
            }

        private bool TeacherExists(Guid id)
            {
            return m_context.Teachers.Any(e => e.Id == id);
            }
        }
    }
