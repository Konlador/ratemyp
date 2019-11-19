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

    [Route("api/images")]
    [ApiController]
    public class ImageController : ControllerBase
        {
        static HttpClient client = new HttpClient();

        [HttpGet("{id}")]
        public async Task<byte[]> GetImageAsync(Guid id)
            {
            byte[] image = new byte[0];
            HttpResponseMessage response = await client.GetAsync(ConfigurationManager.AppSettings["repURL"] + ConfigurationManager.AppSettings["apiName"] + "/image/upload/" + id);

            if (response.IsSuccessStatusCode)
                {
                image = await response.Content.ReadAsByteArrayAsync();
                }

            return image;
            }

        [HttpPost]
        public async Task<Uri> UploadImageAsync([FromBody] JObject data)
            {
            var publicId = (Guid)data["teacherId"];
            var image = (string)data["image"];
            var timeStamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            var stringToSign = "public_id=" + publicId + "&timestamp=" + timeStamp + ConfigurationManager.AppSettings["apiSecretKey"];
            var sign = Hash(stringToSign);

            var imageFile = new
                {
                file = image,
                api_key = ConfigurationManager.AppSettings["apiKey"],
                timestamp = timeStamp,
                public_id = publicId,
                signature = sign
                };

            HttpResponseMessage response = await client.PostAsJsonAsync(
                ConfigurationManager.AppSettings["apiURL"] + ConfigurationManager.AppSettings["apiName"]  + "/image/upload", imageFile);
            response.EnsureSuccessStatusCode();

            return response.Headers.Location;
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