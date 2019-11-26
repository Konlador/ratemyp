using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RateMyP.WebApp.Migrations
{
    public partial class CustomStarRatings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CustomStarRatings",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TeacherId = table.Column<Guid>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    StudentId = table.Column<string>(nullable: true),
                    ThumbUps = table.Column<int>(nullable: false),
                    ThumbDowns = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomStarRatings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CustomStarThumbs",
                columns: table => new
                {
                    CustomStarId = table.Column<Guid>(nullable: false),
                    StudentId = table.Column<string>(nullable: false),
                    ThumbUp = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomStarThumbs", x => new { x.CustomStarId, x.StudentId });
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomStarRatings");

            migrationBuilder.DropTable(
                name: "CustomStarThumbs");
        }
    }
}
