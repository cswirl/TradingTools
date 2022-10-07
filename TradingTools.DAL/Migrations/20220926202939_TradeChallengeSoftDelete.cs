using Microsoft.EntityFrameworkCore.Migrations;

namespace TradingTools.Repository.Migrations
{
    public partial class TradeChallengeSoftDelete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "TradeChallenge",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "TradeChallenge");
        }
    }
}
