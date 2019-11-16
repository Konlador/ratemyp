using RateMyP.Entities;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace RateMyP.Client.Managers
    {
    public interface ITagsManager
        {
        Task<List<Tag>> GetAll();
        }

    public class TagsManager : ITagsManager
        {
        private readonly HttpClient m_client;

        public TagsManager(HttpClient client)
            {
            m_client = client;
            }

        public async Task<List<Tag>> GetAll()
            {
            for (var i = 0; i < 5; i++)
                {
                var response = await m_client.GetAsync("/api/tags");
                if (response.IsSuccessStatusCode)
                    return await response.Content.ReadAsAsync<List<Tag>>();
                }
            return null;
            }
        }
    }
