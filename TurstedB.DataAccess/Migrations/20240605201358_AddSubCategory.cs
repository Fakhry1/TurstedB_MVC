using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace TrustedB.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddSubCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SubCategoryID",
                table: "Topics",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SubCategory",
                columns: table => new
                {
                    SubCategoryId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SubArabicName = table.Column<string>(type: "text", nullable: true),
                    SubEnglishName = table.Column<string>(type: "text", nullable: true),
                    CategoryID = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubCategory", x => x.SubCategoryId);
                    table.ForeignKey(
                        name: "FK_SubCategory_Category_CategoryID",
                        column: x => x.CategoryID,
                        principalTable: "Category",
                        principalColumn: "CategoryId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Topics_SubCategoryID",
                table: "Topics",
                column: "SubCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_SubCategory_CategoryID",
                table: "SubCategory",
                column: "CategoryID");

            migrationBuilder.AddForeignKey(
                name: "FK_Topics_SubCategory_SubCategoryID",
                table: "Topics",
                column: "SubCategoryID",
                principalTable: "SubCategory",
                principalColumn: "SubCategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Topics_SubCategory_SubCategoryID",
                table: "Topics");

            migrationBuilder.DropTable(
                name: "SubCategory");

            migrationBuilder.DropIndex(
                name: "IX_Topics_SubCategoryID",
                table: "Topics");

            migrationBuilder.DropColumn(
                name: "SubCategoryID",
                table: "Topics");
        }
    }
}
