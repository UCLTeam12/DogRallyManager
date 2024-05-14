using DogRallyManager.ViewModels.ChatVMs;

namespace DogRallyManager.Services
{
    public interface IMessageCreationService
    {
        Task CreateMessageAsync(ChatMessageVM chatMessageVM, ChatRoomVM chatRoom);
    }
}