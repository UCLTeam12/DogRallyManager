
using DogRallyManager.DbContexts;
using DogRallyManager.Entities;
using DogRallyManager.ViewModels.AccountVMs;
using DogRallyManager.ViewModels.ChatVMs;

namespace DogRallyManager.Services
{
    public interface IDataService
    {
        Task CreateMessageAsync(Message message);
        Task<List<Message>> GetMessagesForChatRoomAsync(int chatRoomId);
        Task<List<Message>> GetAllMessagesAsync();
        Task<List<ChatRoom>> GetUserAssociatedChatRoomsWithMessagesAsync(string userId);
        Task<ChatRoom?> GetChatRoomByNameAsync(string name);
        Task CreateChatRoom(ChatRoom chatRoom);
        // TO-DO:
        // Concider renaming and extracting some logic to DataService
        Task AddUserToChatRoomAsync(string userName, int chatRoomId);
        Task<bool> DoesRoomExist(string roomName);
        Task<RallyUser?> GetUserByNameAsync(string userName);
        Task<List<RallyUser>> GetSimilarNamedUsersAsync(string userName);
        Task<List<RallyUser>> GetAllUserNamesAsync();
       



    }
}