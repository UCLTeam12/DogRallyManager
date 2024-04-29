using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DogRallyManager.Migrations
{
    /// <inheritdoc />
    public partial class ChangeOfField_isAdminSQLite : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "password",
                value: "black");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "password",
                value: "wreck");
        }
    }
}
