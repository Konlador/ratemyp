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

    [Route("api/ratings")]
    [ApiController]
    public class RatingsController : ControllerBase
        {
        private readonly RateMyPDbContext m_context;
        private readonly ILeaderboardManager m_leaderboardManager;

        public RatingsController(RateMyPDbContext context, ILeaderboardManager leaderboardManager)
            {
            m_context = context;
            m_leaderboardManager = leaderboardManager;
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

        [HttpPost]
        public async Task<ActionResult<Rating>> PostRating(RatingDto ratingDto)
            {
            var ratingId = Guid.NewGuid();
            var ratingTags = new List<RatingTag>();
            var validTags = await m_context.Tags.ToListAsync();
            foreach (var tag in ratingDto.Tags)
                {
                if (validTags.Any(t => t.Id.Equals(tag.Id)))
                    ratingTags.Add(new RatingTag
                        {
                        RatingId = ratingId,
                        TagId = tag.Id
                        }
                    );
                else
                    return NotFound("Tag not found");
                }

            var rating = new Rating
                {
                Id = ratingId,
                DateCreated = DateTime.Now,
                TeacherId = ratingDto.TeacherId,
                Tags = ratingTags,
                Comment = ratingDto.Comment,
                CourseId = ratingDto.CourseId,
                LevelOfDifficulty = ratingDto.LevelOfDifficulty,
                OverallMark = ratingDto.OverallMark,
                WouldTakeTeacherAgain = ratingDto.WouldTakeTeacherAgain,
                RatingType = ratingDto.RatingType,
                ThumbUps = 0,
                ThumbDowns = 0
                };
            m_context.Ratings.Add(rating);
            await m_context.SaveChangesAsync();

            if (rating.TeacherId != Guid.Empty)
                await m_leaderboardManager.UpdateTeacherAsync(rating.TeacherId);
            await m_leaderboardManager.UpdateCourseAsync(rating.CourseId);

            return CreatedAtAction(nameof(GetRatingAsync), new { id = rating.Id });
            }
        }
    }