using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InsureYouAI.Migrations
{
    /// <inheritdoc />
    public partial class mig2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Categories_CategoryID1",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Categories_CategoryID1",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "CategoryID1",
                table: "Categories");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryID1",
                table: "Categories",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Categories_CategoryID1",
                table: "Categories",
                column: "CategoryID1");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Categories_CategoryID1",
                table: "Categories",
                column: "CategoryID1",
                principalTable: "Categories",
                principalColumn: "CategoryID");
        }
    }
}
