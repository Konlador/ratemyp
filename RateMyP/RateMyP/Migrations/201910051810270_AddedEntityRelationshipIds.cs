namespace RateMyP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedEntityRelationshipIds : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TeacherActivities", "Teacher_Id", "dbo.Teachers");
            DropIndex("dbo.TeacherActivities", new[] { "Teacher_Id" });
            RenameColumn(table: "dbo.TeacherActivities", name: "Teacher_Id", newName: "TeacherId");
            AlterColumn("dbo.TeacherActivities", "TeacherId", c => c.Guid(nullable: false));
            CreateIndex("dbo.TeacherActivities", "TeacherId");
            AddForeignKey("dbo.TeacherActivities", "TeacherId", "dbo.Teachers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TeacherActivities", "TeacherId", "dbo.Teachers");
            DropIndex("dbo.TeacherActivities", new[] { "TeacherId" });
            AlterColumn("dbo.TeacherActivities", "TeacherId", c => c.Guid());
            RenameColumn(table: "dbo.TeacherActivities", name: "TeacherId", newName: "Teacher_Id");
            CreateIndex("dbo.TeacherActivities", "Teacher_Id");
            AddForeignKey("dbo.TeacherActivities", "Teacher_Id", "dbo.Teachers", "Id");
        }
    }
}
