using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RateMyP.WebApp.Migrations
{
    public partial class ReportsReworked : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Merchandise",
                table: "Merchandise");

            migrationBuilder.RenameTable(
                name: "Merchandise",
                newName: "Merchandises");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Merchandises",
                table: "Merchandises",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "MerchandiseOrders",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    StudentId = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Price = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MerchandiseOrders", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RatingReports_RatingId",
                table: "RatingReports",
                column: "RatingId");

            migrationBuilder.AddForeignKey(
                name: "FK_RatingReports_Ratings_RatingId",
                table: "RatingReports",
                column: "RatingId",
                principalTable: "Ratings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RatingReports_Ratings_RatingId",
                table: "RatingReports");

            migrationBuilder.DropTable(
                name: "MerchandiseOrders");

            migrationBuilder.DropIndex(
                name: "IX_RatingReports_RatingId",
                table: "RatingReports");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Merchandises",
                table: "Merchandises");

            migrationBuilder.RenameTable(
                name: "Merchandises",
                newName: "Merchandise");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Merchandise",
                table: "Merchandise",
                column: "Id");
        }
    }
}
