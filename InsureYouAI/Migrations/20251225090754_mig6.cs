using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InsureYouAI.Migrations
{
    /// <inheritdoc />
    public partial class mig6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ArticleID",
                table: "Comments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ArticleID",
                table: "Comments",
                column: "ArticleID");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Articles_ArticleID",
                table: "Comments",
                column: "ArticleID",
                principalTable: "Articles",
                principalColumn: "ArticleID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Articles_ArticleID",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_ArticleID",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "ArticleID",
                table: "Comments");
        }
    }
}
