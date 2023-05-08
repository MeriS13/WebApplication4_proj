using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Board.Host.DbMigrator.Migrations
{
    /// <inheritdoc />
    public partial class FileUpdMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AccountId",
                table: "File",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_File_AccountId",
                table: "File",
                column: "AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_File_Account_AccountId",
                table: "File",
                column: "AccountId",
                principalTable: "Account",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_File_Account_AccountId",
                table: "File");

            migrationBuilder.DropIndex(
                name: "IX_File_AccountId",
                table: "File");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "File");
        }
    }
}
