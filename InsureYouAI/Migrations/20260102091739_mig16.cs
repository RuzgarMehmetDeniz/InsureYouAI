using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InsureYouAI.Migrations
{
    /// <inheritdoc />
    public partial class mig16 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MessageDetail",
                table: "Messages",
                newName: "MessagetDetail");

            migrationBuilder.AddColumn<string>(
                name: "AICategory",
                table: "Messages",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Priority",
                table: "Messages",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AICategory",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "Priority",
                table: "Messages");

            migrationBuilder.RenameColumn(
                name: "MessagetDetail",
                table: "Messages",
                newName: "MessageDetail");
        }
    }
}
