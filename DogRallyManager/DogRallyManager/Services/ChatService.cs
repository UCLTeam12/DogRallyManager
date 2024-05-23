using AutoMapper;
using DogRallyManager.Entities;
using DogRallyManager.ViewModels.ChatVMs;
using Microsoft.AspNetCore.Identity;

namespace DogRallyManager.Services
{
    public class ChatService
    {
        private readonly UserManager<RallyUser> _userManager;
        private readonly IDataService _dataService;
        private readonly IMapper _mapper;

        public ChatService(UserManager<RallyUser> usermanager,
            IDataService dataService,
            IMapper mapper)
        {
            _userManager = usermanager;
            _dataService = dataService;
            _mapper = mapper;
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

        public string GetRoomName(string userName1, string userName2)
        {
            // Sorting it alphabethically
            var sortedUsernames = new List<string> { userName1, userName2 }.OrderBy(u => u).ToList();
            return $"Chatroom: {sortedUsernames[0]} and {sortedUsernames[2]}";
        }

        public async Task<ChatRoomVM> GetUserAssociatedChatRoomsVM(string userId)
        {
            var entityChatRooms = await _dataService.GetUserAssociatedChatRoomsWithMessagesAsync(userId);
            var chatRoomsVM = _mapper.Map<ChatRoomVM>(entityChatRooms);
            return chatRoomsVM;
        }

    }
}
