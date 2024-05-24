using AutoMapper;
using DogRallyManager.Entities;
using DogRallyManager.ViewModels.ChatVMs;
using Microsoft.AspNetCore.Identity;

namespace DogRallyManager.Services
{
    public class ChatService : IChatService
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
            var generalRoom = await _dataService.GetChatRoomByNameAsync("General");

            if (generalRoom == null)
            {
                ChatRoom createdGeneralRoom = new ChatRoom { Id = 1, RoomName = "General" };
                await _dataService.CreateChatRoom(createdGeneralRoom);
            }
        }

        public async Task InitiateChat(string participatingUserName1, string participatingUserName2)
        {
            bool RoomExists = await DoesPrivateRoomExist(participatingUserName1, participatingUserName2);
            if (RoomExists)
            {
                return;
            }
            else
            {
                var participatingUser1 = await _userManager.FindByNameAsync(participatingUserName1);
                var participatingUser2 = await _userManager.FindByNameAsync(participatingUserName2);

                ChatRoom chatRoomEntity = new ChatRoom
                {
                    RoomName = GetRoomName(participatingUserName1, participatingUserName2)
                };

                chatRoomEntity.ParticipatingUsers.Add(participatingUser1);
                chatRoomEntity.ParticipatingUsers.Add(participatingUser2);

                await _dataService.CreateChatRoom(chatRoomEntity);
            }
        }

        public string GetRoomName(string userName1, string userName2)
        {
            // Sorting it alphabethically
            var sortedUsernames = new List<string> { userName1, userName2 }.OrderBy(u => u).ToList();
            return $"Chatroom: {sortedUsernames[0]} and {sortedUsernames[1]}";
        }

        public async Task SendMessageAsync(string messageBody, RallyUser sender, int chatRoomId)
        {
            Message message = new Message
            {
                MessageBody = messageBody,
                Sender = sender,
                ChatRoomId = chatRoomId
            };
            await _dataService.CreateMessageAsync(message);
        }
        public async Task<List<ChatRoomVM>> GetUserAssociatedChatRoomsVM(string userId)
        {
            var entityChatRooms = await _dataService.GetUserAssociatedChatRoomsWithMessagesAsync(userId);
            var chatRoomsVM = _mapper.Map<List<ChatRoomVM>>(entityChatRooms);
            return chatRoomsVM;
        }

        public async Task<bool> DoesUserExistAsync(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            return user != null;
        }

        public async Task<bool> DoesPrivateRoomExist(string userName1, string userName2)
        {
            var privateRoomName = GetRoomName(userName1, userName2);
            bool RoomExist = await _dataService.DoesRoomExist(privateRoomName);
            return RoomExist;
        }
    }
}
