using Microsoft.EntityFrameworkCore.Migrations;

namespace RateMyP.WebApp.Migrations
{
    public partial class TagTypesRename : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TagType",
                table: "Tags");

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Tags",
                nullable: false,
                defaultValue: 3);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "Tags");

            migrationBuilder.AddColumn<int>(
                name: "TagType",
                table: "Tags",
                type: "int",
                nullable: false,
                defaultValue: 3);
        }
    }
}
