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

        private bool StudentExists(Guid id)
            {
            return m_context.Students.Any(e => e.Id == id);
            }
        }
    }
