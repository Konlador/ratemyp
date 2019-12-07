using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using RateMyP.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using RateMyP.WebApp.Statistics;

namespace RateMyP.WebApp.Controllers
    {
    public interface IRatingsController
        {
        Task<IActionResult> GetRatings();
        Task<IActionResult> GetTeacherRatings(Guid teacherId);
        Task<IActionResult> GetCourseRatings(Guid courseId);
        Task<IActionResult> GetRating(Guid id);
        Task<ActionResult<RatingThumb>> PostRatingThumb([FromBody]JObject ratingThumbJObject);
        Task<ActionResult<Rating>> PostRating([FromBody] JObject data);
        }

    public delegate Task UpdateLeaderboard(Guid id, EntryType type);

    [Route("api/ratings")]
    [ApiController]
    public class RatingsController : ControllerBase, IRatingsController
        {
        private readonly RateMyPDbContext m_context;
        private readonly UpdateLeaderboard m_updateLeaderboardAsync;

        public RatingsController(RateMyPDbContext context, ILeaderboardManager leaderboardManager)
            {
            m_context = context;
            m_updateLeaderboardAsync = leaderboardManager.Update;
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

        [HttpGet("course={courseId}")]
        public async Task<IActionResult> GetCourseRatings(Guid courseId)
            {
            var ratings = await m_context.Ratings
                .Include(rating => rating.Tags)
                .ThenInclude(ratingTag => ratingTag.Tag)
                .Where(x => x.CourseId.Equals(courseId)).ToListAsync();
            return Ok(SerializeRatings(ratings));
            }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRating(Guid id)
            {
            var rating = await m_context.Ratings
                                        .Include(r => r.Tags)
                                        .ThenInclude(ratingTag => ratingTag.Tag)
                                        .SingleOrDefaultAsync(x => x.Id.Equals(id));

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
        public async Task<ActionResult<RatingThumb>> PostRatingThumb([FromBody]JObject ratingThumbJObject)
            {
            var ratingThumb = new RatingThumb
                {
                RatingId = (Guid)ratingThumbJObject["ratingId"],
                ThumbUp = (bool)ratingThumbJObject["thumbUp"]
                };

            var identity = (ClaimsIdentity)User.Identity;
            var studentId = identity.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;
            if (studentId == null || m_context.Students.Find(studentId) == null)
                return NotFound("Student not found");

            var rating = m_context.Ratings.Find(ratingThumb.RatingId);
            if (rating == null)
                return NotFound("Rating not found");

            ratingThumb.StudentId = studentId;

            if (m_context.RatingThumbs.Find(rating.Id, studentId) != null)
                return Conflict("Rating thumb already exists for specified rating and student");

            m_context.RatingThumbs.Add(ratingThumb);

            if (ratingThumb.ThumbUp)
                rating.ThumbUps = m_context.RatingThumbs.Count(rt => rt.RatingId.Equals(rating.Id) && rt.ThumbUp);
            else
                rating.ThumbDowns = m_context.RatingThumbs.Count(rt => rt.RatingId.Equals(rating.Id) && !rt.ThumbUp);

            await m_context.SaveChangesAsync();
            return Created("RatingThumb", ratingThumb);
            }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Rating>> PostRating([FromBody]JObject data)
            {
            var ratingId = Guid.NewGuid();

            var ratingTags = new List<RatingTag>();
            var tags = JsonConvert.DeserializeObject<List<Tag>>((string)data["tags"]);
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

            Guid.TryParse((string)data["teacherId"], out var teacherId);
            Guid.TryParse((string) data["courseId"], out var courseId);

            var rating = new Rating
                {
                Id = ratingId,
                DateCreated = DateTime.Now,
                TeacherId = teacherId,
                Tags = ratingTags,
                Comment = (string)data["comment"],
                CourseId = courseId,
                LevelOfDifficulty = (int)data["levelOfDifficulty"],
                OverallMark = (int)data["overallMark"],
                WouldTakeTeacherAgain = (bool)data["wouldTakeTeacherAgain"],
                RatingType = (RatingType)(int)data["ratingType"],
                ThumbUps = 0,
                ThumbDowns = 0
                };
            m_context.Ratings.Add(rating);
            await m_context.SaveChangesAsync();

            if (teacherId != Guid.Empty)
                await m_updateLeaderboardAsync(teacherId, EntryType.Teacher);
            await m_updateLeaderboardAsync(courseId, EntryType.Course);

            return CreatedAtAction("GetRating", new { id = rating.Id }, rating);
            }
        }
    }