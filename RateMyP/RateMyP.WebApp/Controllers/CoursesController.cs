using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RateMyP.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using RateMyP.WebApp.Helpers;

namespace RateMyP.WebApp.Controllers
    {
    public interface ICoursesController
        {
        Task<ActionResult<IEnumerable<Course>>> GetCourses();
        Task<ActionResult<Course>> GetCourse(Guid id);
        Task<ActionResult<IEnumerable<Course>>> GetCoursesIndexed(int startIndex);
        Task<ActionResult<IEnumerable<Course>>> GetTeacherCourses(Guid teacherId);
        }

    [Route("api/courses")]
    [ApiController]
    public class CoursesController : ControllerBase, ICoursesController
        {
        private readonly RateMyPDbContext m_context;

        public CoursesController(RateMyPDbContext context)
            {
            m_context = context;
            }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Course>>> GetCourses()
            {
            return await m_context.Courses.OrderBy(x => x.Name).ToListAsync();
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
            return await m_context.Courses.OrderBy(course => course.Name).Skip(startIndex).Take(20).ToListAsync();
            }

        [HttpGet("search={searchString}")]
        public async Task<ActionResult<IEnumerable<Course>>> GetSearchedCourses(string searchString)
            {
            var countToTake = int.Parse(ConfigurationManager.AppSettings["LoadedCoursesNumber"]);
            var search = searchString.ToLower().Denationalize();
            var courses = await m_context.Courses.ToListAsync();
            return courses.Where(x => x.Name
                                .ToLower()
                                .Denationalize()
                                .Contains(search))
                                .OrderBy(x => x.Name)
                                .Take(countToTake)
                          .ToList();
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
