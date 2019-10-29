﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RateMyP.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RateMyP.WebApp.Helpers;

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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Teacher>>> GetTeachers()
            {
            return await m_context.Teachers.ToListAsync();
            }
        /*
    [HttpGet("{startIndex}")]
    public async Task<ActionResult<IEnumerable<Teacher>>> GetTeachersIndexed(int startIndex)
        {
        return await m_context.Teachers.Skip(startIndex).Take(5).ToListAsync();
        }*/

        [HttpGet("{id}")]
        public async Task<ActionResult<Teacher>> GetTeacher(Guid id)
            {
            var teacher = await m_context.Teachers
                .FirstOrDefaultAsync(i => i.Id.Equals(id));

            if (teacher == null)
                return NotFound();

            return teacher;
            }

        [HttpGet("search/{searchString}")]
        public async Task<ActionResult<IEnumerable<Teacher>>> GetSearchedTeachers(string searchString)
            {
            var search = searchString.ToLower().Denationalize();
            return await m_context.Teachers
                                         .Where(x => (x.FirstName + " " + x.LastName)
                                                     .ToLower()
                                                     .Denationalize()
                                                     .Contains(search))
                                         .ToListAsync();
            }

        private bool TeacherExists(Guid id)
            {
            return m_context.Teachers.Any(e => e.Id.Equals(id));
            }
        }
    }