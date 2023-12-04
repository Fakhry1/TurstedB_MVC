using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrustedB.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddSataeHistoryTableupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "StateHistory");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "StateHistory",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_StateHistory_ApplicationUserId",
                table: "StateHistory",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_StateHistory_AspNetUsers_ApplicationUserId",
                table: "StateHistory",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StateHistory_AspNetUsers_ApplicationUserId",
                table: "StateHistory");

            migrationBuilder.DropIndex(
                name: "IX_StateHistory_ApplicationUserId",
                table: "StateHistory");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "StateHistory");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "StateHistory",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
