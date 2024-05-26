using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrustedB.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddstateIdInAttachment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "stateId",
                table: "Attachments",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Attachments_stateId",
                table: "Attachments",
                column: "stateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attachments_TopicsStates_stateId",
                table: "Attachments",
                column: "stateId",
                principalTable: "TopicsStates",
                principalColumn: "stateId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attachments_TopicsStates_stateId",
                table: "Attachments");

            migrationBuilder.DropIndex(
                name: "IX_Attachments_stateId",
                table: "Attachments");

            migrationBuilder.DropColumn(
                name: "stateId",
                table: "Attachments");
        }
    }
}
