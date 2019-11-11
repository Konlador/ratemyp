using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using RateMyP.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace RateMyP.WebApp.Controllers
    {
    [Route("api/reports")]
    [ApiController]
    public class ReportsController : ControllerBase
        {
        private readonly RateMyPDbContext m_context;

        public ReportsController(RateMyPDbContext context)
            {
            m_context = context;
            }

        [HttpGet]
        public async Task<IActionResult> GetReports()
            {
            var reports = await m_context.RatingReports
                                         .ToListAsync();
            return Ok(SerializeReports(reports));
            }

        [HttpGet("rating={ratingId}")]
        public async Task<IActionResult> GetTeacherRatings(Guid ratingId)
            {
            var reports = await m_context.RatingReports
                                         .Where(x => x.RatingId.Equals(ratingId)).ToListAsync();
            return Ok(SerializeReports(reports));
            }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetReport(Guid id)
            {
            var report = await m_context.RatingReports
                                        .SingleAsync(x => x.Id.Equals(id));

            if (report == null)
                return NotFound();

            return Ok(SerializeReport(report));
            }

        private static JArray SerializeReports(IEnumerable<RatingReport> reports)
            {
            return JArray.FromObject(reports.Select(SerializeReport));
            }

        private static JObject SerializeReport(RatingReport report)
            {
            var serializer = new JsonSerializer
                {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
                };
            var serializedReport = JObject.FromObject(report, serializer);
            return serializedReport;
            }

       
        [HttpPost]
        public async Task<ActionResult<RatingReport>> PostRating([FromBody]JObject data)
            {
            var studentId = Guid.Empty;

            if (data["studentId"].ToObject<string>() != "")
                studentId = data["studentId"].ToObject<Guid>();

            var report = new RatingReport
                {
                Reason = data["reason"].ToObject<string>(),
                RatingId = data["ratingId"].ToObject<Guid>(),
                DateCreated = DateTime.Now,
                Id = Guid.NewGuid(),
                StudentId = studentId
                };
            m_context.RatingReports.Add(report);
            await m_context.SaveChangesAsync();

            return CreatedAtAction("GetRatingReport", new { id = report.Id }, report);
            }

        private bool ReportExists(Guid id)
            {
            return m_context.RatingReports.Any(e => e.Id.Equals(id));
            }
        }
    }