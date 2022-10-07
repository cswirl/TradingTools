using Microsoft.EntityFrameworkCore.Migrations;

namespace TradingTools.Repository.Migrations
{
    public partial class CalculatorStateAddSide : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PositionSide",
                table: "Trades",
                newName: "Side");

            migrationBuilder.AddColumn<bool>(
                name: "IsLotSizeChecked",
                table: "CalculatorStates",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Side",
                table: "CalculatorStates",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsLotSizeChecked",
                table: "CalculatorStates");

            migrationBuilder.DropColumn(
                name: "Side",
                table: "CalculatorStates");

            migrationBuilder.RenameColumn(
                name: "Side",
                table: "Trades",
                newName: "PositionSide");
        }
    }
}
