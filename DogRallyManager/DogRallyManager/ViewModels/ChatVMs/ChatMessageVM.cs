using DogRallyManager.Entities;

namespace DogRallyManager.ViewModels.ChatVMs;

public class ChatMessageVM
{
    // I would like this not to user Entity but actually a UserViewModel mapped from RallyUser identity... hmm
    public RallyUser Sender { get; set; }

    public string MessageBody { get; set; }

    public int ChatRoomId { get; set; }

    public DateTime TimeStamp { get; set; } = DateTime.UtcNow;
}