using Microsoft.EntityFrameworkCore.Migrations;

namespace TradingTools.Repository.Migrations
{
    public partial class ComputedColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClosingTradingCost",
                table: "Trades");

            migrationBuilder.DropColumn(
                name: "ClosingTradingFee",
                table: "Trades");

            migrationBuilder.DropColumn(
                name: "DailyInterestRate",
                table: "Trades");

            migrationBuilder.DropColumn(
                name: "InterestCost",
                table: "Trades");

            migrationBuilder.DropColumn(
                name: "OpeningTradingCost",
                table: "Trades");

            migrationBuilder.DropColumn(
                name: "OpeningTradingFee",
                table: "Trades");

            migrationBuilder.AlterColumn<decimal>(
                name: "LeveragedCapital",
                table: "Trades",
                type: "decimal(18,6)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,6)");

            migrationBuilder.AlterColumn<int>(
                name: "DayCount",
                table: "Trades",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<decimal>(
                name: "BorrowAmount",
                table: "Trades",
                type: "decimal(18,6)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,6)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "LeveragedCapital",
                table: "Trades",
                type: "decimal(18,6)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,6)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DayCount",
                table: "Trades",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "BorrowAmount",
                table: "Trades",
                type: "decimal(18,6)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,6)",
                oldNullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ClosingTradingCost",
                table: "Trades",
                type: "decimal(18,6)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ClosingTradingFee",
                table: "Trades",
                type: "decimal(18,6)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "DailyInterestRate",
                table: "Trades",
                type: "decimal(18,6)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "InterestCost",
                table: "Trades",
                type: "decimal(18,6)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "OpeningTradingCost",
                table: "Trades",
                type: "decimal(18,6)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "OpeningTradingFee",
                table: "Trades",
                type: "decimal(18,6)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
