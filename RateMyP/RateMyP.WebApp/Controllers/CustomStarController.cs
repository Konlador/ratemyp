using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Text;
using System.Linq;
using System.Net.Http;
using System.Configuration;
using System.Threading.Tasks;
using System.Security.Cryptography;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RateMyP.WebApp.Models;

namespace RateMyP.WebApp.Controllers
    {

    public interface ICustomStarController
        {
        Task<IActionResult> GetImageAsync(Guid teacherId);
        Task<IActionResult> GetImagesData(Guid teacherId);
        Task<IActionResult> PostImageAsync(Guid teacherId, [FromBody] JObject data);
        Task<ActionResult<CustomStarThumb>> PostCustomStarThumb(CustomStarThumb customStarThumb);
        Task<IActionResult> PostImageWithTagAsync(string studentId, Guid teacherId, [FromBody] JObject data);
        }

    [Route("api/images")]
    [ApiController]
    public class CustomStarController : ControllerBase, ICustomStarController
        {
        private readonly IHttpClientFactory _clientFactory;
        private readonly RateMyPDbContext m_context;

        public CustomStarController(RateMyPDbContext context, IHttpClientFactory clientFactory)
            {
            m_context = context;
            _clientFactory = clientFactory;
            }

        [HttpGet("teacher={teacherId}")]
        public async Task<IActionResult> GetImageAsync(Guid teacherId)
            {
            var client = _clientFactory.CreateClient();
            var transformation = "w_50,h_50,f_png";
            byte[] image = null;
            HttpResponseMessage response = await client.GetAsync($"{ConfigurationManager.AppSettings["ImageRepURL"]}{ConfigurationManager.AppSettings["ImageApiName"]}/{transformation}/{teacherId}");

            if (response.IsSuccessStatusCode)
                {
                image = await response.Content.ReadAsByteArrayAsync();
                }
            else
                {
                response = await client.GetAsync($"{ConfigurationManager.AppSettings["ImageRepURL"]}{ConfigurationManager.AppSettings["ImageApiName"]}/{transformation}/default_full");
                image = await response.Content.ReadAsByteArrayAsync();
                }
            return File(image, "image/png");
            }

        [HttpGet("data/teacher={teacherId}")]
        public async Task<IActionResult> GetImagesData(Guid teacherId)
            {
            var images = await m_context.CustomStarRatings
                                         .Where(x => x.TeacherId.Equals(teacherId)).ToListAsync();
            var sortedImages = images.OrderByDescending(g => g.ThumbUps - g.ThumbDowns).ToList();
            return Ok(sortedImages);
            }


        [HttpPost("thumb")]
        public async Task<ActionResult<CustomStarThumb>> PostCustomStarThumb(CustomStarThumb customStarThumb)
            {
            var customStarRating = m_context.CustomStarRatings.Find(customStarThumb.CustomStarId);
            if (customStarRating == null)
                return NotFound("Rating not found");

            customStarThumb.StudentId = Guid.NewGuid().ToString();
            m_context.CustomStarThumbs.Add(customStarThumb);

            if (customStarThumb.ThumbUp)
                customStarRating.ThumbUps++;
            else
                customStarRating.ThumbDowns++;

            await m_context.SaveChangesAsync();
            return Created("CustomStarThumb", customStarThumb);
            }

        [HttpPost("teacher={teacherId}")]
        public async Task<IActionResult> PostImageAsync(Guid teacherId, [FromBody] JObject data)
            {
            var client = _clientFactory.CreateClient();
            var uri = ConfigurationManager.AppSettings["ImageApiURL"] + ConfigurationManager.AppSettings["ImageApiName"] + "/image/upload";
            var publicId = teacherId;
            var image = (string)data["image"];
            var timeStamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            var stringToSign = "public_id=" + publicId + "&timestamp=" + timeStamp + ConfigurationManager.AppSettings["ImageApiSecretKey"];
            var sign = Hash(stringToSign);

            var imageFile = new
                {
                file = image,
                api_key = ConfigurationManager.AppSettings["ImageApiKey"],
                timestamp = timeStamp,
                public_id = publicId,
                signature = sign
                };

            var json = JsonConvert.SerializeObject(imageFile);
            HttpResponseMessage response = await client.PostAsync(uri, new StringContent(json, Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();

            return CreatedAtAction("GetImage", new { id = publicId }, image); ;
            }

        [HttpPost("teacher={teacherId}/student={studentId}")]
        public async Task<IActionResult> PostImageWithTagAsync(string studentId, Guid teacherId, [FromBody] JObject data)
            {
            var client = _clientFactory.CreateClient();
            var uri = ConfigurationManager.AppSettings["ImageApiURL"] + ConfigurationManager.AppSettings["ImageApiName"] + "/image/upload";
            var publicId = "_" + teacherId + "_" + studentId;
            var image = (string)data["image"];
            var timeStamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            var stringToSign = "public_id=" + publicId + "&tags=" + teacherId + "&timestamp=" + timeStamp + ConfigurationManager.AppSettings["ImageApiSecretKey"];
            var sign = Hash(stringToSign);

            var imageFile = new
                {
                file = image,
                api_key = ConfigurationManager.AppSettings["ImageApiKey"],
                timestamp = timeStamp,
                public_id = publicId,
                signature = sign,
                tags = teacherId,
                };

            var json = JsonConvert.SerializeObject(imageFile);
            HttpResponseMessage response = await client.PostAsync(uri, new StringContent(json, Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();

            var images = await m_context.CustomStarRatings
                                         .Where(x => x.TeacherId.Equals(teacherId) && x.StudentId.Equals(studentId)).SingleOrDefaultAsync();
            if (images == null)
                {
                var customImage = new CustomStarRating
                    {
                    Id = new Guid(),
                    TeacherId = teacherId,
                    DateCreated = DateTime.Now,
                    StudentId = studentId,
                    ThumbDowns = 0,
                    ThumbUps = 0
                    };
                m_context.CustomStarRatings.Add(customImage);
                await m_context.SaveChangesAsync();
                }
            else
                {
                images.DateCreated = DateTime.Now;
                images.ThumbDowns = 0;
                images.ThumbUps = 0;
                await m_context.SaveChangesAsync();
                }

            return CreatedAtAction("GetImage", new { id = publicId }, image); ;
            }

        public static string Hash(string stringToHash)
            {
            using var sha1 = new SHA1Managed();
            return BitConverter.ToString(sha1.ComputeHash(Encoding.UTF8.GetBytes(stringToHash))).Replace("-", "");
            }

        }
    }