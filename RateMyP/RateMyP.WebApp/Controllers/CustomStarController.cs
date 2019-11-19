using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;
using System.Configuration;

namespace RateMyP.WebApp.Controllers
    {

    public interface ICustomStarController
    {
        Task<byte[]> GetImageAsync(Guid teacherId);
        Task<IActionResult> UploadImageAsync(Guid teacherId, [FromBody] JObject data);
    }

    [Route("api/images")]
    [ApiController]
    public class CustomStarController : ControllerBase, ICustomStarController
    {
        private readonly IHttpClientFactory _clientFactory;

        public CustomStarController(IHttpClientFactory clientFactory)
            {
            _clientFactory = clientFactory;
            }

        [HttpGet("teacher={teacherId}")]
        public async Task<byte[]> GetImageAsync(Guid teacherId)
            {
            var client = _clientFactory.CreateClient();
            byte[] image = new byte[0];
            HttpResponseMessage response = await client.GetAsync(ConfigurationManager.AppSettings["ImageRepURL"] + ConfigurationManager.AppSettings["ImageApiName"] + "/image/upload/" + teacherId);

            if (response.IsSuccessStatusCode)
                {
                image = await response.Content.ReadAsByteArrayAsync();
                }

            return image;
            }

        [HttpPost("teacher={teacherId}")]
        public async Task<IActionResult> UploadImageAsync(Guid teacherId, [FromBody] JObject data)
            {
            var client = _clientFactory.CreateClient();
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

            HttpResponseMessage response = await client.PostAsJsonAsync(
                ConfigurationManager.AppSettings["ImageApiURL"] + ConfigurationManager.AppSettings["ImageApiName"]  + "/image/upload", imageFile);
            response.EnsureSuccessStatusCode();

            return CreatedAtAction("GetImage", new { id = publicId }, image); ;
            }

        public static string Hash(string stringToHash)
            {
            using (var sha1 = new SHA1Managed())
                {
                return BitConverter.ToString(sha1.ComputeHash(Encoding.UTF8.GetBytes(stringToHash))).Replace("-", "");
                }
            }

        }
    }