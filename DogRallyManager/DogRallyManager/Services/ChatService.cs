using DogRallyManager.DbContexts;
using DogRallyManager.Entities;
using Microsoft.AspNetCore.Identity;

namespace DogRallyManager.Services
{
    public class ChatService
    {
        private readonly UserManager<RallyUser> _userManager;
        private readonly IDataService _dataService;

        public ChatService(UserManager<RallyUser> usermanager, 
            IDataService dataService)
        {
            _userManager = usermanager;
            _dataService = dataService;
        }

        public async Task CreateGeneralChatRoomAsync()
        {
            var generalRoom = _dataService.GetChatRoomByNameAsync("General");

            if (generalRoom == null)
            {
                ChatRoom createdGeneralRoom = new ChatRoom { Id = 1, RoomName = "General" };
                await _dataService.CreateChatRoom(createdGeneralRoom);
            }
        }
    }
}
