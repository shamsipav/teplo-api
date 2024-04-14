using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeploAPI.Migrations
{
    /// <inheritdoc />
    public partial class materialsparams : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MaterialsWorkParams",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    MaterialId = table.Column<Guid>(type: "uuid", nullable: false),
                    WorkParamsId = table.Column<Guid>(type: "uuid", nullable: false),
                    Consumption = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialsWorkParams", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MaterialsWorkParams");
        }
    }
}
