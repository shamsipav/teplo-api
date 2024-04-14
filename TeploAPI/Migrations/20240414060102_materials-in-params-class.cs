using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeploAPI.Migrations
{
    /// <inheritdoc />
    public partial class materialsinparamsclass : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "FurnaceBaseParamId",
                table: "MaterialsWorkParams",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MaterialsWorkParams_FurnaceBaseParamId",
                table: "MaterialsWorkParams",
                column: "FurnaceBaseParamId");

            migrationBuilder.AddForeignKey(
                name: "FK_MaterialsWorkParams_FurnacesWorkParams_FurnaceBaseParamId",
                table: "MaterialsWorkParams",
                column: "FurnaceBaseParamId",
                principalTable: "FurnacesWorkParams",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MaterialsWorkParams_FurnacesWorkParams_FurnaceBaseParamId",
                table: "MaterialsWorkParams");

            migrationBuilder.DropIndex(
                name: "IX_MaterialsWorkParams_FurnaceBaseParamId",
                table: "MaterialsWorkParams");

            migrationBuilder.DropColumn(
                name: "FurnaceBaseParamId",
                table: "MaterialsWorkParams");
        }
    }
}
