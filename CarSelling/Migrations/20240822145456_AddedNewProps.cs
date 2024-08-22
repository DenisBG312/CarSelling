using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarSelling.Migrations
{
    public partial class AddedNewProps : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CarCreationDate",
                table: "Cars",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Cars",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Cars",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CarCreationDate",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Cars");
        }
    }
}
