using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TradingTools.DAL.Migrations
{
    public partial class AddTrade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TradeId",
                table: "CalculatorStates",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Trades",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ticker = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    PositionSide = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    DateEnter = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Capital = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    Leverage = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    LeveragedCapital = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    EntryPriceAvg = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    LotSize = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    OpeningTradingFee = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    OpeningTradingCost = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    BorrowAmount = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    DayCount = table.Column<int>(type: "int", nullable: false),
                    DailyInterestRate = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    InterestCost = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    DateExit = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ExitPriceAvg = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    ClosingTradingFee = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    ClosingTradingCost = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    PnL = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trades", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CalculatorStates_TradeId",
                table: "CalculatorStates",
                column: "TradeId",
                unique: true,
                filter: "[TradeId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_CalculatorStates_Trades_TradeId",
                table: "CalculatorStates",
                column: "TradeId",
                principalTable: "Trades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CalculatorStates_Trades_TradeId",
                table: "CalculatorStates");

            migrationBuilder.DropTable(
                name: "Trades");

            migrationBuilder.DropIndex(
                name: "IX_CalculatorStates_TradeId",
                table: "CalculatorStates");

            migrationBuilder.DropColumn(
                name: "TradeId",
                table: "CalculatorStates");
        }
    }
}
