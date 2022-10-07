using Microsoft.EntityFrameworkCore.Migrations;

namespace TradingTools.Repository.Migrations
{
    public partial class CalculatorStateComputedColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "PriceIncreaseTarget",
                table: "CalculatorStates",
                type: "decimal(18,6)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,6)");

            migrationBuilder.AlterColumn<decimal>(
                name: "PriceDecreaseTarget",
                table: "CalculatorStates",
                type: "decimal(18,6)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,6)");

            migrationBuilder.AlterColumn<decimal>(
                name: "PEP_ExitPrice",
                table: "CalculatorStates",
                type: "decimal(18,6)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,6)");

            migrationBuilder.AlterColumn<decimal>(
                name: "LEP_ExitPrice",
                table: "CalculatorStates",
                type: "decimal(18,6)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,6)");

            migrationBuilder.AlterColumn<int>(
                name: "DayCount",
                table: "CalculatorStates",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<decimal>(
                name: "BorrowAmount",
                table: "CalculatorStates",
                type: "decimal(18,6)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "LeveragedCapital",
                table: "CalculatorStates",
                type: "decimal(18,6)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BorrowAmount",
                table: "CalculatorStates");

            migrationBuilder.DropColumn(
                name: "LeveragedCapital",
                table: "CalculatorStates");

            migrationBuilder.AlterColumn<decimal>(
                name: "PriceIncreaseTarget",
                table: "CalculatorStates",
                type: "decimal(18,6)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,6)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PriceDecreaseTarget",
                table: "CalculatorStates",
                type: "decimal(18,6)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,6)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PEP_ExitPrice",
                table: "CalculatorStates",
                type: "decimal(18,6)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,6)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "LEP_ExitPrice",
                table: "CalculatorStates",
                type: "decimal(18,6)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,6)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DayCount",
                table: "CalculatorStates",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
