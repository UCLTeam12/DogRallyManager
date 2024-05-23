using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DogRallyManager.Migrations
{
    /// <inheritdoc />
    public partial class DatabaseFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Signs",
                keyColumn: "Id",
                keyValue: 3);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Signs",
                columns: new[] { "Id", "PositionX", "PositionY", "SignType" },
                values: new object[] { 3, 150, 150, "exercise-3.png" });
        }
    }
}
