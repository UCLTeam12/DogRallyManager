namespace DogRallyManager.ViewModels.ChatVMs
{
    public class ChatMessageVM
    {
        public string Author { get; set; }

        public string Message { get; set; }

        public DateTime SentAt { get; set; } = DateTime.Now;

    }
}
