using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TeploAPI.Migrations
{
    /// <inheritdoc />
    public partial class coefficientsreference : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Сoefficients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
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
                    table.PrimaryKey("PK_Сoefficients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "References",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RefId1 = table.Column<int>(type: "integer", nullable: true),
                    RefId2 = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_References", x => x.Id);
                    table.ForeignKey(
                        name: "FK_References_Сoefficients_RefId1",
                        column: x => x.RefId1,
                        principalTable: "Сoefficients",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_References_Сoefficients_RefId2",
                        column: x => x.RefId2,
                        principalTable: "Сoefficients",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Сoefficients",
                columns: new[] { "Id", "IncreaseBlastHumidity", "IncreaseGasPressureUnderGrate", "IncreaseMassFractionOfManganeseInChugun", "IncreaseMassFractionOfPhosphorusInChugun", "IncreaseMassFractionOfTitanInChugun", "IncreaseNaturalGasCunsimption", "IncreaseVolumeFractionOxygenInBlast", "IronMassFractionIncreaseInOreRash", "OutputFromLimestoneCharge", "ReductionMassFractionAshInCokeInRangeOf11to12Percent", "ReductionMassFractionAshInCokeInRangeOf12to13Percent", "ReductionMassFractionOfSera", "ReductionMassFractionOfSeraInChugun", "ReductionMassFractionOfSiliciumInChugun", "ReductionMassFractionTrifles", "ShareCrudeOreReductionCharge", "TemperatureIncreaseInRangeOf1001to1100", "TemperatureIncreaseInRangeOf1101to1200", "TemperatureIncreaseInRangeOf800to900", "TemperatureIncreaseInRangeOf901to1000" },
                values: new object[,]
                {
                    { 1, 0.14999999999999999, -0.20000000000000001, 0.20000000000000001, 1.2, 1.3, -0.69999999999999996, 0.29999999999999999, -1.0, -0.5, -1.5, -2.0, -0.29999999999999999, 1.0, -1.2, -0.5, -0.20000000000000001, -0.23000000000000001, -0.20000000000000001, -0.33000000000000002, -0.29999999999999999 },
                    { 2, -0.070000000000000007, 1.0, -0.20000000000000001, -1.2, -1.3, 0.0, 2.1000000000000001, 1.7, 0.5, 1.5, 1.8, 0.29999999999999999, -1.0, 1.2, 1.0, 0.20000000000000001, 0.12, 0.10000000000000001, 0.20000000000000001, 0.14999999999999999 }
                });

            migrationBuilder.InsertData(
                table: "References",
                columns: new[] { "Id", "RefId1", "RefId2" },
                values: new object[] { 1, 1, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_References_RefId1",
                table: "References",
                column: "RefId1");

            migrationBuilder.CreateIndex(
                name: "IX_References_RefId2",
                table: "References",
                column: "RefId2");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "References");

            migrationBuilder.DropTable(
                name: "Сoefficients");
        }
    }
}
