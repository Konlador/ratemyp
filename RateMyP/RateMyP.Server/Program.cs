using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using RateMyP.Entities;
using RateMyP.Server.Db;

namespace RateMyP.Server
    {
    public class Program
        {
        public static void Main(string[] args)
            {
            //DbDataLoader.LoadDataToDb();

            //var tag = new Tag
            //    {
            //    Id = Guid.NewGuid(),
            //    Text = "Helpful"
            //    };

            //var ratingId = Guid.NewGuid();
            //var rating = new Rating
            //    {
            //    Id = ratingId,
            //    TeacherId = Guid.Parse("C0F0A9CE-5D03-5C13-9D9E-043919A0838E"),
            //    Comment = "Great teacher!! Very helpful, I really liked the class. Studyguies he gives are very helpful in tests. 1st online course I took and it motived me to take more. Extremely recommend this teacher.",
            //    CourseId = Guid.Parse("75F8D852-A864-5B7A-91F8-F70B7526FA99"),
            //    DateCreated = DateTime.Now,
            //    LevelOfDifficulty = 3,
            //    OverallMark = 5,
            //    Tags = new List<RatingTag>
            //                            {
            //                            new RatingTag
            //                                {
            //                                TagId = tag.Id,
            //                                RatingId = ratingId
            //                                }
            //                            },
            //    WouldTakeTeacherAgain = true
            //    };
            //using RateMyPDbContext context = new RateMyPDbContext();
            //context.Tags.Add(tag);
            //context.Ratings.Add(rating);
            //context.SaveChanges();


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
