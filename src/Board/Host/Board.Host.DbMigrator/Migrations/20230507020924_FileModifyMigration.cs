using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Board.Host.DbMigrator.Migrations
{
    /// <inheritdoc />
    public partial class FileModifyMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PostId",
                table: "File",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_File_PostId",
                table: "File",
                column: "PostId");

            migrationBuilder.AddForeignKey(
                name: "FK_File_Post_PostId",
                table: "File",
                column: "PostId",
                principalTable: "Post",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_File_Post_PostId",
                table: "File");

            migrationBuilder.DropIndex(
                name: "IX_File_PostId",
                table: "File");

            migrationBuilder.DropColumn(
                name: "PostId",
                table: "File");
        }
    }
}
