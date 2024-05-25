using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DogRallyManager.Migrations
{
    /// <inheritdoc />
    public partial class JunctionTableForBoardsUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BoardRallyUser",
                columns: table => new
                {
                    BoardsId = table.Column<int>(type: "INTEGER", nullable: false),
                    ParticipatingUsersId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoardRallyUser", x => new { x.BoardsId, x.ParticipatingUsersId });
                    table.ForeignKey(
                        name: "FK_BoardRallyUser_AspNetUsers_ParticipatingUsersId",
                        column: x => x.ParticipatingUsersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BoardRallyUser_Boards_BoardsId",
                        column: x => x.BoardsId,
                        principalTable: "Boards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BoardRallyUser_ParticipatingUsersId",
                table: "BoardRallyUser",
                column: "ParticipatingUsersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BoardRallyUser");
        }
    }
}
