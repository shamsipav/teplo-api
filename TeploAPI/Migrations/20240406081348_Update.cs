using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeploAPI.Migrations
{
    /// <inheritdoc />
    public partial class Update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Day",
                table: "InputVariants");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "InputVariants");

            migrationBuilder.CreateTable(
                name: "DailyInfo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Day = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DailyInfo_InputVariants_Id",
                        column: x => x.Id,
                        principalTable: "InputVariants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DailyInfo");

            migrationBuilder.AddColumn<DateTime>(
                name: "Day",
                table: "InputVariants",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "InputVariants",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
