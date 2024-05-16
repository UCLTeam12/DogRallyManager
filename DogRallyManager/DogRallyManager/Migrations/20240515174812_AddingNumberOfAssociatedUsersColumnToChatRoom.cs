using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DogRallyManager.Migrations
{
    /// <inheritdoc />
    public partial class AddingNumberOfAssociatedUsersColumnToChatRoom : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NumberOfAssociatedUsers",
                table: "ChatRooms",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumberOfAssociatedUsers",
                table: "ChatRooms");
        }
    }
}
