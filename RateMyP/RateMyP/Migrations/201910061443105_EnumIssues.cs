namespace RateMyP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EnumIssues : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TeacherActivities", "LectureType", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TeacherActivities", "LectureType");
        }
    }
}
