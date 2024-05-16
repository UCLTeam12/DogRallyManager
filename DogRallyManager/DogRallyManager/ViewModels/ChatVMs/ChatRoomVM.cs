namespace DogRallyManager.ViewModels.ChatVMs
{
    public class ChatRoomVM
    {
        public int Id { get; set; }
        public string? ChatRoomName { get; set; }
        public ICollection<ChatMessageVM> ChatMessages { get; set; }

    }
}
