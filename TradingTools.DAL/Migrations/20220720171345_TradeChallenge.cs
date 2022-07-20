using Microsoft.EntityFrameworkCore.Migrations;

namespace TradingTools.DAL.Migrations
{
    public partial class TradeChallenge : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TradeChallenge",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TradeCap = table.Column<int>(type: "int", nullable: false),
                    IsOpen = table.Column<bool>(type: "bit", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TradeChallenge", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TradeThread",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TradeChallengeId = table.Column<int>(type: "int", nullable: true),
                    TradeId_tail = table.Column<int>(type: "int", nullable: true),
                    TradeId_head = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TradeThread", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TradeThread_TradeChallenge_TradeChallengeId",
                        column: x => x.TradeChallengeId,
                        principalTable: "TradeChallenge",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TradeThread_Trades_TradeId_head",
                        column: x => x.TradeId_head,
                        principalTable: "Trades",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TradeThread_Trades_TradeId_tail",
                        column: x => x.TradeId_tail,
                        principalTable: "Trades",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TradeThread_TradeChallengeId",
                table: "TradeThread",
                column: "TradeChallengeId");

            migrationBuilder.CreateIndex(
                name: "IX_TradeThread_TradeId_head",
                table: "TradeThread",
                column: "TradeId_head",
                unique: true,
                filter: "[TradeId_head] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_TradeThread_TradeId_tail",
                table: "TradeThread",
                column: "TradeId_tail",
                unique: true,
                filter: "[TradeId_tail] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TradeThread");

            migrationBuilder.DropTable(
                name: "TradeChallenge");
        }
    }
}
