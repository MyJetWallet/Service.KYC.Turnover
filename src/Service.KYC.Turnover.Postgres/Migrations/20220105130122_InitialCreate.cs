using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Service.KYC.Turnover.Postgres.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "kycturnover");

            migrationBuilder.CreateTable(
                name: "deposits",
                schema: "kycturnover",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BrokerId = table.Column<string>(type: "text", nullable: false),
                    ClientId = table.Column<string>(type: "text", nullable: false),
                    WalletId = table.Column<string>(type: "text", nullable: false),
                    TransactionId = table.Column<string>(type: "text", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    AssetSymbol = table.Column<string>(type: "text", nullable: false),
                    Comment = table.Column<string>(type: "text", nullable: false),
                    Integration = table.Column<string>(type: "text", nullable: false),
                    Txid = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    MatchingEngineId = table.Column<string>(type: "text", nullable: false),
                    LastError = table.Column<string>(type: "text", nullable: false),
                    RetriesCount = table.Column<int>(type: "integer", nullable: false),
                    EventDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FeeAmount = table.Column<decimal>(type: "numeric", nullable: false),
                    FeeAssetSymbol = table.Column<string>(type: "text", nullable: false),
                    CardLast4 = table.Column<string>(type: "text", nullable: false),
                    Network = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_deposits", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "settings",
                schema: "kycturnover",
                columns: table => new
                {
                    SettingsKey = table.Column<string>(type: "text", nullable: false),
                    LimitAsset = table.Column<string>(type: "text", nullable: true),
                    LimitLevel2 = table.Column<decimal>(type: "numeric", nullable: false),
                    LimitLevel3 = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_settings", x => x.SettingsKey);
                });

            migrationBuilder.CreateIndex(
                name: "IX_deposits_ClientId",
                schema: "kycturnover",
                table: "deposits",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_deposits_WalletId",
                schema: "kycturnover",
                table: "deposits",
                column: "WalletId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "deposits",
                schema: "kycturnover");

            migrationBuilder.DropTable(
                name: "settings",
                schema: "kycturnover");
        }
    }
}
