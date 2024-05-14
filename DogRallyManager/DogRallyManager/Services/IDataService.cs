
using DogRallyManager.Entities;

namespace DogRallyManager.Services
{
    public interface IDataService
    {
        Task AddMessageAsync(Message message);
        Task<List<Message>> GetAllMessagesAsync();
        Task<List<ChatRoom>> GetAssociatedChatRoomsAsync(string userId);

    }
}