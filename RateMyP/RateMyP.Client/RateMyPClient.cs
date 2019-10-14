using RateMyP.Client.Managers;
using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace RateMyP.Client
    {
    public interface IRateMyPClient
        {
        TeachersManager Teachers { get; }
        StudentsManager Students { get; }
        RatingsManager Ratings { get; }
        CoursesManager Courses { get; }
        }

    public class RateMyPClient : IRateMyPClient
        {
        private static readonly Lazy<RateMyPClient> s_client = new Lazy<RateMyPClient>(() => new RateMyPClient());

        public static RateMyPClient Client => s_client.Value;

        public TeachersManager Teachers { get; }
        public StudentsManager Students { get; }
        public RatingsManager Ratings { get; }
        public CoursesManager Courses { get; }
        public ITagsManager Tags { get; }

        private RateMyPClient()
            {
            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:44382");
            //httpClient.BaseAddress = new Uri("https://ratemypserver79.azurewebsites.net");
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            Teachers = new TeachersManager(httpClient);
            Students = new StudentsManager(httpClient);
            Ratings = new RatingsManager(httpClient);
            Courses = new CoursesManager(httpClient);
            Tags = new TagsManager(httpClient);
            }
        }
    }
