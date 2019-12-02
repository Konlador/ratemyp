using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RateMyP.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RateMyP.WebApp.Controllers
    {
    [Route("api/leaderboard")]
    [ApiController]
    public class LeaderboardController : ControllerBase
        {
        private readonly RateMyPDbContext m_context;

        public LeaderboardController(RateMyPDbContext context)
            {
            m_context = context;
            }

        [HttpGet("teachers/global")]
        public async Task<ActionResult<IEnumerable<object>>> GetTeacherEntriesAllTime()
            {
            return await m_context.Leaderboard.Join(m_context.Teachers, e => e.Id, t => t.Id, (e, t) => new
                {
                t.Id,
                e.EntryType,
                Name = t.FirstName + " " + t.LastName,
                e.AllTimePosition,
                e.AllTimeRatingCount,
                e.AllTimeAverage,
                e.ThisYearPosition,
                e.ThisYearRatingCount,
                e.ThisYearAverage
                })
                .Where(e => e.EntryType == EntryType.Teacher)
                .OrderBy(e => e.AllTimePosition)
                .Take(20).ToListAsync();
            }

        [HttpGet("teachers/year")]
        public async Task<ActionResult<IEnumerable<object>>> GetTeacherEntriesThisYear()
            {
            return await m_context.Leaderboard.Join(m_context.Teachers, e => e.Id, t => t.Id, (e, t) => new
                {
                t.Id,
                e.EntryType,
                Name = t.FirstName + " " + t.LastName,
                e.AllTimePosition,
                e.AllTimeRatingCount,
                e.AllTimeAverage,
                e.ThisYearPosition,
                e.ThisYearRatingCount,
                e.ThisYearAverage
                })
                .Where(e => e.EntryType == EntryType.Teacher)
                .OrderBy(e => e.ThisYearPosition)
                .Take(20).ToListAsync();
            }

        [HttpGet("teacher={id}")]
        public async Task<ActionResult<LeaderboardEntry>> GetTeacherEntry(Guid id)
            {
            var entry = await m_context.Leaderboard
                .Where(e => e.EntryType == EntryType.Teacher)
                .FirstOrDefaultAsync(i => i.Id.Equals(id));

            if (entry == null)
                return NotFound();

            return entry;
            }

        [HttpGet("courses/global")]
        public async Task<ActionResult<IEnumerable<object>>> GetCourseEntriesAllTime()
            {
            return await m_context.Leaderboard.Join(m_context.Courses, e => e.Id, t => t.Id, (e, t) => new
                    {
                    t.Id,
                    e.EntryType,
                    t.Name,
                    e.AllTimePosition,
                    e.AllTimeRatingCount,
                    e.AllTimeAverage,
                    e.ThisYearPosition,
                    e.ThisYearRatingCount,
                    e.ThisYearAverage
                    })
                .Where(e => e.EntryType == EntryType.Course)
                .OrderBy(e => e.AllTimePosition)
                .Take(20).ToListAsync();
            }

        [HttpGet("courses/year")]
        public async Task<ActionResult<IEnumerable<object>>> GetCourseEntriesThisYear()
            {
            return await m_context.Leaderboard.Join(m_context.Courses, e => e.Id, t => t.Id, (e, t) => new
                    {
                    t.Id,
                    e.EntryType,
                    t.Name,
                    e.AllTimePosition,
                    e.AllTimeRatingCount,
                    e.AllTimeAverage,
                    e.ThisYearPosition,
                    e.ThisYearRatingCount,
                    e.ThisYearAverage
                    })
                .Where(e => e.EntryType == EntryType.Course)
                .OrderBy(e => e.ThisYearPosition)
                .Take(20).ToListAsync();
            }

        [HttpGet("course={id}")]
        public async Task<ActionResult<LeaderboardEntry>> GetCourseEntry(Guid id)
            {
            var entry = await m_context.Leaderboard
                .Where(e => e.EntryType == EntryType.Course)
                .FirstOrDefaultAsync(i => i.Id.Equals(id));

            if (entry == null)
                return NotFound();

            return entry;
            }
        }
    }
