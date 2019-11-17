using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RateMyP.WebApp.Migrations
{
    public partial class LeaderboardTableChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseLeaderboardEntries");

            migrationBuilder.DropTable(
                name: "TeacherLeaderboardEntries");

            migrationBuilder.CreateTable(
                name: "Leaderboard",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    EntryType = table.Column<int>(nullable: false),
                    AllTimePosition = table.Column<int>(nullable: false),
                    AllTimeRatingCount = table.Column<int>(nullable: false),
                    AllTimeAverage = table.Column<double>(nullable: false),
                    ThisYearPosition = table.Column<int>(nullable: false),
                    ThisYearRatingCount = table.Column<int>(nullable: false),
                    ThisYearAverage = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Leaderboard", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Leaderboard");

            migrationBuilder.CreateTable(
                name: "CourseLeaderboardEntries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AllTimeAverage = table.Column<double>(type: "float", nullable: false),
                    AllTimePosition = table.Column<int>(type: "int", nullable: false),
                    AllTimeRatingCount = table.Column<int>(type: "int", nullable: false),
                    ThisYearAverage = table.Column<double>(type: "float", nullable: false),
                    ThisYearPosition = table.Column<int>(type: "int", nullable: false),
                    ThisYearRatingCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseLeaderboardEntries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TeacherLeaderboardEntries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AllTimeAverage = table.Column<double>(type: "float", nullable: false),
                    AllTimePosition = table.Column<int>(type: "int", nullable: false),
                    AllTimeRatingCount = table.Column<int>(type: "int", nullable: false),
                    ThisYearAverage = table.Column<double>(type: "float", nullable: false),
                    ThisYearPosition = table.Column<int>(type: "int", nullable: false),
                    ThisYearRatingCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherLeaderboardEntries", x => x.Id);
                });
        }
    }
}
