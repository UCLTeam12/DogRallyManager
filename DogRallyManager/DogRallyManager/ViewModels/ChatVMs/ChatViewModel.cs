namespace DogRallyManager.ViewModels.ChatVMs
{
    public class ChatViewModel
    {
        // THIS IS A WRAPPER CLASS
        public IEnumerable<ChatRoomVM> ChatRooms { get; set; }
        public IEnumerable<ChatMessageVM> ChatMessages { get; set; } 
    }
}
