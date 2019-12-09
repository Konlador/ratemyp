using Microsoft.EntityFrameworkCore.Migrations;

namespace RateMyP.WebApp.Migrations
{
    public partial class StudentPoints : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Points",
                table: "Students",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CustomStarReports_CustomStarId",
                table: "CustomStarReports",
                column: "CustomStarId");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomStarReports_CustomStars_CustomStarId",
                table: "CustomStarReports",
                column: "CustomStarId",
                principalTable: "CustomStars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomStarReports_CustomStars_CustomStarId",
                table: "CustomStarReports");

            migrationBuilder.DropIndex(
                name: "IX_CustomStarReports_CustomStarId",
                table: "CustomStarReports");

            migrationBuilder.DropColumn(
                name: "Points",
                table: "Students");
        }
    }
}
