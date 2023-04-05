using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeploAPI.Migrations
{
    /// <inheritdoc />
    public partial class namefurnace : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Furnaces",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Furnaces");
        }
    }
}
