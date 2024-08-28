using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarSelling.Migrations
{
    public partial class AddedPropsToCarModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Color",
                table: "Cars",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ComfortFeatures",
                table: "Cars",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EngineType",
                table: "Cars",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Cars",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "NumberOfDoors",
                table: "Cars",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "SafetyFeatures",
                table: "Cars",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Color",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "ComfortFeatures",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "EngineType",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "NumberOfDoors",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "SafetyFeatures",
                table: "Cars");
        }
    }
}
