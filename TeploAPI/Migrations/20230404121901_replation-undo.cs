using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeploAPI.Migrations
{
    /// <inheritdoc />
    public partial class replationundo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Furnaces_Users_UserId",
                table: "Furnaces");

            migrationBuilder.DropIndex(
                name: "IX_Furnaces_UserId",
                table: "Furnaces");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Furnaces",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Furnaces",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

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
    }
}
