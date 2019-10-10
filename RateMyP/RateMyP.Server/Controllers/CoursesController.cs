using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RateMyP.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RateMyP.Server.Controllers
    {
    [Route("api/courses")]
    [ApiController]
    public class CoursesController : ControllerBase
        {
        private readonly RateMyPDbContext m_context;

        public CoursesController(RateMyPDbContext context)
            {
            m_context = context;
            }

        // GET: api/Courses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Course>>> GetCourses()
            {
            return await m_context.Courses.ToListAsync();
            }

        // GET: api/Courses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Course>> GetCourse(Guid id)
            {
            var course = await m_context.Courses.FindAsync(id);

            if (course == null)
                return NotFound();

            return course;
            }

        // PUT: api/Courses/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCourse(Guid id, Course course)
            {
            if (id != course.Id)
                return BadRequest();

            m_context.Entry(course).State = EntityState.Modified;

            try
                {
                await m_context.SaveChangesAsync();
                }
            catch (DbUpdateConcurrencyException)
                {
                if (!CourseExists(id))
                    return NotFound();
                else
                    throw;
                }

            return NoContent();
            }

        // POST: api/Courses
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Course>> PostCourse(Course course)
            {
            m_context.Courses.Add(course);
            await m_context.SaveChangesAsync();

            return CreatedAtAction("GetCourse", new { id = course.Id }, course);
            }

        // DELETE: api/Courses/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Course>> DeleteCourse(Guid id)
            {
            var course = await m_context.Courses.FindAsync(id);
            if (course == null)
                return NotFound();

            m_context.Courses.Remove(course);
            await m_context.SaveChangesAsync();

            return course;
            }

        private bool CourseExists(Guid id)
            {
            return m_context.Courses.Any(e => e.Id == id);
            }
        }
    }
