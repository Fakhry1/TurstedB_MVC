using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrustedB.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDate5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StateHistory_AspNetUsers_ApplicationUserId",
                table: "StateHistory");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "StateHistory");

            migrationBuilder.AlterColumn<string>(
                name: "CreationDate",
                table: "Topics",
                type: "text",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                table: "StateHistory",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddForeignKey(
                name: "FK_StateHistory_AspNetUsers_ApplicationUserId",
                table: "StateHistory",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StateHistory_AspNetUsers_ApplicationUserId",
                table: "StateHistory");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "Topics",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                table: "StateHistory",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "StateHistory",
                type: "text",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_StateHistory_AspNetUsers_ApplicationUserId",
                table: "StateHistory",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
