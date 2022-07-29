using Microsoft.EntityFrameworkCore.Migrations;

namespace TradingTools.DAL.Migrations
{
    public partial class TradeThreadDropTail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TradeThread_Trade_TradeId_tail",
                table: "TradeThread");

            migrationBuilder.DropIndex(
                name: "IX_TradeThread_TradeId_tail",
                table: "TradeThread");

            migrationBuilder.DropColumn(
                name: "TradeId_tail",
                table: "TradeThread");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TradeId_tail",
                table: "TradeThread",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TradeThread_TradeId_tail",
                table: "TradeThread",
                column: "TradeId_tail",
                unique: true,
                filter: "[TradeId_tail] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_TradeThread_Trade_TradeId_tail",
                table: "TradeThread",
                column: "TradeId_tail",
                principalTable: "Trade",
                principalColumn: "Id");
        }
    }
}
