using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DogRallyManager.Migrations
{
    /// <inheritdoc />
    public partial class AddedChatRoomId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_ChatRooms_RecipientChatRoomId",
                table: "Messages");

            migrationBuilder.RenameColumn(
                name: "RecipientChatRoomId",
                table: "Messages",
                newName: "ChatRoomId");

            migrationBuilder.RenameIndex(
                name: "IX_Messages_RecipientChatRoomId",
                table: "Messages",
                newName: "IX_Messages_ChatRoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_ChatRooms_ChatRoomId",
                table: "Messages",
                column: "ChatRoomId",
                principalTable: "ChatRooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_ChatRooms_ChatRoomId",
                table: "Messages");

            migrationBuilder.RenameColumn(
                name: "ChatRoomId",
                table: "Messages",
                newName: "RecipientChatRoomId");

            migrationBuilder.RenameIndex(
                name: "IX_Messages_ChatRoomId",
                table: "Messages",
                newName: "IX_Messages_RecipientChatRoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_ChatRooms_RecipientChatRoomId",
                table: "Messages",
                column: "RecipientChatRoomId",
                principalTable: "ChatRooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
