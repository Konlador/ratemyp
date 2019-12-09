using System;
using System.Linq;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using RateMyP.WebApp.Db;
using RateMyP.WebApp.Statistics;


namespace RateMyP.WebApp
    {
    public class Program
        {
        public static void Main(string[] args)
            {
            //using (var context = new RateMyPDbContext())
            //    {
            //    var dataLoader = new DbDataLoader(context);
            //    dataLoader.LoadDataToDb();
            //    }
            //RunLeaderboardUpdate();

            CreateWebHostBuilder(args).Build().Run();
            }

        private static async void RunLeaderboardUpdate()
            {
            var dbC = new RateMyPDbContext();
            var lbM = new LeaderboardManager(new TeacherStatisticsAnalyzer(dbC), new CourseStatisticsAnalyzer(dbC), dbC);
            await lbM.FullUpdateAsync();
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
