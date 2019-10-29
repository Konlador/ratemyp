using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using RateMyP.WebApp.Models;
using ClientErrorData = Microsoft.AspNetCore.Mvc.ClientErrorData;

namespace RateMyP.WebApp.Controllers
    {
    [Route("api/ratings")]
    [ApiController]
    public class RatingsController : ControllerBase
        {
        private readonly RateMyPDbContext m_context;

        public RatingsController(RateMyPDbContext context)
            {
            m_context = context;
            }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Rating>>> GetRatings()
            {
            var jObj = JObject.FromObject(await m_context.Ratings.ToListAsync());


            return await m_context.Ratings
                                  .Include(rating => rating.Tags)
                                  .ThenInclude(ratingTag => ratingTag.Tag)
                                  .ToListAsync();
            }

        [HttpGet("teacher={teacherId}")]
        public async Task<ActionResult<IEnumerable<Rating>>> GetTeacherRatings(Guid teacherId)
            {
            return await m_context.Ratings
                                  .Include(rating => rating.Tags)
                                  .ThenInclude(ratingTag => ratingTag.Tag)
                                  .Where(x => x.TeacherId.Equals(teacherId)).ToListAsync();
            }

        [HttpGet("{id}")]
        public async Task<ActionResult<Rating>> GetRating(Guid id)
            {
            var rating = await m_context.Ratings.FindAsync(id);

            if (rating == null)
                return NotFound();

            return rating;
            }

        // POST: api/Ratings
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Rating>> PostRating(Rating rating)
            {
            m_context.Ratings.Add(rating);
            await m_context.SaveChangesAsync();

            return CreatedAtAction("GetRating", new { id = rating.Id }, rating);
            }

        private bool RatingExists(Guid id)
            {
            return m_context.Ratings.Any(e => e.Id == id);
            }
        }
    }
