using DogRallyManager.ViewModels.AccountVMs;

namespace DogRallyManager.ViewModels.ChatVMs
{
    public class ChatRoomVM
    {
        public int Id { get; set; }
        public string? RoomName { get; set; }

        public int NumberOfAssociatedUsers { get; set; }

        public ICollection<UserViewModel> ParticipatingUsers { get; set; } = new List<UserViewModel>();

        public ICollection<ChatMessageVM> ChatMessages { get; set; } = new List<ChatMessageVM>();


    }
}
