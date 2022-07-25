using Microsoft.EntityFrameworkCore.Migrations;

namespace TradingTools.DAL.Migrations
{
    public partial class AddTradeChallengeProspect : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TradeChallengeProspect",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TradeChallengeId = table.Column<int>(type: "int", nullable: false),
                    CalculatorStateId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TradeChallengeProspect", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TradeChallengeProspect_CalculatorState_CalculatorStateId",
                        column: x => x.CalculatorStateId,
                        principalTable: "CalculatorState",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TradeChallengeProspect_TradeChallenge_TradeChallengeId",
                        column: x => x.TradeChallengeId,
                        principalTable: "TradeChallenge",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TradeChallengeProspect_CalculatorStateId",
                table: "TradeChallengeProspect",
                column: "CalculatorStateId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TradeChallengeProspect_TradeChallengeId",
                table: "TradeChallengeProspect",
                column: "TradeChallengeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TradeChallengeProspect");
        }
    }
}
