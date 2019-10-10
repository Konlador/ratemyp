using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RateMyP.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RateMyP.Server.Controllers
    {
    [Route("api/students")]
    [ApiController]
    public class StudentsController : ControllerBase
        {
        private readonly RateMyPDbContext m_context;

        public StudentsController(RateMyPDbContext context)
            {
            m_context = context;
            }

        // GET: api/Students
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
            {
            return await m_context.Students.ToListAsync();
            }

        // GET: api/Students/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudent(Guid id)
            {
            var student = await m_context.Students.FindAsync(id);

            if (student == null)
                return NotFound();

            return student;
            }

        // PUT: api/Students/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudent(Guid id, Student student)
            {
            if (id != student.Id)
                return BadRequest();

            m_context.Entry(student).State = EntityState.Modified;

            try
                {
                await m_context.SaveChangesAsync();
                }
            catch (DbUpdateConcurrencyException)
                {
                if (!StudentExists(id))
                    return NotFound();
                else
                    throw;
                }

            return NoContent();
            }

        // POST: api/Students
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Student>> PostStudent(Student student)
            {
            m_context.Students.Add(student);
            await m_context.SaveChangesAsync();

            return CreatedAtAction("GetStudent", new { id = student.Id }, student);
            }

        // DELETE: api/Students/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Student>> DeleteStudent(Guid id)
            {
            var student = await m_context.Students.FindAsync(id);
            if (student == null)
                return NotFound();

            m_context.Students.Remove(student);
            await m_context.SaveChangesAsync();

            return student;
            }

        private bool StudentExists(Guid id)
            {
            return m_context.Students.Any(e => e.Id == id);
            }
        }
    }
