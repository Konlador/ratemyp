using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using RateMyP.WebApp.Db;
using RateMyP.WebApp.Models;

namespace RateMyP.WebApp
    {
    public class Program
        {
        public static void Main(string[] args)
            {
            //var lbM = new LeaderboardManager(new RateMyPDbContext());
            //lbM.RunFullLeaderboardUpdate();
            //DbDataLoader.LoadDataToDb();
            //LoadRating();
            CreateWebHostBuilder(args).Build().Run();
            }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
        }
    }
