namespace RateMyP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DoubleForeignKeyHandling : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TeacherActivities", "CourseId", "dbo.Courses");
            DropForeignKey("dbo.TeacherActivities", "TeacherId", "dbo.Teachers");
            AddForeignKey("dbo.TeacherActivities", "CourseId", "dbo.Courses", "Id");
            AddForeignKey("dbo.TeacherActivities", "TeacherId", "dbo.Teachers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TeacherActivities", "TeacherId", "dbo.Teachers");
            DropForeignKey("dbo.TeacherActivities", "CourseId", "dbo.Courses");
            AddForeignKey("dbo.TeacherActivities", "TeacherId", "dbo.Teachers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.TeacherActivities", "CourseId", "dbo.Courses", "Id", cascadeDelete: true);
        }
    }
}
