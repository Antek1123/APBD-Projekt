using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace projektApbd.Shared.Migrations
{
    public partial class second : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ceo",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "Employees",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "HqAddress",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "HqCountry",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "HqState",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "Industry",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "Listdate",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "Logo",
                table: "Company");

            migrationBuilder.RenameColumn(
                name: "Url",
                table: "Company",
                newName: "Logo_Url");

            migrationBuilder.RenameColumn(
                name: "Sector",
                table: "Company",
                newName: "Homepage_url");

            migrationBuilder.RenameColumn(
                name: "Phone",
                table: "Company",
                newName: "Phone_Number");

            migrationBuilder.RenameColumn(
                name: "Exchange",
                table: "Company",
                newName: "Locale");

            migrationBuilder.RenameColumn(
                name: "Country",
                table: "Company",
                newName: "Currency_Name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Phone_Number",
                table: "Company",
                newName: "Phone");

            migrationBuilder.RenameColumn(
                name: "Logo_Url",
                table: "Company",
                newName: "Url");

            migrationBuilder.RenameColumn(
                name: "Locale",
                table: "Company",
                newName: "Exchange");

            migrationBuilder.RenameColumn(
                name: "Homepage_url",
                table: "Company",
                newName: "Sector");

            migrationBuilder.RenameColumn(
                name: "Currency_Name",
                table: "Company",
                newName: "Country");

            migrationBuilder.AddColumn<string>(
                name: "Ceo",
                table: "Company",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Employees",
                table: "Company",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "HqAddress",
                table: "Company",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HqCountry",
                table: "Company",
                type: "nvarchar(3)",
                maxLength: 3,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HqState",
                table: "Company",
                type: "nvarchar(3)",
                maxLength: 3,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Industry",
                table: "Company",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "Listdate",
                table: "Company",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Logo",
                table: "Company",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");
        }
    }
}
