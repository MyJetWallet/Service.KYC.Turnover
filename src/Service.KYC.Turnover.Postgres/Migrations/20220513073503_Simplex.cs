using Microsoft.EntityFrameworkCore.Migrations;
using Service.Bitgo.DepositDetector.Domain.Models;

#nullable disable

namespace Service.KYC.Turnover.Postgres.Migrations
{
    public partial class Simplex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "AssetIndexPrice",
                schema: "kycturnover",
                table: "deposits",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<SimplexData>(
                name: "SimplexData",
                schema: "kycturnover",
                table: "deposits",
                type: "jsonb",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AssetIndexPrice",
                schema: "kycturnover",
                table: "deposits");

            migrationBuilder.DropColumn(
                name: "SimplexData",
                schema: "kycturnover",
                table: "deposits");
        }
    }
}
