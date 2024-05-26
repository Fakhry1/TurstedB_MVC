using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TrustedB.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddSeedToTransitionStates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }
    }
}
