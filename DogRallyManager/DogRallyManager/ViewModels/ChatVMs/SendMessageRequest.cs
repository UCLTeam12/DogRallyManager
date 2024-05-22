using DogRallyManager.ViewModels.AccountVMs;

namespace DogRallyManager.ViewModels.ChatVMs
{
    // This class is used as an intermediary object for when typing a new message in a newly created room
    // that only exists on APP-layer.
    // It is used in the post request of ChatController Index view, when a chatroom has an id of 0,
    // and it is then handled by the ChatController SendMessageOnNewlyCreatedRoom action, where it takes an object of this type 
    // as an argument.
    public class SendMessageRequest
    {
         public string MessageBody { get; set; }
         public IEnumerable<string> RecipientUserNames { get; set; }
    }
}
