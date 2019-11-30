using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using RateMyP.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RateMyP.WebApp.Controllers
    {
    public interface IReportsController
        {
        Task<ActionResult<IEnumerable<RatingReport>>> GetRatingsReports();
        Task<ActionResult<IEnumerable<CustomStarReport>>> GetCustomStarsReports();
        Task<ActionResult<IEnumerable<RatingReport>>> GetRatingReports(Guid ratingId);
        Task<ActionResult<IEnumerable<CustomStarReport>>> GetCustomStarReports(Guid customStarId);
        Task<IActionResult> GetReport(Guid id);
        Task<ActionResult<RatingReport>> PostRatingReport([FromBody] JObject data);
        Task<ActionResult<CustomStarReport>> PostCustomStarReport([FromBody] JObject data);
        }

    [Route("api/reports")]
    [ApiController]
    public class ReportsController : ControllerBase, IReportsController
        {
        private readonly RateMyPDbContext m_context;

        public ReportsController(RateMyPDbContext context)
            {
            m_context = context;
            }

        [HttpGet("rating")]
        public async Task<ActionResult<IEnumerable<RatingReport>>> GetRatingsReports()
            {
            return await m_context.RatingReports.ToListAsync();
            }

        [HttpGet("customStar")]
        public async Task<ActionResult<IEnumerable<CustomStarReport>>> GetCustomStarsReports()
            {
            return await m_context.CustomStarReports.ToListAsync();
            }

        [HttpGet("custom-star={customStarId}")]
        public async Task<ActionResult<IEnumerable<CustomStarReport>>> GetCustomStarReports(Guid customStarId)
            {
            return await m_context.CustomStarReports.Where(x => x.CustomStarId.Equals(customStarId)).ToListAsync();
            }

        [HttpGet("rating={ratingId}")]
        public async Task<ActionResult<IEnumerable<RatingReport>>> GetRatingReports(Guid ratingId)
            {
            return await m_context.RatingReports.Where(x => x.RatingId.Equals(ratingId)).ToListAsync();
            }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetReport(Guid id)
            {
            var ratingReport = await m_context.RatingReports
                                        .SingleAsync(x => x.Id.Equals(id));
            var customStarReport = await m_context.CustomStarReports
                                                        .SingleAsync(x => x.Id.Equals(id));

            if (ratingReport != null)
                return Ok(ratingReport);
            if (customStarReport != null)
                return Ok(customStarReport);

            return NotFound("Report not found");
            }

        [HttpPost("rating")]
        public async Task<ActionResult<RatingReport>> PostRatingReport([FromBody] JObject data)
            {
            var report = new RatingReport
                {
                Id = Guid.NewGuid(),
                DateCreated = DateTime.Now,
                StudentId = (string)data["studentId"],
                RatingId = (Guid)data["ratingId"],
                Reason = (string)data["reason"]
                };
            m_context.RatingReports.Add(report);
            await m_context.SaveChangesAsync();

            return CreatedAtAction("GetRatingReport", new { id = report.Id }, report);
            }

        [HttpPost("custom-star")]
        public async Task<ActionResult<CustomStarReport>> PostCustomStarReport([FromBody] JObject data)
            {
            var report = new CustomStarReport
                {
                Id = Guid.NewGuid(),
                DateCreated = DateTime.Now,
                StudentId = (string)data["studentId"],
                CustomStarId = (Guid)data["customStarId"],
                Reason = (string)data["reason"]
                };
            m_context.CustomStarReports.Add(report);
            await m_context.SaveChangesAsync();

            return CreatedAtAction("GetCustomStarReport", new { id = report.Id }, report);
            }
        }
    }