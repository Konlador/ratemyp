using Microsoft.EntityFrameworkCore;
using RateMyP.WebApp.Models;

namespace RateMyP.WebApp
    {
    public class RateMyPDbContext : DbContext
        {
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<TeacherActivity> TeacherActivities { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<RatingLike> RatingLikes { get; set; }
        public DbSet<RatingTag> RatingTags { get; set; }
        public DbSet<Tag> Tags { get; set; }

        public RateMyPDbContext()
            : base(new DbContextOptions<RateMyPDbContext>())
            {
            }

        public RateMyPDbContext(DbContextOptions<RateMyPDbContext> options)
            : base(options)
            {
            }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlServer("Data Source=ratemyp.database.windows.net;Initial Catalog=RateMyP;User Id=koldunai; Password=abrikosas79?;");
        //("Data Source=ratemyp.database.windows.net;Initial Catalog=RateMyP;User Id=koldunai; Password=abrikosas79?;");

        protected override void OnModelCreating(ModelBuilder builder)
            {
            builder.Entity<RatingLike>().HasKey(table => new { table.RatingId, table.StudentId });
            builder.Entity<RatingTag>().HasKey(table => new { table.RatingId, table.TagId });
            }
        }
    }
