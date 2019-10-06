namespace RateMyP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Linked : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CommentLikes",
                c => new
                    {
                        CommentId = c.Guid(nullable: false),
                        StudentId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.CommentId, t.StudentId })
                .ForeignKey("dbo.Comments", t => t.CommentId, cascadeDelete: true)
                .ForeignKey("dbo.Students", t => t.StudentId, cascadeDelete: true)
                .Index(t => t.CommentId)
                .Index(t => t.StudentId);
            
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CommentOnId = c.Guid(nullable: false),
                        CommentOnType = c.Int(nullable: false),
                        Content = c.String(),
                        Likes = c.Int(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        Student_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Students", t => t.Student_Id)
                .Index(t => t.Student_Id);
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Studies = c.String(),
                        Faculty = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        Type = c.Int(nullable: false),
                        Credits = c.Int(nullable: false),
                        Faculty = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Ratings",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        OverallMark = c.Int(nullable: false),
                        LevelOfDifficulty = c.Int(nullable: false),
                        WouldTakeTeacherAgain = c.Boolean(nullable: false),
                        Tags = c.String(),
                        DateCreated = c.DateTime(nullable: false),
                        Comment = c.String(),
                        Course_Id = c.Guid(),
                        Student_Id = c.Guid(),
                        Teacher_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Courses", t => t.Course_Id)
                .ForeignKey("dbo.Students", t => t.Student_Id)
                .ForeignKey("dbo.Teachers", t => t.Teacher_Id)
                .Index(t => t.Course_Id)
                .Index(t => t.Student_Id)
                .Index(t => t.Teacher_Id);
            
            CreateTable(
                "dbo.Teachers",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Description = c.String(),
                        Rank = c.String(),
                        Faculty = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TeacherActivities",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        TeacherId = c.Guid(nullable: false),
                        CourseId = c.Guid(nullable: false),
                        DateStarted = c.DateTime(nullable: false),
                        Type = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Courses", t => t.CourseId, cascadeDelete: true)
                .ForeignKey("dbo.Teachers", t => t.TeacherId, cascadeDelete: true)
                .Index(t => t.TeacherId)
                .Index(t => t.CourseId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TeacherActivities", "TeacherId", "dbo.Teachers");
            DropForeignKey("dbo.TeacherActivities", "CourseId", "dbo.Courses");
            DropForeignKey("dbo.Ratings", "Teacher_Id", "dbo.Teachers");
            DropForeignKey("dbo.Ratings", "Student_Id", "dbo.Students");
            DropForeignKey("dbo.Ratings", "Course_Id", "dbo.Courses");
            DropForeignKey("dbo.CommentLikes", "StudentId", "dbo.Students");
            DropForeignKey("dbo.CommentLikes", "CommentId", "dbo.Comments");
            DropForeignKey("dbo.Comments", "Student_Id", "dbo.Students");
            DropIndex("dbo.TeacherActivities", new[] { "CourseId" });
            DropIndex("dbo.TeacherActivities", new[] { "TeacherId" });
            DropIndex("dbo.Ratings", new[] { "Teacher_Id" });
            DropIndex("dbo.Ratings", new[] { "Student_Id" });
            DropIndex("dbo.Ratings", new[] { "Course_Id" });
            DropIndex("dbo.Comments", new[] { "Student_Id" });
            DropIndex("dbo.CommentLikes", new[] { "StudentId" });
            DropIndex("dbo.CommentLikes", new[] { "CommentId" });
            DropTable("dbo.TeacherActivities");
            DropTable("dbo.Teachers");
            DropTable("dbo.Ratings");
            DropTable("dbo.Courses");
            DropTable("dbo.Students");
            DropTable("dbo.Comments");
            DropTable("dbo.CommentLikes");
        }
    }
}
