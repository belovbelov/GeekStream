﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace GeekStream.Infrastructure.Migrations
{
    public partial class ArticleType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Articles",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "Articles");
        }
    }
}
