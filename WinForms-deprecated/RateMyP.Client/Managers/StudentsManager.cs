using RateMyP.Entities;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace RateMyP.Client.Managers
    {
    public interface IStudentsManager
        {
        Task<List<Student>> GetAll();
        }

    public class StudentsManager : IStudentsManager
        {
        private readonly HttpClient m_client;

        public StudentsManager(HttpClient client)
            {
            m_client = client;
            }

        public async Task<List<Student>> GetAll()
            {
            for (var i = 0; i < 5; i++)
                {
                var response = await m_client.GetAsync("/api/students");
                if (response.IsSuccessStatusCode)
                    return await response.Content.ReadAsAsync<List<Student>>();
                }
            return null;
            }
        }
    }
