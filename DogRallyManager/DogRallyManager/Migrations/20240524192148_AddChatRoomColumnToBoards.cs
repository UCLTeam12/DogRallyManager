using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DogRallyManager.Migrations
{
    /// <inheritdoc />
    public partial class AddChatRoomColumnToBoards : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AssociatedChatRoomId",
                table: "Boards",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Boards_AssociatedChatRoomId",
                table: "Boards",
                column: "AssociatedChatRoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_Boards_ChatRooms_AssociatedChatRoomId",
                table: "Boards",
                column: "AssociatedChatRoomId",
                principalTable: "ChatRooms",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Boards_ChatRooms_AssociatedChatRoomId",
                table: "Boards");

            migrationBuilder.DropIndex(
                name: "IX_Boards_AssociatedChatRoomId",
                table: "Boards");

            migrationBuilder.DropColumn(
                name: "AssociatedChatRoomId",
                table: "Boards");
        }
    }
}
