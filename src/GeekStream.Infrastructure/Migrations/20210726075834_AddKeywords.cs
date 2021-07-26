using Microsoft.EntityFrameworkCore.Migrations;

namespace GeekStream.Infrastructure.Migrations
{
    public partial class AddKeywords : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArticleKeyword");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Keywords",
                table: "Keywords");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Keywords");

            migrationBuilder.AlterColumn<string>(
                name: "Word",
                table: "Keywords",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "ArticleId",
                table: "Keywords",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Keywords",
                table: "Keywords",
                column: "Word");

            migrationBuilder.CreateIndex(
                name: "IX_Keywords_ArticleId",
                table: "Keywords",
                column: "ArticleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Keywords_Articles_ArticleId",
                table: "Keywords",
                column: "ArticleId",
                principalTable: "Articles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Keywords_Articles_ArticleId",
                table: "Keywords");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Keywords",
                table: "Keywords");

            migrationBuilder.DropIndex(
                name: "IX_Keywords_ArticleId",
                table: "Keywords");

            migrationBuilder.DropColumn(
                name: "ArticleId",
                table: "Keywords");

            migrationBuilder.AlterColumn<string>(
                name: "Word",
                table: "Keywords",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Keywords",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Keywords",
                table: "Keywords",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ArticleKeyword",
                columns: table => new
                {
                    ArticlesId = table.Column<int>(type: "int", nullable: false),
                    KeywordsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleKeyword", x => new { x.ArticlesId, x.KeywordsId });
                    table.ForeignKey(
                        name: "FK_ArticleKeyword_Articles_ArticlesId",
                        column: x => x.ArticlesId,
                        principalTable: "Articles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArticleKeyword_Keywords_KeywordsId",
                        column: x => x.KeywordsId,
                        principalTable: "Keywords",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArticleKeyword_KeywordsId",
                table: "ArticleKeyword",
                column: "KeywordsId");
        }
    }
}
