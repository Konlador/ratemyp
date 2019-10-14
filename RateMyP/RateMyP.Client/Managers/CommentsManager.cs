using RateMyP.Entities;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace RateMyP.Client.Managers
    {
    public interface ICommentsManager
        {
        Task<List<Comment>> GetAll();
        }

    public class CommentsManager : ICommentsManager
        {
        private readonly HttpClient m_client;

        public CommentsManager(HttpClient client)
            {
            m_client = client;
            }

        public async Task<List<Comment>> GetAll()
            {
            for (var i = 0; i < 5; i++)
                {
                var response = await m_client.GetAsync("/api/comments");
                if (response.IsSuccessStatusCode)
                    return await response.Content.ReadAsAsync<List<Comment>>();
                }
            return null;
            }
        }
    }
