using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Board.Host.DbMigrator.Migrations
{
    /// <inheritdoc />
    public partial class Migration2Dop : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answer_Account_AccountId",
                table: "Answer");

            migrationBuilder.DropIndex(
                name: "IX_Answer_AccountId",
                table: "Answer");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "Answer");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "Answer",
                type: "character varying(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "Answer",
                type: "character varying(800)",
                maxLength: 800,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Account",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Answer_AccId",
                table: "Answer",
                column: "AccId");

            migrationBuilder.AddForeignKey(
                name: "FK_Answer_Account_AccId",
                table: "Answer",
                column: "AccId",
                principalTable: "Account",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answer_Account_AccId",
                table: "Answer");

            migrationBuilder.DropIndex(
                name: "IX_Answer_AccId",
                table: "Answer");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Account");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "Answer",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(256)",
                oldMaxLength: 256);

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "Answer",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(800)",
                oldMaxLength: 800);

            migrationBuilder.AddColumn<Guid>(
                name: "AccountId",
                table: "Answer",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Answer_AccountId",
                table: "Answer",
                column: "AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Answer_Account_AccountId",
                table: "Answer",
                column: "AccountId",
                principalTable: "Account",
                principalColumn: "Id");
        }
    }
}
