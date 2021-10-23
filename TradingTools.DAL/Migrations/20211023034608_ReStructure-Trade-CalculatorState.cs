using Microsoft.EntityFrameworkCore.Migrations;

namespace TradingTools.DAL.Migrations
{
    public partial class ReStructureTradeCalculatorState : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ReasonForEntry",
                table: "CalculatorStates",
                newName: "ReasonForEntry");

            migrationBuilder.AddColumn<decimal>(
                name: "FinalCapital",
                table: "Trades",
                type: "decimal(18,6)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PnL_percentage",
                table: "Trades",
                type: "decimal(18,6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TradingStyle",
                table: "Trades",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "ClosingTradingCost",
                table: "CalculatorStates",
                type: "decimal(18,6)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ClosingTradingFee",
                table: "CalculatorStates",
                type: "decimal(18,6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CounterBias",
                table: "CalculatorStates",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ExchangeFee",
                table: "CalculatorStates",
                type: "decimal(18,6)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "InterestCost",
                table: "CalculatorStates",
                type: "decimal(18,6)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "LotSize",
                table: "CalculatorStates",
                type: "decimal(18,6)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "OpeningTradingCost",
                table: "CalculatorStates",
                type: "decimal(18,6)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "OpeningTradingFee",
                table: "CalculatorStates",
                type: "decimal(18,6)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "ProBias",
                table: "CalculatorStates",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReasonForExit",
                table: "CalculatorStates",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TradingStyle",
                table: "CalculatorStates",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FinalCapital",
                table: "Trades");

            migrationBuilder.DropColumn(
                name: "PnL_percentage",
                table: "Trades");

            migrationBuilder.DropColumn(
                name: "TradingStyle",
                table: "Trades");

            migrationBuilder.DropColumn(
                name: "ClosingTradingCost",
                table: "CalculatorStates");

            migrationBuilder.DropColumn(
                name: "ClosingTradingFee",
                table: "CalculatorStates");

            migrationBuilder.DropColumn(
                name: "CounterBias",
                table: "CalculatorStates");

            migrationBuilder.DropColumn(
                name: "ExchangeFee",
                table: "CalculatorStates");

            migrationBuilder.DropColumn(
                name: "InterestCost",
                table: "CalculatorStates");

            migrationBuilder.DropColumn(
                name: "LotSize",
                table: "CalculatorStates");

            migrationBuilder.DropColumn(
                name: "OpeningTradingCost",
                table: "CalculatorStates");

            migrationBuilder.DropColumn(
                name: "OpeningTradingFee",
                table: "CalculatorStates");

            migrationBuilder.DropColumn(
                name: "ProBias",
                table: "CalculatorStates");

            migrationBuilder.DropColumn(
                name: "ReasonForExit",
                table: "CalculatorStates");

            migrationBuilder.DropColumn(
                name: "TradingStyle",
                table: "CalculatorStates");

            migrationBuilder.RenameColumn(
                name: "ReasonForEntry",
                table: "CalculatorStates",
                newName: "ReasonForEntry");
        }
    }
}
