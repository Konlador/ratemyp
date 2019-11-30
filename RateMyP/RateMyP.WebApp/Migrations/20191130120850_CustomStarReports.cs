using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RateMyP.WebApp.Migrations
{
    public partial class CustomStarReports : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CustomStarReports",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CustomStarId = table.Column<Guid>(nullable: false),
                    StudentId = table.Column<string>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    Reason = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomStarReports", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomStarReports");
        }
    }
}
