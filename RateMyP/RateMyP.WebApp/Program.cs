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
            //RunBadgeUpdates();
            CreateWebHostBuilder(args).Build().Run();
            }

        private static async void RunLeaderboardUpdate()
            {
            var dbC = new RateMyPDbContext();
            var lbM = new LeaderboardManager(new TeacherStatisticsAnalyzer(dbC), new CourseStatisticsAnalyzer(dbC), dbC);
            await lbM.FullUpdateAsync();
            }

        private static async void RunBadgeUpdates()
            {
            var dbC = new RateMyPDbContext();
            var bM = new BadgeManager(dbC, new TeacherStatisticsAnalyzer(dbC));
            await bM.FullUpdateAsync();
            }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
        }
    }
