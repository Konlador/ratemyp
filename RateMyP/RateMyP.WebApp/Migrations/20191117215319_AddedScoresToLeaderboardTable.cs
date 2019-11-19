using Microsoft.EntityFrameworkCore.Migrations;

namespace RateMyP.WebApp.Migrations
{
    public partial class AddedScoresToLeaderboardTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "AllTimeScore",
                table: "Leaderboard",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "ThisYearScore",
                table: "Leaderboard",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AllTimeScore",
                table: "Leaderboard");

            migrationBuilder.DropColumn(
                name: "ThisYearScore",
                table: "Leaderboard");
        }
    }
}
