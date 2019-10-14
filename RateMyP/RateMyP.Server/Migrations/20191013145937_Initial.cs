using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RateMyP.Server.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    CourseType = table.Column<int>(nullable: false),
                    Credits = table.Column<int>(nullable: false),
                    Faculty = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Studies = table.Column<string>(nullable: true),
                    Faculty = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tag",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Text = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tag", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Teachers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Rank = table.Column<string>(nullable: true),
                    Faculty = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teachers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ratings",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TeacherId = table.Column<Guid>(nullable: true),
                    StudentId = table.Column<Guid>(nullable: true),
                    CourseId = table.Column<Guid>(nullable: true),
                    OverallMark = table.Column<int>(nullable: false),
                    LevelOfDifficulty = table.Column<int>(nullable: false),
                    WouldTakeTeacherAgain = table.Column<bool>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    Comment = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ratings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ratings_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ratings_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ratings_Teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TeacherActivities",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DateStarted = table.Column<DateTime>(nullable: false),
                    LectureType = table.Column<int>(nullable: false),
                    CourseId = table.Column<Guid>(nullable: false),
                    TeacherId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherActivities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeacherActivities_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeacherActivities_Teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RatingLikes",
                columns: table => new
                {
                    RatingId = table.Column<Guid>(nullable: false),
                    StudentId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RatingLikes", x => new { x.RatingId, x.StudentId });
                    table.ForeignKey(
                        name: "FK_RatingLikes_Ratings_RatingId",
                        column: x => x.RatingId,
                        principalTable: "Ratings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RatingLikes_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TeacherTag",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TeacherId = table.Column<Guid>(nullable: true),
                    TagId = table.Column<Guid>(nullable: true),
                    Count = table.Column<int>(nullable: false),
                    RatingId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherTag", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeacherTag_Ratings_RatingId",
                        column: x => x.RatingId,
                        principalTable: "Ratings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TeacherTag_Tag_TagId",
                        column: x => x.TagId,
                        principalTable: "Tag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TeacherTag_Teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RatingLikes_StudentId",
                table: "RatingLikes",
                column: "StudentId");

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
                name: "IX_TeacherActivities_CourseId",
                table: "TeacherActivities",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherActivities_TeacherId",
                table: "TeacherActivities",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherTag_RatingId",
                table: "TeacherTag",
                column: "RatingId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherTag_TagId",
                table: "TeacherTag",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherTag_TeacherId",
                table: "TeacherTag",
                column: "TeacherId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RatingLikes");

            migrationBuilder.DropTable(
                name: "TeacherActivities");

            migrationBuilder.DropTable(
                name: "TeacherTag");

            migrationBuilder.DropTable(
                name: "Ratings");

            migrationBuilder.DropTable(
                name: "Tag");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Teachers");
        }
    }
}
