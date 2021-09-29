using Microsoft.EntityFrameworkCore.Migrations;

namespace TradingTools.DAL.Migrations
{
    public partial class CreateTradingToolsDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CalculatorStates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Capital = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Leverage = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EntryPriceAvg = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DayCount = table.Column<int>(type: "int", nullable: false),
                    DailyInterestRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PriceIncreaseTarget = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PriceDecreaseTarget = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PEP_ExitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PEP_Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LEP_ExitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LEP_Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ticker = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Strategy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalculatorStates", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CalculatorStates");
        }
    }
}
