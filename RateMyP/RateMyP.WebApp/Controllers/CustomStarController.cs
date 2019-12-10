using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Text;
using System.Linq;
using System.Net.Http;
using System.Configuration;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RateMyP.WebApp.Models;

namespace RateMyP.WebApp.Controllers
    {
    [Route("api/customstar")]
    [ApiController]
    public class CustomStarController : ControllerBase
        {
        private readonly IHttpClientFactory m_clientFactory;
        private readonly RateMyPDbContext m_context;

        public CustomStarController(RateMyPDbContext context, IHttpClientFactory clientFactory)
            {
            m_context = context;
            m_clientFactory = clientFactory;
            }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomStarAsync(Guid id)
            {
            var images = await m_context.CustomStars
                                         .Where(x => x.Id.Equals(id)).FirstAsync();

            if (images == null)
                return NotFound();

            return Ok(images);
            }

        [HttpGet("teacher={teacherId}")]
        public async Task<IActionResult> GetTeacherCustomStarsAsync(Guid teacherId)
            {
            var customStars = await m_context.CustomStars
                                         .Where(x => x.TeacherId.Equals(teacherId)).ToListAsync();
            var sortedStars = customStars.OrderByDescending(g => g.ThumbUps - g.ThumbDowns).ToList();
            return Ok(sortedStars);
            }

        [HttpGet("teacher={teacherId}/image")]
        public async Task<IActionResult> GetCustomStarImageAsync(Guid teacherId)
            {
            var customStar = await m_context.CustomStars
                                            .Where(x => x.TeacherId.Equals(teacherId))
                                            .OrderByDescending(g => g.ThumbUps - g.ThumbDowns).FirstOrDefaultAsync();

            // using a third party image storage service https://cloudinary.com
            var imageId = "default_full";
            const int customStarScoreThreshold = 1;
            if (customStar != null && customStar!.ThumbUps >= customStarScoreThreshold)
                imageId = "_" + customStar.Id;

            const string transformation = "w_50,h_50,f_png";
            var htppClient = m_clientFactory.CreateClient();
            var imageResponse = await htppClient.GetAsync($"{ConfigurationManager.AppSettings["ImageRepURL"]}{ConfigurationManager.AppSettings["ImageApiName"]}/{transformation}/{imageId}");

            if (!imageResponse.IsSuccessStatusCode)
                return NotFound();

            var image = await imageResponse.Content.ReadAsByteArrayAsync();
            return File(image, "image/png");
            }

        [Authorize]
        [HttpPost("teacher={teacherId}")]
        public async Task<IActionResult> PostCustomStarAsync(Guid teacherId, [FromBody] JObject data)
            {
            // getting the authorized students id
            var identity = (ClaimsIdentity)User.Identity;
            var studentId = identity.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;
            if (studentId == null || m_context.Students.Find(studentId) == null)
                return NotFound("Student not found");

            var customStarId = Guid.NewGuid();
            var uploadDate = DateTime.Now;
            var timestamp = (int)uploadDate.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
            var publicId = $"_{customStarId}";
            var image = (string)data["image"];
            var stringToSign = "public_id=" + publicId + "&tags=" + teacherId + "&timestamp=" + timestamp + ConfigurationManager.AppSettings["ImageApiSecretKey"];

            var imageFile = new
                {
                file = image,
                api_key = ConfigurationManager.AppSettings["ImageApiKey"],
                timestamp,
                public_id = publicId,
                signature = Hash(stringToSign),
                tags = teacherId,
                };

            var client = m_clientFactory.CreateClient();
            var uri = ConfigurationManager.AppSettings["ImageApiURL"] + ConfigurationManager.AppSettings["ImageApiName"] + "/image/upload";
            var response = await client.PostAsync(uri, new StringContent(JsonConvert.SerializeObject(imageFile), Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();

            var customStar = new CustomStar
                {
                Id = customStarId,
                TeacherId = teacherId,
                DateCreated = uploadDate,
                StudentId = studentId,
                ThumbDowns = 0,
                ThumbUps = 0
                };

            m_context.CustomStars.Add(customStar);
            await m_context.SaveChangesAsync();

            return Created("CustomStar", new { id = customStarId }); ;
            }

        [Authorize]
        [HttpPost("thumb")]
        public async Task<ActionResult<CustomStarThumb>> PostCustomStarThumbAsync([FromBody]JObject customStarThumbJObject)
            {
            var customStarThumb = new CustomStarThumb
                {
                CustomStarId = (Guid)customStarThumbJObject["customStarId"],
                ThumbUp = (bool)customStarThumbJObject["thumbUp"],
                };

            var customStar = m_context.CustomStars.Find(customStarThumb.CustomStarId);
            if (customStar == null)
                return NotFound("Custom star not found");

            // getting the authorized students id
            var identity = (ClaimsIdentity)User.Identity;
            var studentId = identity.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;
            if (studentId == null || m_context.Students.Find(studentId) == null)
                return NotFound("Student not found");

            customStarThumb.StudentId = studentId;

            if (m_context.CustomStarThumbs.Find(customStar.Id, studentId) != null)
                return Conflict("Custom star thumb already exists for specified rating and student");

            m_context.CustomStarThumbs.Add(customStarThumb);
            await m_context.SaveChangesAsync();

            if (customStarThumb.ThumbUp)
                customStar.ThumbUps = m_context.CustomStarThumbs.Count(x => x.CustomStarId.Equals(customStar.Id) && x.ThumbUp);
            else
                customStar.ThumbDowns = m_context.CustomStarThumbs.Count(x => x.CustomStarId.Equals(customStar.Id) && !x.ThumbUp);

            await m_context.SaveChangesAsync();
            return Created("CustomStarThumb", customStarThumb);
            }

        private static string Hash(string stringToHash)
            {
            using var sha1 = new SHA1Managed();
            return BitConverter.ToString(sha1.ComputeHash(Encoding.UTF8.GetBytes(stringToHash))).Replace("-", "");
            }
        }
    }