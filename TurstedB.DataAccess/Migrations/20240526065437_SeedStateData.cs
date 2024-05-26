using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TrustedB.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class SeedStateData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "TopicsStates",
                columns: new[] { "stateId", "ArabicName", "EnglishName" },
                values: new object[,]
                {
                    { 1, "قيد اعداد", "Current" },
                    { 2, "اعتماد", "Approved" },
                    { 3, "تصحيح لغوي", "Check Lang" },
                    { 4, "اعتماد تصميم", "Approve Desgin" },
                    { 5, "نشر", "Publish" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TopicsStates",
                keyColumn: "stateId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TopicsStates",
                keyColumn: "stateId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "TopicsStates",
                keyColumn: "stateId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "TopicsStates",
                keyColumn: "stateId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "TopicsStates",
                keyColumn: "stateId",
                keyValue: 5);
        }
    }
}
