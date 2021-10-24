using Microsoft.EntityFrameworkCore.Migrations;

namespace TradingTools.DAL.Migrations
{
    public partial class CalculatorStatesTradeExitPerfectEntry : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProBias",
                table: "CalculatorStates");

            migrationBuilder.AddColumn<decimal>(
                name: "PerfectEntry_EntryPrice",
                table: "CalculatorStates",
                type: "decimal(18,6)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PerfectEntry_ExitPrice",
                table: "CalculatorStates",
                type: "decimal(18,6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PerfectEntry_Note",
                table: "CalculatorStates",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "TradeExit_ExitPrice",
                table: "CalculatorStates",
                type: "decimal(18,6)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PerfectEntry_EntryPrice",
                table: "CalculatorStates");

            migrationBuilder.DropColumn(
                name: "PerfectEntry_ExitPrice",
                table: "CalculatorStates");

            migrationBuilder.DropColumn(
                name: "PerfectEntry_Note",
                table: "CalculatorStates");

            migrationBuilder.DropColumn(
                name: "TradeExit_ExitPrice",
                table: "CalculatorStates");

            migrationBuilder.AddColumn<string>(
                name: "ProBias",
                table: "CalculatorStates",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
