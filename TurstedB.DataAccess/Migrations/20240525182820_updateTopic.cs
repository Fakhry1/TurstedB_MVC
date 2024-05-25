using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrustedB.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class updateTopic : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Topics",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Topics_ApplicationUserId",
                table: "Topics",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Topics_AspNetUsers_ApplicationUserId",
                table: "Topics",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Topics_AspNetUsers_ApplicationUserId",
                table: "Topics");

            migrationBuilder.DropIndex(
                name: "IX_Topics_ApplicationUserId",
                table: "Topics");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Topics");
        }
    }
}
