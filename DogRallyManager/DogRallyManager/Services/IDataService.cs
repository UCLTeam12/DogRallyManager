
using DogRallyManager.DbContexts;
using DogRallyManager.Entities;
using DogRallyManager.ViewModels.AccountVMs;
using DogRallyManager.ViewModels.ChatVMs;

namespace DogRallyManager.Services
{
    public interface IDataService
    {
        Task AddMessageAsync(string messageBody, int chatRoomId, RallyUser sender);
        Task<List<Message>> GetMessagesForChatRoomAsync(int chatRoomId);
        Task<List<Message>> GetAllMessagesAsync();
        Task<List<ChatRoom>> GetUserAssociatedChatRoomsWithMessagesAsync(string userId);
        Task<ChatRoom?> GetChatRoomByNameAsync(string name);
        Task CreateChatRoom(ChatRoom chatRoom);
        Task AddUserToChatRoomAsync(string userName, int chatRoomId);
        Task<bool> RoomExists(string initiatingUser, string recipientUser);
        Task<RallyUser?> GetUserByNameAsync(string userName);
        Task<List<RallyUser>> GetSimilarNamedUsersAsync(string userName);
        Task<List<RallyUser>> GetAllUserNamesAsync();
       



    }
}