namespace RateMyP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ForeignKeyIssues : DbMigration
    {
        public override void Up()
        {
            RenameIndex(table: "dbo.TeacherActivities", name: "IX_Course_Id", newName: "IX_CourseId");
            RenameColumn(table: "dbo.TeacherActivities", name: "Course_Id", newName: "CourseId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.TeacherActivities", name: "IX_Course_Id", newName: "IX_CourseId");
            RenameColumn(table: "dbo.TeacherActivities", name: "Course_Id", newName: "CourseId");
        }
    }
}
