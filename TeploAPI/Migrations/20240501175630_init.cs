using System;
using Microsoft.EntityFrameworkCore.Migrations;

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
                name: "CokeCunsumptionReferences",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    IronMassFractionIncreaseInOreRash = table.Column<double>(type: "double precision", nullable: false),
                    ShareCrudeOreReductionCharge = table.Column<double>(type: "double precision", nullable: false),
                    TemperatureIncreaseInRangeOf800to900 = table.Column<double>(type: "double precision", nullable: false),
                    TemperatureIncreaseInRangeOf901to1000 = table.Column<double>(type: "double precision", nullable: false),
                    TemperatureIncreaseInRangeOf1001to1100 = table.Column<double>(type: "double precision", nullable: false),
                    TemperatureIncreaseInRangeOf1101to1200 = table.Column<double>(type: "double precision", nullable: false),
                    IncreaseGasPressureUnderGrate = table.Column<double>(type: "double precision", nullable: false),
                    ReductionMassFractionOfSiliciumInChugun = table.Column<double>(type: "double precision", nullable: false),
                    ReductionMassFractionOfSeraInChugun = table.Column<double>(type: "double precision", nullable: false),
                    IncreaseMassFractionOfPhosphorusInChugun = table.Column<double>(type: "double precision", nullable: false),
                    IncreaseMassFractionOfManganeseInChugun = table.Column<double>(type: "double precision", nullable: false),
                    IncreaseMassFractionOfTitanInChugun = table.Column<double>(type: "double precision", nullable: false),
                    IncreaseBlastHumidity = table.Column<double>(type: "double precision", nullable: false),
                    IncreaseNaturalGasCunsimption = table.Column<double>(type: "double precision", nullable: false),
                    OutputFromLimestoneCharge = table.Column<double>(type: "double precision", nullable: false),
                    IncreaseVolumeFractionOxygenInBlast = table.Column<double>(type: "double precision", nullable: false),
                    ReductionMassFractionTrifles = table.Column<double>(type: "double precision", nullable: false),
                    ReductionMassFractionAshInCokeInRangeOf11to12Percent = table.Column<double>(type: "double precision", nullable: false),
                    ReductionMassFractionAshInCokeInRangeOf12to13Percent = table.Column<double>(type: "double precision", nullable: false),
                    ReductionMassFractionOfSera = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CokeCunsumptionReferences", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Furnaces",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    NumberOfFurnace = table.Column<int>(type: "integer", nullable: false),
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
                    HeightOfColoshnik = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Furnaces", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FurnacesWorkParams",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    FurnaceId = table.Column<Guid>(type: "uuid", nullable: false),
                    Day = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
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
                    AverageSizeOfPieceCharge = table.Column<double>(type: "double precision", nullable: false),
                    HeatOfBurningOfNaturalGasOnFarms = table.Column<double>(type: "double precision", nullable: false),
                    HeatOfIncompleteBurningCarbonOfCoke = table.Column<double>(type: "double precision", nullable: false),
                    TemperatureOfCokeThatCameToTuyeres = table.Column<double>(type: "double precision", nullable: false),
                    SaveDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    NumberOfFurnace = table.Column<int>(type: "integer", nullable: false),
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
                    HeightOfColoshnik = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FurnacesWorkParams", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FurnanceCapacityReferences",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    IronMassFractionIncreaseInOreRash = table.Column<double>(type: "double precision", nullable: false),
                    ShareCrudeOreReductionCharge = table.Column<double>(type: "double precision", nullable: false),
                    TemperatureIncreaseInRangeOf800to900 = table.Column<double>(type: "double precision", nullable: false),
                    TemperatureIncreaseInRangeOf901to1000 = table.Column<double>(type: "double precision", nullable: false),
                    TemperatureIncreaseInRangeOf1001to1100 = table.Column<double>(type: "double precision", nullable: false),
                    TemperatureIncreaseInRangeOf1101to1200 = table.Column<double>(type: "double precision", nullable: false),
                    IncreaseGasPressureUnderGrate = table.Column<double>(type: "double precision", nullable: false),
                    ReductionMassFractionOfSiliciumInChugun = table.Column<double>(type: "double precision", nullable: false),
                    ReductionMassFractionOfSeraInChugun = table.Column<double>(type: "double precision", nullable: false),
                    IncreaseMassFractionOfPhosphorusInChugun = table.Column<double>(type: "double precision", nullable: false),
                    IncreaseMassFractionOfManganeseInChugun = table.Column<double>(type: "double precision", nullable: false),
                    IncreaseMassFractionOfTitanInChugun = table.Column<double>(type: "double precision", nullable: false),
                    IncreaseBlastHumidity = table.Column<double>(type: "double precision", nullable: false),
                    IncreaseNaturalGasCunsimption = table.Column<double>(type: "double precision", nullable: false),
                    OutputFromLimestoneCharge = table.Column<double>(type: "double precision", nullable: false),
                    IncreaseVolumeFractionOxygenInBlast = table.Column<double>(type: "double precision", nullable: false),
                    ReductionMassFractionTrifles = table.Column<double>(type: "double precision", nullable: false),
                    ReductionMassFractionAshInCokeInRangeOf11to12Percent = table.Column<double>(type: "double precision", nullable: false),
                    ReductionMassFractionAshInCokeInRangeOf12to13Percent = table.Column<double>(type: "double precision", nullable: false),
                    ReductionMassFractionOfSera = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FurnanceCapacityReferences", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Materials",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Moisture = table.Column<double>(type: "double precision", nullable: false),
                    Fe2O3 = table.Column<double>(type: "double precision", nullable: false),
                    Fe = table.Column<double>(type: "double precision", nullable: false),
                    FeO = table.Column<double>(type: "double precision", nullable: false),
                    CaO = table.Column<double>(type: "double precision", nullable: false),
                    SiO2 = table.Column<double>(type: "double precision", nullable: false),
                    MgO = table.Column<double>(type: "double precision", nullable: false),
                    Al2O3 = table.Column<double>(type: "double precision", nullable: false),
                    TiO2 = table.Column<double>(type: "double precision", nullable: false),
                    MnO = table.Column<double>(type: "double precision", nullable: false),
                    P = table.Column<double>(type: "double precision", nullable: false),
                    S = table.Column<double>(type: "double precision", nullable: false),
                    Zn = table.Column<double>(type: "double precision", nullable: false),
                    Mn = table.Column<double>(type: "double precision", nullable: false),
                    Cr = table.Column<double>(type: "double precision", nullable: false),
                    FiveZero = table.Column<double>(type: "double precision", nullable: false),
                    BaseOne = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Materials", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: true),
                    LastName = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    Password = table.Column<string>(type: "text", nullable: true),
                    LastLoginIp = table.Column<string>(type: "text", nullable: true),
                    LastLoginDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MaterialsWorkParams",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    MaterialId = table.Column<Guid>(type: "uuid", nullable: false),
                    FurnaceBaseParamId = table.Column<Guid>(type: "uuid", nullable: false),
                    Consumption = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialsWorkParams", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MaterialsWorkParams_FurnacesWorkParams_FurnaceBaseParamId",
                        column: x => x.FurnaceBaseParamId,
                        principalTable: "FurnacesWorkParams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MaterialsWorkParams_FurnaceBaseParamId",
                table: "MaterialsWorkParams",
                column: "FurnaceBaseParamId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CokeCunsumptionReferences");

            migrationBuilder.DropTable(
                name: "Furnaces");

            migrationBuilder.DropTable(
                name: "FurnanceCapacityReferences");

            migrationBuilder.DropTable(
                name: "Materials");

            migrationBuilder.DropTable(
                name: "MaterialsWorkParams");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "FurnacesWorkParams");
        }
    }
}
