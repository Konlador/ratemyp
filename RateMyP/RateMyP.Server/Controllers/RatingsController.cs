using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RateMyP.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RateMyP.Server.Controllers
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
            return await m_context.Ratings.ToListAsync();
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

        // PUT: api/Ratings/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRating(Guid id, Rating rating)
            {
            if (id != rating.Id)
                return BadRequest();

            m_context.Entry(rating).State = EntityState.Modified;

            try
                {
                await m_context.SaveChangesAsync();
                }
            catch (DbUpdateConcurrencyException)
                {
                if (!RatingExists(id))
                    return NotFound();
                else
                    throw;
                }

            return NoContent();
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

        // DELETE: api/Ratings/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Rating>> DeleteRating(Guid id)
            {
            var rating = await m_context.Ratings.FindAsync(id);
            if (rating == null)
                return NotFound();

            m_context.Ratings.Remove(rating);
            await m_context.SaveChangesAsync();

            return rating;
            }

        private bool RatingExists(Guid id)
            {
            return m_context.Ratings.Any(e => e.Id == id);
            }
        }
    }
