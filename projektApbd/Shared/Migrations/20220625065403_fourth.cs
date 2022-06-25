using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace projektApbd.Shared.Migrations
{
    public partial class fourth : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AfterHours",
                table: "DailyOpenClose");

            migrationBuilder.DropColumn(
                name: "PreMarket",
                table: "DailyOpenClose");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "AfterHours",
                table: "DailyOpenClose",
                type: "decimal(10,2)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PreMarket",
                table: "DailyOpenClose",
                type: "int",
                nullable: true);
        }
    }
}
