using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeploAPI.Migrations
{
    /// <inheritdoc />
    public partial class datesaveinput : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "SaveDate",
                table: "Furnaces",
                type: "timestamp with time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SaveDate",
                table: "Furnaces");
        }
    }
}
