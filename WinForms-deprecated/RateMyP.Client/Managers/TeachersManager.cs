using RateMyP.Entities;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace RateMyP.Client.Managers
    {
    public interface ITeachersManager
        {
        Task<List<Teacher>> GetAll();
        Task<Teacher> Get(Guid teacherId);
        }

    public class TeachersManager : ITeachersManager
        {
        private readonly HttpClient m_client;

        public TeachersManager(HttpClient client)
            {
            m_client = client;
            }

        public async Task<List<Teacher>> GetAll()
            {
            for (var i = 0; i < 5; i++)
                {
                var response = await m_client.GetAsync("/api/teachers");
                if (response.IsSuccessStatusCode)
                    return await response.Content.ReadAsAsync<List<Teacher>>();
                }
            return null;
            }

        public async Task<Teacher> Get(Guid teacherId)
            {
            for (var i = 0; i < 5; i++)
                {
                var response = await m_client.GetAsync($"/api/teachers/{teacherId.ToString()}");
                if (response.IsSuccessStatusCode)
                    return await response.Content.ReadAsAsync<Teacher>();
                }
            return null;
            }
        }
    }
