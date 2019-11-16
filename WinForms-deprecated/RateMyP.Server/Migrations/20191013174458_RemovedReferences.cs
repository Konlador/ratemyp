using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RateMyP.Server.Migrations
{
    public partial class RemovedReferences : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RatingLikes_Ratings_RatingId",
                table: "RatingLikes");

            migrationBuilder.DropForeignKey(
                name: "FK_RatingLikes_Students_StudentId",
                table: "RatingLikes");

            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_Courses_CourseId",
                table: "Ratings");

            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_Students_StudentId",
                table: "Ratings");

            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_Teachers_TeacherId",
                table: "Ratings");

            migrationBuilder.DropForeignKey(
                name: "FK_TeacherActivities_Courses_CourseId",
                table: "TeacherActivities");

            migrationBuilder.DropForeignKey(
                name: "FK_TeacherTag_Teachers_TeacherId",
                table: "TeacherTag");

            migrationBuilder.DropIndex(
                name: "IX_TeacherTag_TeacherId",
                table: "TeacherTag");

            migrationBuilder.DropIndex(
                name: "IX_TeacherActivities_CourseId",
                table: "TeacherActivities");

            migrationBuilder.DropIndex(
                name: "IX_Ratings_CourseId",
                table: "Ratings");

            migrationBuilder.DropIndex(
                name: "IX_Ratings_StudentId",
                table: "Ratings");

            migrationBuilder.DropIndex(
                name: "IX_Ratings_TeacherId",
                table: "Ratings");

            migrationBuilder.DropIndex(
                name: "IX_RatingLikes_StudentId",
                table: "RatingLikes");

            migrationBuilder.AlterColumn<Guid>(
                name: "TeacherId",
                table: "TeacherTag",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "TeacherId",
                table: "Ratings",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "StudentId",
                table: "Ratings",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "CourseId",
                table: "Ratings",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "TeacherId",
                table: "TeacherTag",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<Guid>(
                name: "TeacherId",
                table: "Ratings",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<Guid>(
                name: "StudentId",
                table: "Ratings",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<Guid>(
                name: "CourseId",
                table: "Ratings",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.CreateIndex(
                name: "IX_TeacherTag_TeacherId",
                table: "TeacherTag",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherActivities_CourseId",
                table: "TeacherActivities",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_CourseId",
                table: "Ratings",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_StudentId",
                table: "Ratings",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_TeacherId",
                table: "Ratings",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_RatingLikes_StudentId",
                table: "RatingLikes",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_RatingLikes_Ratings_RatingId",
                table: "RatingLikes",
                column: "RatingId",
                principalTable: "Ratings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RatingLikes_Students_StudentId",
                table: "RatingLikes",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_Courses_CourseId",
                table: "Ratings",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_Students_StudentId",
                table: "Ratings",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_Teachers_TeacherId",
                table: "Ratings",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherActivities_Courses_CourseId",
                table: "TeacherActivities",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherTag_Teachers_TeacherId",
                table: "TeacherTag",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
