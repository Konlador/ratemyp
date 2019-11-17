using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RateMyP.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RateMyP.WebApp.Controllers
    {
    [Route("api/leaderboard/")]
    [ApiController]
    public class LeaderboardController : ControllerBase
        {
        private readonly RateMyPDbContext m_context;

        public LeaderboardController(RateMyPDbContext context)
            {
            m_context = context;
            }

        [HttpGet("teachers/all")]
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
                }).OrderBy(e => e.AllTimePosition).Take(20).ToListAsync();
            //return await m_context.TeacherLeaderboard.Take(20).OrderBy(entry => entry.AllTimePosition).ToListAsync();
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
                }).OrderBy(e => e.ThisYearPosition).Take(20).ToListAsync();
            }

        [HttpGet("teacher={id}")]
        public async Task<ActionResult<LeaderboardEntry>> GetTeacherEntry(Guid id)
            {
            var entry = await m_context.Leaderboard
                .FirstOrDefaultAsync(i => i.Id.Equals(id));

            if (entry == null)
                return NotFound();

            return entry;
            }

        }
    }
