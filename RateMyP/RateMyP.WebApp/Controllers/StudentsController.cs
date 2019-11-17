using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RateMyP.WebApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RateMyP.WebApp.Controllers
    {
    public interface IStudentsController
        {
        Task<ActionResult<IEnumerable<Student>>> GetStudents();
        Task<ActionResult<Student>> GetStudent(string id);
        Task<ActionResult<Student>> PostStudent(Student student);
        }

    [Route("api/students")]
    [ApiController]
    public class StudentsController : ControllerBase, IStudentsController
        {
        private readonly RateMyPDbContext m_context;

        public StudentsController(RateMyPDbContext context)
            {
            m_context = context;
            }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
            {
            return await m_context.Students.ToListAsync();
            }

        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudent(string id)
            {
            var student = await m_context.Students.FindAsync(id);

            if (student == null)
                {
                student = new Student { Id = id };
                m_context.Students.Add(student);
                await m_context.SaveChangesAsync();
                }

            return student;
            }

        [HttpPost]
        public async Task<ActionResult<Student>> PostStudent(Student student)
            {
            m_context.Students.Add(student);
            await m_context.SaveChangesAsync();

            return CreatedAtAction("GetStudent", new { id = student.Id }, student);
            }
        }
    }
