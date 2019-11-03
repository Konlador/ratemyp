using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RateMyP.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RateMyP.WebApp.Controllers
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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Course>>> GetCourses()
            {
            return await m_context.Courses.ToListAsync();
            }

        [HttpGet("{id}")]
        public async Task<ActionResult<Course>> GetCourse(Guid id)
            {
            var course = await m_context.Courses.FindAsync(id);

            if (course == null)
                return NotFound();

            return course;
            }

        [HttpGet("startIndex={startIndex}")]
        public async Task<ActionResult<IEnumerable<Course>>> GetCoursesIndexed(int startIndex)
            {
            return await m_context.Courses.Skip(startIndex).Take(20).ToListAsync();
            }

        [HttpGet("teacher={teacherId}")]
        public async Task<ActionResult<IEnumerable<Course>>> GetTeacherCourses(Guid teacherId)
            {
            var courseIds = await m_context.TeacherActivities
                .Where(x => x.TeacherId.Equals(teacherId))
                .Select(x => x.CourseId)
                .Distinct()
                .ToListAsync();
            var courses = await m_context.Courses.Where(r => courseIds.Contains(r.Id)).ToListAsync();
            return courses;
            }
        }
    }
