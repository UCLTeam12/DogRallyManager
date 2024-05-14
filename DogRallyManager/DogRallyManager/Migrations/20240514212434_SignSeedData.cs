using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DogRallyManager.Migrations
{
    /// <inheritdoc />
    public partial class SignSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Signs",
                columns: new[] { "Id", "PositionX", "PositionY", "SignType" },
                values: new object[,]
                {
                    { 1, 50, 50, "exercise-1.png" },
                    { 2, 100, 100, "exercise-2.png" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Signs",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Signs",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
