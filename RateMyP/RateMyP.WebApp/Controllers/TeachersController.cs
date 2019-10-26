using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RateMyP.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RateMyP.WebApp.Controllers
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
            return await m_context.Teachers.Include(x => x.Activities).ToListAsync();
            }

        // GET: api/Teachers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Teacher>> GetTeacher(Guid id)
            {
            var teacher = await m_context.Teachers.Include(x => x.Activities)
                .FirstOrDefaultAsync(i => i.Id.Equals(id));

            if (teacher == null)
                return NotFound();

            return teacher;
            }

        private bool TeacherExists(Guid id)
            {
            return m_context.Teachers.Any(e => e.Id.Equals(id));
            }
        }
    }
