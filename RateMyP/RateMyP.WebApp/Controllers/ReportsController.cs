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
        Task<ActionResult<IEnumerable<RatingReport>>> GetReports();
        Task<ActionResult<IEnumerable<RatingReport>>> GetRatingReports(Guid ratingId);
        Task<ActionResult<RatingReport>> GetReport(Guid id);
        Task<ActionResult<RatingReport>> PostReport([FromBody] JObject data);
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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RatingReport>>> GetReports()
            {
            return await m_context.RatingReports.ToListAsync();
            }

        [HttpGet("rating={ratingId}")]
        public async Task<ActionResult<IEnumerable<RatingReport>>> GetRatingReports(Guid ratingId)
            {
            return await m_context.RatingReports.Where(x => x.RatingId.Equals(ratingId)).ToListAsync();
            }

        [HttpGet("{id}")]
        public async Task<ActionResult<RatingReport>> GetReport(Guid id)
            {
            var report = await m_context.RatingReports
                                        .SingleAsync(x => x.Id.Equals(id));

            if (report == null)
                return NotFound();

            return report;
            }

        [HttpPost]
        public async Task<ActionResult<RatingReport>> PostReport([FromBody] JObject data)
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
        }
    }