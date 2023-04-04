using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace TeploAPI.Migrations
{
    /// <inheritdoc />
    public partial class materials : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "SaveDate",
                table: "Furnaces",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Materials",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Coal = table.Column<double>(type: "double precision", nullable: false),
                    Share = table.Column<double>(type: "double precision", nullable: false),
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Materials");

            migrationBuilder.AlterColumn<DateTime>(
                name: "SaveDate",
                table: "Furnaces",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);
        }
    }
}
