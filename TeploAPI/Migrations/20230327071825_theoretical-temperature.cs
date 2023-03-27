using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeploAPI.Migrations
{
    /// <inheritdoc />
    public partial class theoreticaltemperature : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "HeatOfBurningOfNaturalGasOnFarms",
                table: "Furnaces",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "HeatOfIncompleteBurningCarbonOfCoke",
                table: "Furnaces",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "TemperatureOfCokeThatCameToTuyeres",
                table: "Furnaces",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HeatOfBurningOfNaturalGasOnFarms",
                table: "Furnaces");

            migrationBuilder.DropColumn(
                name: "HeatOfIncompleteBurningCarbonOfCoke",
                table: "Furnaces");

            migrationBuilder.DropColumn(
                name: "TemperatureOfCokeThatCameToTuyeres",
                table: "Furnaces");
        }
    }
}
