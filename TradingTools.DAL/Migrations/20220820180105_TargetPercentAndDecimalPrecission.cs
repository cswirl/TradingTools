using Microsoft.EntityFrameworkCore.Migrations;

namespace TradingTools.DAL.Migrations
{
    public partial class TargetPercentAndDecimalPrecission : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "TargetPercentage",
                table: "TradeChallenge",
                type: "decimal(18,9)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<decimal>(
                name: "PnL_percentage",
                table: "Trade",
                type: "decimal(18,9)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,6)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PnL",
                table: "Trade",
                type: "decimal(18,9)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,6)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "LotSize",
                table: "Trade",
                type: "decimal(18,9)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,6)");

            migrationBuilder.AlterColumn<decimal>(
                name: "LeveragedCapital",
                table: "Trade",
                type: "decimal(18,9)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,6)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Leverage",
                table: "Trade",
                type: "decimal(18,9)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,6)");

            migrationBuilder.AlterColumn<decimal>(
                name: "FinalCapital",
                table: "Trade",
                type: "decimal(18,9)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,6)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ExitPriceAvg",
                table: "Trade",
                type: "decimal(18,9)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,6)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "EntryPriceAvg",
                table: "Trade",
                type: "decimal(18,9)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,6)");

            migrationBuilder.AlterColumn<decimal>(
                name: "DayCount",
                table: "Trade",
                type: "decimal(18,9)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,6)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Capital",
                table: "Trade",
                type: "decimal(18,9)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,6)");

            migrationBuilder.AlterColumn<decimal>(
                name: "BorrowAmount",
                table: "Trade",
                type: "decimal(18,9)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,6)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "TradeExit_ExitPrice",
                table: "CalculatorState",
                type: "decimal(18,9)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,6)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PriceIncreaseTarget",
                table: "CalculatorState",
                type: "decimal(18,9)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,6)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PriceDecreaseTarget",
                table: "CalculatorState",
                type: "decimal(18,9)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,6)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PerfectEntry_ExitPrice",
                table: "CalculatorState",
                type: "decimal(18,9)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,6)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PerfectEntry_EntryPrice",
                table: "CalculatorState",
                type: "decimal(18,9)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,6)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PEP_ExitPrice",
                table: "CalculatorState",
                type: "decimal(18,9)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,6)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "OpeningTradingFee",
                table: "CalculatorState",
                type: "decimal(18,9)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,6)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "OpeningTradingCost",
                table: "CalculatorState",
                type: "decimal(18,9)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,6)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "LotSize",
                table: "CalculatorState",
                type: "decimal(18,9)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,6)");

            migrationBuilder.AlterColumn<decimal>(
                name: "LeveragedCapital",
                table: "CalculatorState",
                type: "decimal(18,9)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,6)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Leverage",
                table: "CalculatorState",
                type: "decimal(18,9)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,6)");

            migrationBuilder.AlterColumn<decimal>(
                name: "LEP_ExitPrice",
                table: "CalculatorState",
                type: "decimal(18,9)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,6)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "InterestCost",
                table: "CalculatorState",
                type: "decimal(18,9)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,6)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ExchangeFee",
                table: "CalculatorState",
                type: "decimal(18,9)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,6)");

            migrationBuilder.AlterColumn<decimal>(
                name: "EntryPriceAvg",
                table: "CalculatorState",
                type: "decimal(18,9)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,6)");

            migrationBuilder.AlterColumn<decimal>(
                name: "DayCount",
                table: "CalculatorState",
                type: "decimal(18,9)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,6)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "DailyInterestRate",
                table: "CalculatorState",
                type: "decimal(18,9)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,6)");

            migrationBuilder.AlterColumn<decimal>(
                name: "ClosingTradingFee",
                table: "CalculatorState",
                type: "decimal(18,9)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,6)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ClosingTradingCost",
                table: "CalculatorState",
                type: "decimal(18,9)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,6)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Capital",
                table: "CalculatorState",
                type: "decimal(18,9)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,6)");

            migrationBuilder.AlterColumn<decimal>(
                name: "BorrowAmount",
                table: "CalculatorState",
                type: "decimal(18,9)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,6)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TargetPercentage",
                table: "TradeChallenge");

            migrationBuilder.AlterColumn<decimal>(
                name: "PnL_percentage",
                table: "Trade",
                type: "decimal(18,6)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,9)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PnL",
                table: "Trade",
                type: "decimal(18,6)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,9)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "LotSize",
                table: "Trade",
                type: "decimal(18,6)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,9)");

            migrationBuilder.AlterColumn<decimal>(
                name: "LeveragedCapital",
                table: "Trade",
                type: "decimal(18,6)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,9)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Leverage",
                table: "Trade",
                type: "decimal(18,6)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,9)");

            migrationBuilder.AlterColumn<decimal>(
                name: "FinalCapital",
                table: "Trade",
                type: "decimal(18,6)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,9)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ExitPriceAvg",
                table: "Trade",
                type: "decimal(18,6)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,9)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "EntryPriceAvg",
                table: "Trade",
                type: "decimal(18,6)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,9)");

            migrationBuilder.AlterColumn<decimal>(
                name: "DayCount",
                table: "Trade",
                type: "decimal(18,6)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,9)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Capital",
                table: "Trade",
                type: "decimal(18,6)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,9)");

            migrationBuilder.AlterColumn<decimal>(
                name: "BorrowAmount",
                table: "Trade",
                type: "decimal(18,6)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,9)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "TradeExit_ExitPrice",
                table: "CalculatorState",
                type: "decimal(18,6)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,9)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PriceIncreaseTarget",
                table: "CalculatorState",
                type: "decimal(18,6)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,9)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PriceDecreaseTarget",
                table: "CalculatorState",
                type: "decimal(18,6)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,9)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PerfectEntry_ExitPrice",
                table: "CalculatorState",
                type: "decimal(18,6)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,9)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PerfectEntry_EntryPrice",
                table: "CalculatorState",
                type: "decimal(18,6)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,9)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PEP_ExitPrice",
                table: "CalculatorState",
                type: "decimal(18,6)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,9)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "OpeningTradingFee",
                table: "CalculatorState",
                type: "decimal(18,6)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,9)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "OpeningTradingCost",
                table: "CalculatorState",
                type: "decimal(18,6)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,9)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "LotSize",
                table: "CalculatorState",
                type: "decimal(18,6)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,9)");

            migrationBuilder.AlterColumn<decimal>(
                name: "LeveragedCapital",
                table: "CalculatorState",
                type: "decimal(18,6)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,9)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Leverage",
                table: "CalculatorState",
                type: "decimal(18,6)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,9)");

            migrationBuilder.AlterColumn<decimal>(
                name: "LEP_ExitPrice",
                table: "CalculatorState",
                type: "decimal(18,6)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,9)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "InterestCost",
                table: "CalculatorState",
                type: "decimal(18,6)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,9)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ExchangeFee",
                table: "CalculatorState",
                type: "decimal(18,6)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,9)");

            migrationBuilder.AlterColumn<decimal>(
                name: "EntryPriceAvg",
                table: "CalculatorState",
                type: "decimal(18,6)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,9)");

            migrationBuilder.AlterColumn<decimal>(
                name: "DayCount",
                table: "CalculatorState",
                type: "decimal(18,6)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,9)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "DailyInterestRate",
                table: "CalculatorState",
                type: "decimal(18,6)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,9)");

            migrationBuilder.AlterColumn<decimal>(
                name: "ClosingTradingFee",
                table: "CalculatorState",
                type: "decimal(18,6)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,9)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ClosingTradingCost",
                table: "CalculatorState",
                type: "decimal(18,6)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,9)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Capital",
                table: "CalculatorState",
                type: "decimal(18,6)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,9)");

            migrationBuilder.AlterColumn<decimal>(
                name: "BorrowAmount",
                table: "CalculatorState",
                type: "decimal(18,6)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,9)",
                oldNullable: true);
        }
    }
}
