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
            var serializer = new JsonSerializer
                {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
                };
            var serializedRating = JObject.FromObject(rating, serializer);
            var serializedTagsList = rating.Tags.Select(ratingTag => JObject.FromObject(ratingTag.Tag, serializer)).ToList();
            serializedRating["tags"] = JArray.FromObject(serializedTagsList, serializer);
            return serializedRating;
            }

        [HttpPost("thumb")]
        public async Task<ActionResult<RatingThumb>> PostRatingThumb(RatingThumb ratingThumb)
            {
            m_context.RatingThumbs.Add(ratingThumb);
            await m_context.SaveChangesAsync();
            return Created("RatingThumb", ratingThumb);
            }

        [HttpPost]
        public async Task<ActionResult<Rating>> PostRating([FromBody]JObject data)
            {
            var ratingId = Guid.NewGuid();
            var ratingTags = new List<RatingTag>();
            var tags = JsonConvert.DeserializeObject<List<Tag>>(data["tags"].ToString());

            foreach (var tag in tags)
                {
                if (await m_context.Tags.AnyAsync(t => t.Id.Equals(tag.Id)))
                    ratingTags.Add(new RatingTag
                                    {
                                    RatingId = ratingId,
                                    TagId = tag.Id
                                    }
                    );
                else
                    return NotFound("Tag not found");
                }

            var teacherId = new Guid();

            if (data["teacherId"].ToObject<string>() != "")
                teacherId = data["teacherId"].ToObject<Guid>();

            var rating = new Rating
                {
                Comment = data["comment"].ToObject<string>(),
                CourseId = data["courseId"].ToObject<Guid>(),
                DateCreated = DateTime.Now,
                Id = ratingId,
                LevelOfDifficulty = data["levelOfDifficulty"].ToObject<int>(),
                OverallMark = data["overallMark"].ToObject<int>(),
                Tags = ratingTags,
                TeacherId = teacherId,
                WouldTakeTeacherAgain = data["wouldTakeTeacherAgain"].ToObject<Boolean>(),
                RatingType = data["ratingType"].ToObject<RatingType>(),
            };
            m_context.Ratings.Add(rating);
            await m_context.SaveChangesAsync();

            return CreatedAtAction("GetRating", new { id = rating.Id }, rating);
            }
        }
    }