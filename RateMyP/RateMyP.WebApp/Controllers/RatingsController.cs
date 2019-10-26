using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RateMyP.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        // GET: api/Ratings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Rating>>> GetRatings()
            {
            return await m_context.Ratings
                                  .Include(rating => rating.Tags)
                                  .ThenInclude(ratingTag => ratingTag.Tag)
                                  .ToListAsync();
            }

        // GET: api/Ratings/teacher=5
        [HttpGet("teacher={teacherId}")]
        public async Task<ActionResult<IEnumerable<Rating>>> GetTeacherRatings(Guid teacherId)
            {
            return await m_context.Ratings
                                  .Include(rating => rating.Tags)
                                  .ThenInclude(ratingTag => ratingTag.Tag)
                                  .Where(x => x.TeacherId.Equals(teacherId)).ToListAsync();
            }

        // GET: api/Ratings/5
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
