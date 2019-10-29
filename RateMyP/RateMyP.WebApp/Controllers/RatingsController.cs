﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using RateMyP.WebApp.Models;
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

        [HttpGet]
        public async Task<IActionResult> GetRatings()
            {
            var ratings = await m_context.Ratings
                                         .Include(rating => rating.Tags)
                                         .ThenInclude(ratingTag => ratingTag.Tag)
                                         .ToListAsync();
            return Ok(SerializeRatings(ratings));
            }

        [HttpGet("teacher={teacherId}")]
        public async Task<IActionResult> GetTeacherRatings(Guid teacherId)
            {
            var ratings = await m_context.Ratings
                                         .Include(rating => rating.Tags)
                                         .ThenInclude(ratingTag => ratingTag.Tag)
                                         .Where(x => x.TeacherId.Equals(teacherId)).ToListAsync();
            return Ok(SerializeRatings(ratings));
            }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRating(Guid id)
            {
            var rating = await m_context.Ratings
                                        .Include(r => r.Tags)
                                        .ThenInclude(ratingTag => ratingTag.Tag)
                                        .SingleAsync(x => x.Id.Equals(id));

            if (rating == null)
                return NotFound();

            return Ok(SerializeRating(rating));
            }

        private static JArray SerializeRatings(IEnumerable<Rating> ratings)
            {
            return JArray.FromObject(ratings.Select(SerializeRating));
            }

        private static JObject SerializeRating(Rating rating)
            {
            var serializedRating = JObject.FromObject(rating);
            var serializedTagsList = rating.Tags.Select(ratingTag => JObject.FromObject(ratingTag.Tag)).ToList();
            serializedRating["Tags"] = JArray.FromObject(serializedTagsList);
            return serializedRating;
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
        }
    }