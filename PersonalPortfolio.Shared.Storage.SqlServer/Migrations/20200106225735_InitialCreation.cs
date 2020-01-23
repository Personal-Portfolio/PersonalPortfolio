using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PersonalPortfolio.Shared.Storage.SqlServer.Migrations
{
    public partial class InitialCreation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Currencies",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateCreated = table.Column<DateTime>(type: "datetime2(2)", nullable: false, defaultValueSql: "sysutcdatetime()"),
                    DateUpdated = table.Column<DateTime>(type: "datetime2(2)", nullable: true),
                    Code = table.Column<string>(maxLength: 4, nullable: false),
                    Description = table.Column<string>(maxLength: 300, nullable: true, defaultValue: "")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currencies", x => x.Id);
                    table.UniqueConstraint("AK_Currencies_Code", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "SecurityTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateCreated = table.Column<DateTime>(type: "datetime2(2)", nullable: false, defaultValueSql: "sysutcdatetime()"),
                    DateUpdated = table.Column<DateTime>(type: "datetime2(2)", nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SecurityTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CurrencyRates",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateCreated = table.Column<DateTime>(type: "datetime2(2)", nullable: false, defaultValueSql: "sysutcdatetime()"),
                    DateUpdated = table.Column<DateTime>(type: "datetime2(2)", nullable: true),
                    CurrencyId = table.Column<int>(nullable: false),
                    RateTime = table.Column<DateTime>(type: "date", nullable: false),
                    Value = table.Column<decimal>(type: "decimal(9,5)", nullable: false),
                    SourceCurrencyId = table.Column<int>(nullable: false),
                    DataSourceId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurrencyRates", x => x.Id);
                    table.UniqueConstraint("AK_CurrencyRates_DataSourceId_SourceCurrencyId_CurrencyId_RateTime", x => new { x.DataSourceId, x.SourceCurrencyId, x.CurrencyId, x.RateTime });
                    table.ForeignKey(
                        name: "FK_CurrencyRates_Currencies_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currencies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CurrencyRates_Currencies_SourceCurrencyId",
                        column: x => x.SourceCurrencyId,
                        principalTable: "Currencies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Securities",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateCreated = table.Column<DateTime>(type: "datetime2(2)", nullable: false, defaultValueSql: "sysutcdatetime()"),
                    DateUpdated = table.Column<DateTime>(type: "datetime2(2)", nullable: true),
                    Ticker = table.Column<string>(maxLength: 64, nullable: false),
                    Description = table.Column<string>(maxLength: 300, nullable: true, defaultValue: ""),
                    TypeId = table.Column<int>(nullable: false),
                    BaseCurrencyId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Securities", x => x.Id);
                    table.UniqueConstraint("AK_Securities_Ticker", x => x.Ticker);
                    table.ForeignKey(
                        name: "FK_Securities_Currencies_BaseCurrencyId",
                        column: x => x.BaseCurrencyId,
                        principalTable: "Currencies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Securities_SecurityTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "SecurityTypes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SecurityPrices",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateCreated = table.Column<DateTime>(type: "datetime2(2)", nullable: false, defaultValueSql: "sysutcdatetime()"),
                    DateUpdated = table.Column<DateTime>(type: "datetime2(2)", nullable: true),
                    CurrencyId = table.Column<int>(nullable: false),
                    SecurityId = table.Column<int>(nullable: false),
                    TradeDate = table.Column<DateTime>(type: "date", nullable: false),
                    Average = table.Column<decimal>(type: "decimal(9,5)", nullable: false),
                    Open = table.Column<decimal>(type: "decimal(9,5)", nullable: false),
                    Close = table.Column<decimal>(type: "decimal(9,5)", nullable: false),
                    Low = table.Column<decimal>(type: "decimal(9,5)", nullable: false),
                    High = table.Column<decimal>(type: "decimal(9,5)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SecurityPrices", x => x.Id);
                    table.UniqueConstraint("AK_SecurityPrices_TradeDate_SecurityId", x => new { x.TradeDate, x.SecurityId });
                    table.ForeignKey(
                        name: "FK_SecurityPrices_Currencies_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currencies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SecurityPrices_Securities_SecurityId",
                        column: x => x.SecurityId,
                        principalTable: "Securities",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CurrencyRates_CurrencyId",
                table: "CurrencyRates",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_CurrencyRates_SourceCurrencyId",
                table: "CurrencyRates",
                column: "SourceCurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Securities_BaseCurrencyId",
                table: "Securities",
                column: "BaseCurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Securities_TypeId",
                table: "Securities",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_SecurityPrices_CurrencyId",
                table: "SecurityPrices",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_SecurityPrices_SecurityId",
                table: "SecurityPrices",
                column: "SecurityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CurrencyRates");

            migrationBuilder.DropTable(
                name: "SecurityPrices");

            migrationBuilder.DropTable(
                name: "Securities");

            migrationBuilder.DropTable(
                name: "Currencies");

            migrationBuilder.DropTable(
                name: "SecurityTypes");
        }
    }
}
