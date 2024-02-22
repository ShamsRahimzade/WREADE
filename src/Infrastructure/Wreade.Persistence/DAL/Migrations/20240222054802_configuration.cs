using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wreade.Persistence.DAL.Migrations
{
    public partial class configuration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chapters_Books_BookId",
                table: "Chapters");

            migrationBuilder.AddColumn<int>(
                name: "BookId1",
                table: "Chapters",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Chapters_BookId1",
                table: "Chapters",
                column: "BookId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Chapters_Books_BookId",
                table: "Chapters",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Chapters_Books_BookId1",
                table: "Chapters",
                column: "BookId1",
                principalTable: "Books",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chapters_Books_BookId",
                table: "Chapters");

            migrationBuilder.DropForeignKey(
                name: "FK_Chapters_Books_BookId1",
                table: "Chapters");

            migrationBuilder.DropIndex(
                name: "IX_Chapters_BookId1",
                table: "Chapters");

            migrationBuilder.DropColumn(
                name: "BookId1",
                table: "Chapters");

            migrationBuilder.AddForeignKey(
                name: "FK_Chapters_Books_BookId",
                table: "Chapters",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
