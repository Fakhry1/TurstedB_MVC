using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrustedB.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UptdateComments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "stateId",
                table: "CommentHistory",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CommentHistory_stateId",
                table: "CommentHistory",
                column: "stateId");

            migrationBuilder.AddForeignKey(
                name: "FK_CommentHistory_TopicsStates_stateId",
                table: "CommentHistory",
                column: "stateId",
                principalTable: "TopicsStates",
                principalColumn: "stateId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommentHistory_TopicsStates_stateId",
                table: "CommentHistory");

            migrationBuilder.DropIndex(
                name: "IX_CommentHistory_stateId",
                table: "CommentHistory");

            migrationBuilder.DropColumn(
                name: "stateId",
                table: "CommentHistory");
        }
    }
}
