using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using RateMyP.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using RateMyP.WebApp.Helpers;
using RateMyP.WebApp.Models.Reports;

namespace RateMyP.WebApp.Controllers
    {
    public interface IReportsController
        {
        Task<ActionResult<IEnumerable<RatingReport>>> GetRatingReportsAsync();
        Task<ActionResult<IEnumerable<RatingReport>>> GetRatingReportsByRatingAsync(Guid ratingId);
        Task<ActionResult<RatingReport>> PostRatingReportAsync([FromBody] JObject data);
        Task<ActionResult<IEnumerable<CustomStarReport>>> GetCustomStarReportsAsync();
        Task<ActionResult<IEnumerable<CustomStarReport>>> GetCustomStarReportsByCustomStarAsync(Guid customStarId);
        Task<ActionResult<CustomStarReport>> PostCustomStarReportAsync([FromBody] JObject data);
        }

    [Route("api/reports")]
    [ApiController]
    public class ReportsController : ControllerBase
        {
        private readonly RateMyPDbContext m_context;

        public ReportsController(RateMyPDbContext context)
            {
            m_context = context;
            }

        #region Rating
        [Authorize]
        [HttpGet("rating")]
        public ActionResult<IEnumerable<RatingReportDto>> GetRatingReports()
            {
            var ratingReports = m_context.RatingReports.SelectRatingReportDto();
            return Ok(ratingReports);
            }

        [Authorize]
        [HttpGet("rating/{id}")]
        public ActionResult<RatingReportDto> GetRatingReport(Guid id)
            {
            var ratingReport = m_context.RatingReports
                                         .Include(rr => rr.Rating)
                                         .ThenInclude(r => r.Tags)
                                         .ThenInclude(rt => rt.Tag)
                                         .Single(rr => rr.Id.Equals(id));
            if (ratingReport == null)
                return NotFound();

            return Ok(ratingReport.SelectRatingReportDto());
            }

        [Authorize]
        [HttpGet("rating={ratingId}")]
        public async Task<ActionResult<IEnumerable<RatingReport>>> GetRatingReportsByRatingAsync(Guid ratingId)
            {
            return await m_context.RatingReports.Where(x => x.RatingId.Equals(ratingId)).ToListAsync();
            }

        [Authorize]
        [HttpPost("rating")]
        public async Task<ActionResult<RatingReportDto>> PostRatingReportAsync(RatingReportDto ratingReportDto)
            {
            var identity = (ClaimsIdentity)User.Identity;
            var studentId = identity.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;
            if (studentId == null || m_context.Students.Find(studentId) == null)
                return NotFound("Student not found");

            var report = new RatingReport
                {
                Id = Guid.NewGuid(),
                DateCreated = DateTime.Now,
                StudentId = studentId,
                RatingId = ratingReportDto.RatingId,
                Reason = ratingReportDto.Reason
                };
            m_context.RatingReports.Add(report);
            await m_context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetRatingReport), new { id = report.Id });
            }

        [Authorize]
        [HttpDelete("rating/{id}")]
        public async Task<IActionResult> DeleteRatingReportAsync(Guid id)
            {
            var ratingReport = await m_context.RatingReports.FindAsync(id);
            if (ratingReport == null)
                return NotFound();

            m_context.RatingReports.Remove(ratingReport);
            await m_context.SaveChangesAsync();

            return Ok();
            }
        #endregion

        #region CustomStar
        [Authorize]
        [HttpGet("custom-star")]
        public ActionResult<IEnumerable<CustomStarReport>> GetCustomStarReports()
            {
            var customStarReports = m_context.CustomStarReports
                                             .Include(cr => cr.CustomStar);
            return Ok(customStarReports);
            }

        [Authorize]
        [HttpGet("custom-star/{id}")]
        public ActionResult<CustomStarReport> GetCustomStarReport(Guid id)
            {
            var report = m_context.CustomStarReports
                                        .Include(cr => cr.CustomStar)
                                        .Single(rr => rr.Id.Equals(id));
            if (report == null)
                return NotFound();

            return Ok(report);
            }

        [Authorize]
        [HttpPost("custom-star")]
        public async Task<ActionResult<CustomStarReport>> PostCustomStarReportAsync(CustomStarReportDto customStarReportDto)
            {
            var identity = (ClaimsIdentity)User.Identity;
            var studentId = identity.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;
            if (studentId == null || m_context.Students.Find(studentId) == null)
                return NotFound("Student not found");

            var report = new CustomStarReport
                {
                Id = Guid.NewGuid(),
                DateCreated = DateTime.Now,
                StudentId = studentId,
                CustomStarId = customStarReportDto.CustomStarId,
                Reason = customStarReportDto.Reason
                };
            m_context.CustomStarReports.Add(report);
            await m_context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCustomStarReport), new { id = report.Id });
            }

        [Authorize]
        [HttpDelete("custom-star/{id}")]
        public async Task<IActionResult> DeleteCustomStarReportAsync(Guid id)
            {
            var report = await m_context.CustomStarReports.FindAsync(id);
            if (report == null)
                return NotFound();

            m_context.CustomStarReports.Remove(report);
            await m_context.SaveChangesAsync();

            return Ok();
            }
        #endregion
        }
    }
