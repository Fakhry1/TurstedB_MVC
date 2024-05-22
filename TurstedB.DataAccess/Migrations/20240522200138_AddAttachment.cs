using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrustedB.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddAttachment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileSize",
                table: "Topics");

            migrationBuilder.DropColumn(
                name: "TopicFile",
                table: "Topics");

            migrationBuilder.CreateTable(
                name: "Attachments",
                columns: table => new
                {
                    FileId = table.Column<Guid>(type: "uuid", nullable: false),
                    FileName = table.Column<string>(type: "text", nullable: true),
                    FilePath = table.Column<string>(type: "text", nullable: true),
                    FileType = table.Column<string>(type: "text", nullable: true),
                    TopicId = table.Column<Guid>(type: "uuid", nullable: true),
                    ApplicationUserId = table.Column<string>(type: "text", nullable: true),
                    AttachmentSetDate = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attachments", x => x.FileId);
                    table.ForeignKey(
                        name: "FK_Attachments_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Attachments_Topics_TopicId",
                        column: x => x.TopicId,
                        principalTable: "Topics",
                        principalColumn: "TopicId");
                });

            migrationBuilder.CreateTable(
                name: "CommentHistory",
                columns: table => new
                {
                    CommentHistoryId = table.Column<Guid>(type: "uuid", nullable: false),
                    Comment = table.Column<string>(type: "text", nullable: false),
                    TopicId = table.Column<Guid>(type: "uuid", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "text", nullable: false),
                    CommentSetDate = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommentHistory", x => x.CommentHistoryId);
                    table.ForeignKey(
                        name: "FK_CommentHistory_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CommentHistory_Topics_TopicId",
                        column: x => x.TopicId,
                        principalTable: "Topics",
                        principalColumn: "TopicId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Attachments_ApplicationUserId",
                table: "Attachments",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Attachments_TopicId",
                table: "Attachments",
                column: "TopicId");

            migrationBuilder.CreateIndex(
                name: "IX_CommentHistory_ApplicationUserId",
                table: "CommentHistory",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_CommentHistory_TopicId",
                table: "CommentHistory",
                column: "TopicId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Attachments");

            migrationBuilder.DropTable(
                name: "CommentHistory");

            migrationBuilder.AddColumn<string>(
                name: "FileSize",
                table: "Topics",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TopicFile",
                table: "Topics",
                type: "text",
                nullable: true);
        }
    }
}
