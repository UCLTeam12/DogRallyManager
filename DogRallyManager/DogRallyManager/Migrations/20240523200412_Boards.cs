using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DogRallyManager.Migrations
{
    /// <inheritdoc />
    public partial class Boards : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "PositionY",
                table: "Signs",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "PositionX",
                table: "Signs",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<int>(
                name: "BoardId",
                table: "Signs",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Boards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Boards", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Signs",
                keyColumn: "Id",
                keyValue: 1,
                column: "BoardId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Signs",
                keyColumn: "Id",
                keyValue: 2,
                column: "BoardId",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_Signs_BoardId",
                table: "Signs",
                column: "BoardId");

            migrationBuilder.AddForeignKey(
                name: "FK_Signs_Boards_BoardId",
                table: "Signs",
                column: "BoardId",
                principalTable: "Boards",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Signs_Boards_BoardId",
                table: "Signs");

            migrationBuilder.DropTable(
                name: "Boards");

            migrationBuilder.DropIndex(
                name: "IX_Signs_BoardId",
                table: "Signs");

            migrationBuilder.DropColumn(
                name: "BoardId",
                table: "Signs");

            migrationBuilder.AlterColumn<int>(
                name: "PositionY",
                table: "Signs",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PositionX",
                table: "Signs",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);
        }
    }
}
