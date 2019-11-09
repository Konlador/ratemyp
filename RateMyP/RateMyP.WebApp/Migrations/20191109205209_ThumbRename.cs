using Microsoft.EntityFrameworkCore.Migrations;

namespace RateMyP.WebApp.Migrations
{
    public partial class ThumbRename : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ThumbsUp",
                table: "RatingThumbs");

            migrationBuilder.AddColumn<bool>(
                name: "ThumbUp",
                table: "RatingThumbs",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ThumbUp",
                table: "RatingThumbs");

            migrationBuilder.AddColumn<bool>(
                name: "ThumbsUp",
                table: "RatingThumbs",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
