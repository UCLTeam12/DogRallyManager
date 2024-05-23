using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DogRallyManager.Migrations
{
    /// <inheritdoc />
    public partial class DeleteSeedDataFromSigns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Signs_Boards_BoardId",
                table: "Signs");

            migrationBuilder.DeleteData(
                table: "Signs",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Signs",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.AlterColumn<int>(
                name: "BoardId",
                table: "Signs",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Signs_Boards_BoardId",
                table: "Signs",
                column: "BoardId",
                principalTable: "Boards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Signs_Boards_BoardId",
                table: "Signs");

            migrationBuilder.AlterColumn<int>(
                name: "BoardId",
                table: "Signs",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.InsertData(
                table: "Signs",
                columns: new[] { "Id", "BoardId", "PositionX", "PositionY", "SignType" },
                values: new object[,]
                {
                    { 1, null, 50, 50, "exercise-1.png" },
                    { 2, null, 100, 100, "exercise-2.png" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Signs_Boards_BoardId",
                table: "Signs",
                column: "BoardId",
                principalTable: "Boards",
                principalColumn: "Id");
        }
    }
}
