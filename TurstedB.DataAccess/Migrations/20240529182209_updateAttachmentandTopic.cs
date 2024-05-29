using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrustedB.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class updateAttachmentandTopic : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MainFile",
                table: "Topics",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MainFile",
                table: "Attachments",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MainFile",
                table: "Topics");

            migrationBuilder.DropColumn(
                name: "MainFile",
                table: "Attachments");
        }
    }
}
