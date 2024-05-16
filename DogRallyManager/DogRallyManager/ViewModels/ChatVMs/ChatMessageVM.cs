using DogRallyManager.Entities;

namespace DogRallyManager.ViewModels.ChatVMs;

public class ChatMessageVM
{
    public RallyUser Sender { get; set; }

    public string MessageBody { get; set; }

    public int ChatRoomId { get; set; }

    public DateTime TimeStamp { get; set; } = DateTime.UtcNow;
}