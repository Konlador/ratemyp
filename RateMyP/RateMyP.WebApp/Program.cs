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

            CreateWebHostBuilder(args).Build().Run();
            }

        private static async void RunLeaderboardUpdate()
            {
            RateMyPDbContext dbC = new RateMyPDbContext();
            LeaderboardManager lbM = new LeaderboardManager(new TeacherStatisticsAnalyzer(dbC), new CourseStatisticsAnalyzer(dbC), dbC);
            await lbM.FullUpdate();
            }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
        }
    }
