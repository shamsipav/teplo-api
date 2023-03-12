using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace TeploAPI.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Furnaces",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NumberOfFurnace = table.Column<double>(type: "double precision", nullable: false),
                    UsefulVolumeOfFurnace = table.Column<double>(type: "double precision", nullable: false),
                    UsefulHeightOfFurnace = table.Column<double>(type: "double precision", nullable: false),
                    DiameterOfColoshnik = table.Column<double>(type: "double precision", nullable: false),
                    DiameterOfRaspar = table.Column<double>(type: "double precision", nullable: false),
                    DiameterOfHorn = table.Column<double>(type: "double precision", nullable: false),
                    HeightOfHorn = table.Column<double>(type: "double precision", nullable: false),
                    HeightOfTuyeres = table.Column<double>(type: "double precision", nullable: false),
                    HeightOfZaplechiks = table.Column<double>(type: "double precision", nullable: false),
                    HeightOfRaspar = table.Column<double>(type: "double precision", nullable: false),
                    HeightOfShaft = table.Column<double>(type: "double precision", nullable: false),
                    HeightOfColoshnik = table.Column<double>(type: "double precision", nullable: false),
                    EstablishedLevelOfEmbankment = table.Column<double>(type: "double precision", nullable: false),
                    NumberOfTuyeres = table.Column<double>(type: "double precision", nullable: false),
                    DailyСapacityOfFurnace = table.Column<double>(type: "double precision", nullable: false),
                    SpecificConsumptionOfCoke = table.Column<double>(type: "double precision", nullable: false),
                    SpecificConsumptionOfZRM = table.Column<double>(type: "double precision", nullable: false),
                    ShareOfPelletsInCharge = table.Column<double>(type: "double precision", nullable: false),
                    BlastConsumption = table.Column<double>(type: "double precision", nullable: false),
                    BlastTemperature = table.Column<double>(type: "double precision", nullable: false),
                    BlastPressure = table.Column<double>(type: "double precision", nullable: false),
                    BlastHumidity = table.Column<double>(type: "double precision", nullable: false),
                    OxygenContentInBlast = table.Column<double>(type: "double precision", nullable: false),
                    NaturalGasConsumption = table.Column<double>(type: "double precision", nullable: false),
                    ColoshGasTemperature = table.Column<double>(type: "double precision", nullable: false),
                    ColoshGasPressure = table.Column<double>(type: "double precision", nullable: false),
                    ColoshGas_CO = table.Column<double>(type: "double precision", nullable: false),
                    ColoshGas_CO2 = table.Column<double>(type: "double precision", nullable: false),
                    ColoshGas_H2 = table.Column<double>(type: "double precision", nullable: false),
                    Chugun_SI = table.Column<double>(type: "double precision", nullable: false),
                    Chugun_MN = table.Column<double>(type: "double precision", nullable: false),
                    Chugun_P = table.Column<double>(type: "double precision", nullable: false),
                    Chugun_S = table.Column<double>(type: "double precision", nullable: false),
                    Chugun_C = table.Column<double>(type: "double precision", nullable: false),
                    AshContentInCoke = table.Column<double>(type: "double precision", nullable: false),
                    VolatileContentInCoke = table.Column<double>(type: "double precision", nullable: false),
                    SulfurContentInCoke = table.Column<double>(type: "double precision", nullable: false),
                    SpecificSlagYield = table.Column<double>(type: "double precision", nullable: false),
                    HeatCapacityOfAgglomerate = table.Column<double>(type: "double precision", nullable: false),
                    HeatCapacityOfPellets = table.Column<double>(type: "double precision", nullable: false),
                    HeatCapacityOfCoke = table.Column<double>(type: "double precision", nullable: false),
                    AcceptedTemperatureOfBackupZone = table.Column<double>(type: "double precision", nullable: false),
                    ProportionOfHeatLossesOfLowerPart = table.Column<double>(type: "double precision", nullable: false),
                    AverageSizeOfPieceCharge = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Furnaces", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Furnaces");
        }
    }
}
