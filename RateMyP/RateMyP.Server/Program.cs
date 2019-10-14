using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using RateMyP.Server.Db;

namespace RateMyP.Server
    {
    public class Program
        {
        public static void Main(string[] args)
            {
            //DbDataLoader.LoadDataToDb();
            CreateHostBuilder(args).Build().Run();
            }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
        }
    }
