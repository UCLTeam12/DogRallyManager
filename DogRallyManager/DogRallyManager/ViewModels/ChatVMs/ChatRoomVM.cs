namespace DogRallyManager.ViewModels.ChatVMs
{
    public class ChatRoomVM
    {
        public int ChatId { get; set; }
        public string? ChatRoomName { get; set; }
        public List<ChatMessageVM> Messages { get; set; } = new();

    }
}
