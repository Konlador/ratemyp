using System;
using System.Collections.Generic;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using RateMyP.WebApp.Db;
using RateMyP.WebApp.Models;

namespace RateMyP.WebApp
    {
    public class Program
        {
        private static void LoadRating()
            {
            var ratingId = Guid.NewGuid();
            var rating = new Rating
                {
                Id = ratingId,
                Comment = "mldc",
                CourseId = Guid.Parse("75F8D852-A864-5B7A-91F8-F70B7526FA99"),
                TeacherId = Guid.Parse("C0F0A9CE-5D03-5C13-9D9E-043919A0838E"),
                DateCreated = DateTime.Now,
                LevelOfDifficulty = 1,
                OverallMark = 4,
                Tags = new List<RatingTag>
                                        {
                                        new RatingTag
                                            {
                                            RatingId = ratingId,
                                            TagId = Guid.Parse("61B5CC67-FF3D-4938-9961-2DE73E5ED675")
                                            },
                                        new RatingTag
                                            {
                                            RatingId = ratingId,
                                            TagId = Guid.Parse("CF5995ED-A734-4CB4-9E4E-60C88F509A91")
                                            }
                                        }
                };

            using var context = new RateMyPDbContext();
            context.Ratings.Add(rating);
            context.SaveChanges();
            }

        public static void Main(string[] args)
            {
            //DbDataLoader.LoadDataToDb();
            //LoadRating();
            CreateWebHostBuilder(args).Build().Run();
            }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
        }
    }
