using Microsoft.EntityFrameworkCore.Migrations;

namespace RateMyP.WebApp.Migrations
{
    public partial class ReportPrimaryKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_RatingReports",
                table: "RatingReports");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RatingReports",
                table: "RatingReports",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_RatingReports",
                table: "RatingReports");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RatingReports",
                table: "RatingReports",
                columns: new[] { "RatingId", "StudentId" });
        }
    }
}
