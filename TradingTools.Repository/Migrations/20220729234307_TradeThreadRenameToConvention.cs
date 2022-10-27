using Microsoft.EntityFrameworkCore.Migrations;

namespace TradingTools.Repository.Migrations
{
    public partial class TradeThreadRenameToConvention : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TradeThread_Trade_TradeId_head",
                table: "TradeThread");

            migrationBuilder.DropIndex(
                name: "IX_TradeThread_TradeId_head",
                table: "TradeThread");

            migrationBuilder.RenameColumn(
                name: "TradeId_head",
                table: "TradeThread",
                newName: "TradeId");

            migrationBuilder.CreateIndex(
                name: "IX_TradeThread_TradeId",
                table: "TradeThread",
                column: "TradeId",
                unique: true,
                filter: "[TradeId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_TradeThread_Trade_TradeId",
                table: "TradeThread",
                column: "TradeId",
                principalTable: "Trade",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TradeThread_Trade_TradeId",
                table: "TradeThread");

            migrationBuilder.DropIndex(
                name: "IX_TradeThread_TradeId",
                table: "TradeThread");

            migrationBuilder.RenameColumn(
                name: "TradeId",
                table: "TradeThread",
                newName: "TradeId_head");

            migrationBuilder.CreateIndex(
                name: "IX_TradeThread_TradeId_head",
                table: "TradeThread",
                column: "TradeId_head",
                unique: true,
                filter: "[TradeId_head] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_TradeThread_Trade_TradeId_head",
                table: "TradeThread",
                column: "TradeId_head",
                principalTable: "Trade",
                principalColumn: "Id");
        }
    }
}
