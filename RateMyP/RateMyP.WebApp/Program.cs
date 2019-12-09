using System.IO;
using System.Reflection;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using RateMyP.WebApp.Statistics;


namespace RateMyP.WebApp
    {
    public class Program
        {
        public static void Main(string[] args)
            {
            //DbDataLoader.LoadDataToDb();
            //LoadRating();
            //RunLeaderboardUpdate();
            CreateWebHostBuilder(args).Build().Run();
            }

        private static async void RunLeaderboardUpdate()
            {
            RateMyPDbContext dbC = new RateMyPDbContext();
            LeaderboardManager lbM = new LeaderboardManager(new TeacherStatisticsAnalyzer(dbC), new CourseStatisticsAnalyzer(dbC), dbC);
            await lbM.FullUpdate();
            }

        private byte[] LoadImage(string name)
            {
            var assembly = Assembly.GetExecutingAssembly();
            using var stream = assembly.GetManifestResourceStream($"RateMyP.WebApp.Db.SeedData.{name}");
            }
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
        }
    }
