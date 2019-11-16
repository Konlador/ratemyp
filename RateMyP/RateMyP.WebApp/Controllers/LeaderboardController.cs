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

        [HttpGet("teachers")]
        public async Task<ActionResult<IEnumerable<TeacherLeaderboardEntry>>> GetTeacherEntriesAllTime()
            {
            return await m_context.TeacherLeaderboardEntries.Take(20).OrderBy(entry => entry.AllTimePosition).ToListAsync();
            }

        [HttpGet("teacher={id}")]
        public async Task<ActionResult<TeacherLeaderboardEntry>> GetTeacherEntry(Guid id)
            {
            var entry = await m_context.TeacherLeaderboardEntries
                .FirstOrDefaultAsync(i => i.Id.Equals(id));

            if (entry == null)
                return NotFound();

            return entry;
            }

        }
    }
