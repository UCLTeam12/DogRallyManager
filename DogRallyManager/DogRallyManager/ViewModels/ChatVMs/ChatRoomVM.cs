using DogRallyManager.ViewModels.AccountVMs;

namespace DogRallyManager.ViewModels.ChatVMs
{
    public class ChatRoomVM
    {
        public int Id { get; set; }
        public string? ChatRoomName { get; set; }

        public int NumberOfAssociatedUsers { get; set; }

        public ICollection<UserViewModel> userViewModels { get; set; }
        public ICollection<ChatMessageVM> ChatMessages { get; set; }

    }
}
