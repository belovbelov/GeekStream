using Microsoft.EntityFrameworkCore.Migrations;

namespace GeekStream.Infrastructure.Migrations
{
    public partial class AddImageToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Files_ImageFilePathId",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Files_AspNetUsers_UserId",
                table: "Files");

            migrationBuilder.DropIndex(
                name: "IX_Files_UserId",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Files");

            migrationBuilder.RenameColumn(
                name: "FilePathId",
                table: "Files",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ImageFilePathId",
                table: "Categories",
                newName: "ImageId");

            migrationBuilder.RenameIndex(
                name: "IX_Categories_ImageFilePathId",
                table: "Categories",
                newName: "IX_Categories_ImageId");

            migrationBuilder.AddColumn<int>(
                name: "AvatarId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_AvatarId",
                table: "AspNetUsers",
                column: "AvatarId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Files_AvatarId",
                table: "AspNetUsers",
                column: "AvatarId",
                principalTable: "Files",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Files_ImageId",
                table: "Categories",
                column: "ImageId",
                principalTable: "Files",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Files_AvatarId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Files_ImageId",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_AvatarId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "AvatarId",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Files",
                newName: "FilePathId");

            migrationBuilder.RenameColumn(
                name: "ImageId",
                table: "Categories",
                newName: "ImageFilePathId");

            migrationBuilder.RenameIndex(
                name: "IX_Categories_ImageId",
                table: "Categories",
                newName: "IX_Categories_ImageFilePathId");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Files",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Files_UserId",
                table: "Files",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Files_ImageFilePathId",
                table: "Categories",
                column: "ImageFilePathId",
                principalTable: "Files",
                principalColumn: "FilePathId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Files_AspNetUsers_UserId",
                table: "Files",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
