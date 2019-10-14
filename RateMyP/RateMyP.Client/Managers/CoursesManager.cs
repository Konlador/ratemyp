using System;
using RateMyP.Entities;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace RateMyP.Client.Managers
    {
    public interface ICoursesManager
        {
        Task<List<Course>> GetAll();
        Task<Course> Get(Guid courseId);
        }

    public class CoursesManager : ICoursesManager
        {
        private readonly HttpClient m_client;

        public CoursesManager(HttpClient client)
            {
            m_client = client;
            }

        public async Task<List<Course>> GetAll()
            {
            for (var i = 0; i < 5; i++)
                {
                var response = await m_client.GetAsync("/api/courses");
                if (response.IsSuccessStatusCode)
                    return await response.Content.ReadAsAsync<List<Course>>();
                }
            return null;
            }

        public async Task<Course> Get(Guid courseId)
            {
            for (var i = 0; i < 5; i++)
                {
                var response = await m_client.GetAsync($"/api/courses/{courseId.ToString()}");
                if (response.IsSuccessStatusCode)
                    return await response.Content.ReadAsAsync<Course>();
                }
            return null;
            }
        }
    }
