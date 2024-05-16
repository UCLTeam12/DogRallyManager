namespace DogRallyManager.ViewModels.ChatVMs
{
    public class ChatRoomVM
    {
        public int ChatId { get; set; }
        public string? ChatRoomName { get; set; }
        public ICollection<ChatMessageVM> ChatMessages { get; set; }

    }
}
