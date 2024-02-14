using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wreade.Persistence.DAL.Migrations
{
    public partial class chapterimagecolumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ChapterViewCounts_ChapterId",
                table: "ChapterViewCounts");

            migrationBuilder.AddColumn<string>(
                name: "ChapterImage",
                table: "Chapters",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_ChapterViewCounts_ChapterId",
                table: "ChapterViewCounts",
                column: "ChapterId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ChapterViewCounts_ChapterId",
                table: "ChapterViewCounts");

            migrationBuilder.DropColumn(
                name: "ChapterImage",
                table: "Chapters");

            migrationBuilder.CreateIndex(
                name: "IX_ChapterViewCounts_ChapterId",
                table: "ChapterViewCounts",
                column: "ChapterId");
        }
    }
}
