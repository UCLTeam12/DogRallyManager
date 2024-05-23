
using DogRallyManager.DbContexts;
using DogRallyManager.Entities;
using DogRallyManager.ViewModels.AccountVMs;
using DogRallyManager.ViewModels.ChatVMs;

namespace DogRallyManager.Services
{
    public interface IDataService
    {
        Task CreateGeneralChatRoomAsync();
        Task AddMessageAsync(string messageBody, int chatRoomId, RallyUser sender);
        Task<List<ChatMessageVM>> GetMessagesForChatRoomAsync(int chatRoomId);
        Task<List<Message>> GetAllMessagesAsync();
        Task<List<ChatRoom>> GetAssociatedChatRoomsWithMessagesAsync(string userId);
        Task AddChatRoomAsync(ChatRoom chatRoom);
        Task AddUserToChatRoomAsync(string userName, int chatRoomId);
        Task<UserViewModel> GetUserAsync(string userName);
        Task<List<UserViewModel>> GetSimilarNamedUsersAsync(string userName);
        Task<List<UserViewModel>> GetAllUserNamesAsync();
        Task<bool> RoomAlreadyExists(string initiatingUser, string recipientUser);



    }
}