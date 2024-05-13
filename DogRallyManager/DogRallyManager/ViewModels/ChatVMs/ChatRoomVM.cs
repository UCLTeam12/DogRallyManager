namespace DogRallyManager.ViewModels.ChatVMs
{
    public class ChatRoomVM
    {
        public string? ChatRoomName { get; set; }
        public List<ChatMessageVM> Messages { get; set; } = new();

    }
}
