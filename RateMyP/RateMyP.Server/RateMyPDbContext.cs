using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using CsvHelper;
using RateMyP.Entities;
using Microsoft.EntityFrameworkCore;

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

        protected override void OnModelCreating(ModelBuilder builder)
            {
            builder.Entity<CommentLike>().HasKey(table => new { table.CommentId, table.StudentId });
            }
        }
    }
