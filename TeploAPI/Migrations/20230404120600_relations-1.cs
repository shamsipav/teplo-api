using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeploAPI.Migrations
{
    /// <inheritdoc />
    public partial class relations1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Furnaces",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Furnaces_UserId",
                table: "Furnaces",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Furnaces_Users_UserId",
                table: "Furnaces",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Furnaces_Users_UserId",
                table: "Furnaces");

            migrationBuilder.DropIndex(
                name: "IX_Furnaces_UserId",
                table: "Furnaces");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Furnaces");
        }
    }
}
