using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeploAPI.Migrations
{
    /// <inheritdoc />
    public partial class materialsupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MaterialsWorkParams_FurnacesWorkParams_FurnaceBaseParamId",
                table: "MaterialsWorkParams");

            migrationBuilder.DropColumn(
                name: "WorkParamsId",
                table: "MaterialsWorkParams");

            migrationBuilder.AlterColumn<Guid>(
                name: "FurnaceBaseParamId",
                table: "MaterialsWorkParams",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_MaterialsWorkParams_FurnacesWorkParams_FurnaceBaseParamId",
                table: "MaterialsWorkParams",
                column: "FurnaceBaseParamId",
                principalTable: "FurnacesWorkParams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MaterialsWorkParams_FurnacesWorkParams_FurnaceBaseParamId",
                table: "MaterialsWorkParams");

            migrationBuilder.AlterColumn<Guid>(
                name: "FurnaceBaseParamId",
                table: "MaterialsWorkParams",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<Guid>(
                name: "WorkParamsId",
                table: "MaterialsWorkParams",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddForeignKey(
                name: "FK_MaterialsWorkParams_FurnacesWorkParams_FurnaceBaseParamId",
                table: "MaterialsWorkParams",
                column: "FurnaceBaseParamId",
                principalTable: "FurnacesWorkParams",
                principalColumn: "Id");
        }
    }
}
