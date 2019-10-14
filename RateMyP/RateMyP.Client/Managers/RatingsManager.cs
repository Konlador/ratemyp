using System;
using RateMyP.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace RateMyP.Client.Managers
    {
    public class RatingsManager
        {
        private readonly HttpClient m_client;

        public RatingsManager(HttpClient client)
            {
            m_client = client;
            }

        public async Task<List<Rating>> GetAll()
            {
            for (var i = 0; i < 5; i++)
                {
                var response = await m_client.GetAsync("/api/ratings");
                if (response.IsSuccessStatusCode)
                    return await response.Content.ReadAsAsync<List<Rating>>();
                }
            return null;
            }

        public async Task<List<Rating>> GetTeacherRatings(Guid teacherId)
            {
            for (var i = 0; i < 5; i++)
                {
                var response = await m_client.GetAsync("/api/ratings");
                if (!response.IsSuccessStatusCode) continue;
                var allRatings = await response.Content.ReadAsAsync<List<Rating>>();
                return allRatings.Where(x => x.TeacherId.Equals(teacherId)).ToList();
                }
            return null;
            }

        public async void Post(Rating rating)
            {
            HttpResponseMessage response = null;
            for (var i = 0; i < 5 && response?.StatusCode != HttpStatusCode.Created; i++)
                response = await m_client.PostAsJsonAsync("api/ratings", rating);
            response?.EnsureSuccessStatusCode();
            }
        }
    }
