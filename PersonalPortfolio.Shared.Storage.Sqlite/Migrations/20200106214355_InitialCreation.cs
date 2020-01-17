using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PersonalPortfolio.Shared.Storage.Sqlite.Migrations
{
    public partial class InitialCreation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "Securities",
                table => new
                {
                    Id = table.Column<int>()
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Securities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                "Rates",
                table => new
                {
                    Id = table.Column<int>()
                        .Annotation("Sqlite:Autoincrement", true),
                    TargetSymbolId = table.Column<int>(),
                    SourceSymbolId = table.Column<int>(),
                    RateTime = table.Column<DateTime>(),
                    Value = table.Column<float>()
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rates", x => x.Id);
                    table.ForeignKey(
                        "FK_Rates_Securities_SourceSymbolId",
                        x => x.SourceSymbolId,
                        "Securities",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        "FK_Rates_Securities_TargetSymbolId",
                        x => x.TargetSymbolId,
                        "Securities",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                "IX_Rates_SourceSymbolId",
                "Rates",
                "SourceSymbolId");

            migrationBuilder.CreateIndex(
                "IX_Rates_TargetSymbolId",
                "Rates",
                "TargetSymbolId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "Rates");

            migrationBuilder.DropTable(
                "Securities");
        }
    }
}
