using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RateMyP.WebApp.Models.Leaderboard;
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
        public ActionResult<IEnumerable<TeacherLeaderboardEntry>> GetTopTeachersAllTime()
            {
            var entries = m_context.TeacherLeaderboard
                            .Include(x => x.Teacher)
                            .OrderBy(x => x.AllTimePosition)
                            .Take(20);
            return Ok(entries);
            }

        [HttpGet("teachers/year")]
        public ActionResult<IEnumerable<object>> GetTopTeachersThisYear()
            {
            var entries = m_context.TeacherLeaderboard
                                   .Include(x => x.Teacher)
                                   .OrderBy(x => x.ThisYearPosition)
                                   .Take(20);
            return Ok(entries);
            }

        [HttpGet("teacher={teacherId}")]
        public async Task<ActionResult<LeaderboardEntry>> GetTeacherEntryAsync(Guid teacherId)
            {
            var entry = await m_context.TeacherLeaderboard
                                       .Include(x => x.Teacher)
                                       .FirstAsync(x => x.TeacherId.Equals(teacherId));
            if (entry == null)
                return NotFound();

            return entry;
            }

        [HttpGet("courses/global")]
        public ActionResult<IEnumerable<object>> GetCourseEntriesAllTime()
            {
            var entries = m_context.CourseLeaderboard
                                   .Include(x => x.Course)
                                   .OrderBy(x => x.AllTimePosition)
                                   .Take(20);
            return Ok(entries);
            }

        [HttpGet("courses/year")]
        public ActionResult<IEnumerable<object>> GetCourseEntriesThisYear()
            {
            var entries = m_context.CourseLeaderboard
                                   .Include(x => x.Course)
                                   .OrderBy(x => x.ThisYearPosition)
                                   .Take(20);
            return Ok(entries);
            }

        [HttpGet("course={courseId}")]
        public async Task<ActionResult<LeaderboardEntry>> GetCourseEntryAsync(Guid courseId)
            {
            var entry = await m_context.CourseLeaderboard
                                       .Include(x => x.Course)
                                       .FirstAsync(x => x.CourseId.Equals(courseId));
            if (entry == null)
                return NotFound();

            return entry;
            }
        }
    }
