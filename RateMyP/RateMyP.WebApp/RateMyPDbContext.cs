using Microsoft.EntityFrameworkCore;
using RateMyP.WebApp.Models;
using Microsoft.Extensions.Configuration;

namespace RateMyP.WebApp
    {
    public interface IRateMyPDbContext
        {
        DbSet<Teacher> Teachers { get; set; }
        DbSet<TeacherActivity> TeacherActivities { get; set; }
        DbSet<Course> Courses { get; set; }
        DbSet<Rating> Ratings { get; set; }
        DbSet<Student> Students { get; set; }
        DbSet<RatingThumb> RatingThumbs { get; set; }
        DbSet<RatingTag> RatingTags { get; set; }
        DbSet<Tag> Tags { get; set; }
        DbSet<RatingReport> RatingReports { get; set; }
        DbSet<CustomStarReport> CustomStarReports { get; set; }
        DbSet<LeaderboardEntry> Leaderboard { get; set; }
        DbSet<CustomStar> CustomStars { get; set; }
        DbSet<CustomStarThumb> CustomStarThumbs { get; set; }
        }

    public class RateMyPDbContext : DbContext, IRateMyPDbContext
        {
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<TeacherActivity> TeacherActivities { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<RatingThumb> RatingThumbs { get; set; }
        public DbSet<RatingTag> RatingTags { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<RatingReport> RatingReports { get; set; }
        public DbSet<CustomStarReport> CustomStarReports { get; set; }
        public DbSet<LeaderboardEntry> Leaderboard { get; set; }
        public DbSet<CustomStar> CustomStars { get; set; }
        public DbSet<CustomStarThumb> CustomStarThumbs { get; set; }

        public RateMyPDbContext()
            : base(new DbContextOptions<RateMyPDbContext>())
            {
            }

        public RateMyPDbContext(DbContextOptions<RateMyPDbContext> options)
            : base(options)
            {
            }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlServer("Data Source=ratemyp.database.windows.net;Initial Catalog=RateMyP;User Id=koldunai; Password=abrikosas79?;MultipleActiveResultSets=true;");

        protected override void OnModelCreating(ModelBuilder builder)
            {
            builder.Entity<RatingThumb>().HasKey(table => new { table.RatingId, table.StudentId });
            builder.Entity<CustomStarThumb>().HasKey(table => new { table.CustomStarId, table.StudentId });
            builder.Entity<RatingTag>().HasKey(table => new { table.RatingId, table.TagId });
            }
        }
    }
