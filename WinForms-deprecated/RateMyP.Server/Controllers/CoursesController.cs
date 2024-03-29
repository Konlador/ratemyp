﻿using Microsoft.AspNetCore.Mvc;
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

        private bool CourseExists(Guid id)
            {
            return m_context.Courses.Any(e => e.Id == id);
            }
        }
    }
