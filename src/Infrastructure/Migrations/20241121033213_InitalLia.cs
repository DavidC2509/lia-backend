using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lia.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitalLia : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Prom",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    PromModified = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prom", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PromDinamyc",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    NameEvent = table.Column<string>(type: "text", nullable: false),
                    DateEvent = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CityEvent = table.Column<string>(type: "text", nullable: false),
                    AddresEvent = table.Column<string>(type: "text", nullable: false),
                    AdditionalInformation = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PromDinamyc", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Prom");

            migrationBuilder.DropTable(
                name: "PromDinamyc");
        }
    }
}
