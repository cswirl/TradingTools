using Microsoft.EntityFrameworkCore.Migrations;

namespace TradingTools.Repository.Migrations
{
    public partial class RenameTablesToSingular : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CalculatorStates_Trades_TradeId",
                table: "CalculatorStates");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Trades",
                table: "Trades");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CalculatorStates",
                table: "CalculatorStates");

            migrationBuilder.RenameTable(
                name: "Trades",
                newName: "Trade");

            migrationBuilder.RenameTable(
                name: "CalculatorStates",
                newName: "CalculatorState");

            migrationBuilder.RenameIndex(
                name: "IX_CalculatorStates_TradeId",
                table: "CalculatorState",
                newName: "IX_CalculatorState_TradeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Trade",
                table: "Trade",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CalculatorState",
                table: "CalculatorState",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "TradeChallenge",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TradeCap = table.Column<int>(type: "int", nullable: false),
                    IsOpen = table.Column<bool>(type: "bit", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TradeChallenge", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TradeThread",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TradeChallengeId = table.Column<int>(type: "int", nullable: true),
                    TradeId_tail = table.Column<int>(type: "int", nullable: true),
                    TradeId_head = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TradeThread", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TradeThread_Trade_TradeId_head",
                        column: x => x.TradeId_head,
                        principalTable: "Trade",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TradeThread_Trade_TradeId_tail",
                        column: x => x.TradeId_tail,
                        principalTable: "Trade",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TradeThread_TradeChallenge_TradeChallengeId",
                        column: x => x.TradeChallengeId,
                        principalTable: "TradeChallenge",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TradeThread_TradeChallengeId",
                table: "TradeThread",
                column: "TradeChallengeId");

            migrationBuilder.CreateIndex(
                name: "IX_TradeThread_TradeId_head",
                table: "TradeThread",
                column: "TradeId_head",
                unique: true,
                filter: "[TradeId_head] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_TradeThread_TradeId_tail",
                table: "TradeThread",
                column: "TradeId_tail",
                unique: true,
                filter: "[TradeId_tail] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_CalculatorState_Trade_TradeId",
                table: "CalculatorState",
                column: "TradeId",
                principalTable: "Trade",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CalculatorState_Trade_TradeId",
                table: "CalculatorState");

            migrationBuilder.DropTable(
                name: "TradeThread");

            migrationBuilder.DropTable(
                name: "TradeChallenge");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Trade",
                table: "Trade");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CalculatorState",
                table: "CalculatorState");

            migrationBuilder.RenameTable(
                name: "Trade",
                newName: "Trades");

            migrationBuilder.RenameTable(
                name: "CalculatorState",
                newName: "CalculatorStates");

            migrationBuilder.RenameIndex(
                name: "IX_CalculatorState_TradeId",
                table: "CalculatorStates",
                newName: "IX_CalculatorStates_TradeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Trades",
                table: "Trades",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CalculatorStates",
                table: "CalculatorStates",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CalculatorStates_Trades_TradeId",
                table: "CalculatorStates",
                column: "TradeId",
                principalTable: "Trades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
