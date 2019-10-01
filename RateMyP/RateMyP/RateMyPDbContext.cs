using RateMyP.Entities;
using System.Data.Entity;

namespace RateMyP
    {
    public class RateMyPDbContext : DbContext
        {
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<TeacherActivity> TeacherActivities { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<CommentLike> CommentLikes { get; set; }
        }
    }
