
using DogRallyManager.DbContexts;
using DogRallyManager.Entities;
using DogRallyManager.ViewModels.AccountVMs;
using DogRallyManager.ViewModels.ChatVMs;

namespace DogRallyManager.Services
{
    public interface IDataService
    {
        Task AddMessageAsync(Message message);

        Task<List<ChatMessageVM>> GetMessagesForChatRoomAsync(int chatRoomId);
        Task<List<Message>> GetAllMessagesAsync();
        Task<List<ChatRoom>> GetAssociatedChatRoomsWithMessagesAsync(string userId);
        Task AddChatRoomAsync(ChatRoom chatRoom);
        Task AddUserToChatRoomAsync(string userName, int chatRoomId);
        Task<IEnumerable<UserViewModel>> GetAllUserNamesAsync();
        




    }
}