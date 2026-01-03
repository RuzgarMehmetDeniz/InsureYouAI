using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InsureYouAI.Migrations
{
    /// <inheritdoc />
    public partial class mig7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PricingPlanItems",
                columns: table => new
                {
                    PricingPlanItemID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PricingPlanID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PricingPlanItems", x => x.PricingPlanItemID);
                    table.ForeignKey(
                        name: "FK_PricingPlanItems_PricingPlans_PricingPlanID",
                        column: x => x.PricingPlanID,
                        principalTable: "PricingPlans",
                        principalColumn: "PricingPlanID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PricingPlanItems_PricingPlanID",
                table: "PricingPlanItems",
                column: "PricingPlanID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PricingPlanItems");
        }
    }
}
