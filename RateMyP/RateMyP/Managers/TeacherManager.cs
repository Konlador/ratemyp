using RateMyP.Entities;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace RateMyP.Managers
    {
    public class TeacherManager
        {
        private readonly HttpClient m_client;

        public TeacherManager(HttpClient client)
            {
            m_client = client;
            }

        public async Task<List<Teacher>> GetAll()
            {
            for (var i = 0; i < 5; i++)
                {
                var response = await m_client.GetAsync("/api/parkinglots");
                if (response.IsSuccessStatusCode)
                    return await response.Content.ReadAsAsync<List<ParkingLot>>();
                }
            return null;
            }
        }
    }
