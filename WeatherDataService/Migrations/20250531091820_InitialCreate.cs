using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Digipay_Task.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "weather_info",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    json_raw_data = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_weather_info", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_weather_info_created_date",
                table: "weather_info",
                column: "created_date");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "weather_info");
        }
    }
}
