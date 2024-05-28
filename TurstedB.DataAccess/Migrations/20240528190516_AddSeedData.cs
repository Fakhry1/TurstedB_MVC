using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TrustedB.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "CategoryId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "CategoryId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "CategoryId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "CategoryId",
                keyValue: 4);

            migrationBuilder.InsertData(
                table: "StateTransition",
                columns: new[] { "StateTransitionId", "RoleId", "Statefrom", "Stateto" },
                values: new object[,]
                {
                    { 1, null, 1, 2 },
                    { 2, null, 2, 3 },
                    { 3, null, 3, 4 },
                    { 4, null, 4, 5 },
                    { 5, null, 4, 1 },
                    { 6, null, 3, 1 },
                    { 7, null, 2, 1 }
                });

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
                table: "StateTransition",
                keyColumn: "StateTransitionId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "StateTransition",
                keyColumn: "StateTransitionId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "StateTransition",
                keyColumn: "StateTransitionId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "StateTransition",
                keyColumn: "StateTransitionId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "StateTransition",
                keyColumn: "StateTransitionId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "StateTransition",
                keyColumn: "StateTransitionId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "StateTransition",
                keyColumn: "StateTransitionId",
                keyValue: 7);

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

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "CategoryId", "ArabicName", "EnglishName" },
                values: new object[,]
                {
                    { 1, "توجيه", "Guide" },
                    { 2, "صور", "Images" },
                    { 3, "مرئيات", "Videos" },
                    { 4, "صوتيات", "Audio" }
                });
        }
    }
}
