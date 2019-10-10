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

        // PUT: api/Teachers/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTeacher(Guid id, Teacher teacher)
            {
            if (id != teacher.Id)
                return BadRequest();

            m_context.Entry(teacher).State = EntityState.Modified;

            try
                {
                await m_context.SaveChangesAsync();
                }
            catch (DbUpdateConcurrencyException)
                {
                if (!TeacherExists(id))
                    return NotFound();
                else
                    throw;
                }

            return NoContent();
            }

        // POST: api/Teachers
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Teacher>> PostTeacher(Teacher teacher)
            {
            m_context.Teachers.Add(teacher);
            await m_context.SaveChangesAsync();

            return CreatedAtAction("GetTeacher", new { id = teacher.Id }, teacher);
            }

        // DELETE: api/Teachers/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Teacher>> DeleteTeacher(Guid id)
            {
            var teacher = await m_context.Teachers.FindAsync(id);
            if (teacher == null)
                return NotFound();

            m_context.Teachers.Remove(teacher);
            await m_context.SaveChangesAsync();

            return teacher;
            }

        private bool TeacherExists(Guid id)
            {
            return m_context.Teachers.Any(e => e.Id == id);
            }
        }
    }
