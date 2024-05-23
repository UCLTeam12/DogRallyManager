using DogRallyManager.Entities;
using DogRallyManager.ViewModels.ChatVMs;

namespace DogRallyManager.Services
{
    public interface IChatService
    {
        Task CreateGeneralChatRoomAsync();
        Task SendMessageAsync(string messageBody, RallyUser sender, int chatroomId);
        Task<bool> DoesPrivateRoomExist(string userName1, string userName2);
        Task<bool> DoesUserExistAsync(string userName);
        string GetRoomName(string userName1, string userName2);
        Task<List<ChatRoomVM>> GetUserAssociatedChatRoomsVM(string userId);
        Task InitiateChat(string participatingUserName1, string participatingUserName2);
    }
}