using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DogRallyManager.Migrations
{
    /// <inheritdoc />
    public partial class AddTriggersToUpdateChatRoomsAssociatedUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
         migrationBuilder.Sql(@"
                CREATE TRIGGER UpdateNumberOfAssociatedUsersOnUserAdd
                AFTER INSERT ON ChatRoomRallyUser
                BEGIN
                    UPDATE ChatRooms
                    SET NumberOfAssociatedUsers = (
                        SELECT COUNT(*) FROM ChatRoomRallyUser WHERE UserId = NEW.UserId
                    )
                WHERE Id = NEW.UserId;
            END;
            ");

            migrationBuilder.Sql(@"
            CREATE TRIGGER UpdateNumberOfAssociatedUsersOnUserDelete
            AFTER DELETE ON ChatRoomRallyUser
            BEGIN
                UPDATE ChatRoom
                SET NumberOfAssociatedUsers = (
                    SELECT COUNT(*) FROM ChatRoomRallyUser WHERE UserId = OLD.UserId
                )
                WHERE Id = OLD.UserId;
            END;
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP TRIGGER UpdateNumberOfAssociatedUsersOnUserAdd");
            migrationBuilder.Sql("DROP TRIGGER UpdateNumberOfAssociatedUsersOnUserDelete");
        }
    }
}

