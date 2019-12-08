using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RateMyP.WebApp.Migrations
{
    public partial class LeaderboardRework : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Leaderboard");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "MerchandiseOrders");

            migrationBuilder.CreateTable(
                name: "CourseLeaderboard",
                columns: table => new
                {
                    CourseId = table.Column<Guid>(nullable: false),
                    AllTimePosition = table.Column<int>(nullable: false),
                    AllTimeRatingCount = table.Column<int>(nullable: false),
                    AllTimeAverage = table.Column<double>(nullable: false),
                    AllTimeScore = table.Column<double>(nullable: false),
                    ThisYearPosition = table.Column<int>(nullable: false),
                    ThisYearRatingCount = table.Column<int>(nullable: false),
                    ThisYearAverage = table.Column<double>(nullable: false),
                    ThisYearScore = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseLeaderboard", x => x.CourseId);
                    table.ForeignKey(
                        name: "FK_CourseLeaderboard_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TeacherLeaderboard",
                columns: table => new
                {
                    TeacherId = table.Column<Guid>(nullable: false),
                    AllTimePosition = table.Column<int>(nullable: false),
                    AllTimeRatingCount = table.Column<int>(nullable: false),
                    AllTimeAverage = table.Column<double>(nullable: false),
                    AllTimeScore = table.Column<double>(nullable: false),
                    ThisYearPosition = table.Column<int>(nullable: false),
                    ThisYearRatingCount = table.Column<int>(nullable: false),
                    ThisYearAverage = table.Column<double>(nullable: false),
                    ThisYearScore = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherLeaderboard", x => x.TeacherId);
                    table.ForeignKey(
                        name: "FK_TeacherLeaderboard_Teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseLeaderboard");

            migrationBuilder.DropTable(
                name: "TeacherLeaderboard");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "MerchandiseOrders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Leaderboard",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AllTimeAverage = table.Column<double>(type: "float", nullable: false),
                    AllTimePosition = table.Column<int>(type: "int", nullable: false),
                    AllTimeRatingCount = table.Column<int>(type: "int", nullable: false),
                    AllTimeScore = table.Column<double>(type: "float", nullable: false),
                    EntryType = table.Column<int>(type: "int", nullable: false),
                    ThisYearAverage = table.Column<double>(type: "float", nullable: false),
                    ThisYearPosition = table.Column<int>(type: "int", nullable: false),
                    ThisYearRatingCount = table.Column<int>(type: "int", nullable: false),
                    ThisYearScore = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Leaderboard", x => x.Id);
                });
        }
    }
}
