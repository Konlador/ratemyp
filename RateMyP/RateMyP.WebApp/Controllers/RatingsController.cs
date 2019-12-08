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
using RateMyP.WebApp.Helpers;
using RateMyP.WebApp.Statistics;

namespace RateMyP.WebApp.Controllers
    {
    public interface IRatingsController
        {
        Task<IActionResult> GetRatings();
        Task<IActionResult> GetTeacherRatings(Guid teacherId);
        Task<IActionResult> GetCourseRatings(Guid courseId);
        Task<IActionResult> GetRating(Guid id);
        Task<ActionResult<RatingThumb>> PostRatingThumb(RatingThumb ratingThumb);
        Task<ActionResult<Rating>> PostRating([FromBody] JObject data);
        }

    public delegate Task UpdateLeaderboard(Guid id, EntryType type);

    [Route("api/ratings")]
    [ApiController]
    public class RatingsController : ControllerBase
        {
        private readonly RateMyPDbContext m_context;
        private readonly UpdateLeaderboard m_updateLeaderboardAsync;

        public RatingsController(RateMyPDbContext context, ILeaderboardManager leaderboardManager)
            {
            m_context = context;
            m_updateLeaderboardAsync = leaderboardManager.Update;
            }

        [HttpGet]
        public ActionResult<IEnumerable<RatingDto>> GetRatings()
            {
            var ratings = m_context.Ratings.SelectRatingDto();
            return Ok(ratings);
            }

        [HttpGet("teacher={teacherId}")]
        public ActionResult<IEnumerable<RatingDto>> GetTeacherRatings(Guid teacherId)
            {
            var ratings = m_context.Ratings
                                   .Where(x => x.TeacherId.Equals(teacherId))
                                   .SelectRatingDto();
            return Ok(ratings);
            }

        [HttpGet("course={courseId}")]
        public ActionResult<IEnumerable<RatingDto>> GetCourseRatings(Guid courseId)
            {
            var ratings = m_context.Ratings
                                   .Where(x => x.CourseId.Equals(courseId))
                                   .SelectRatingDto();
            return Ok(ratings);
            }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRatingAsync(Guid id)
            {
            var rating = await m_context.Ratings
                                        .Include(r => r.Tags)
                                        .ThenInclude(rt => rt.Tag)
                                        .SingleAsync(x => x.Id.Equals(id));
            if (rating == null)
                return NotFound();

            return Ok(rating.SelectRatingDto());
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
            Guid.TryParse((string)data["courseId"], out var courseId);

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