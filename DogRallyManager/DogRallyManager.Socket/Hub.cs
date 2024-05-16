using Microsoft.AspNetCore.SignalR;

namespace DogRallyManager.Socket
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string user, string message, int chatroomId)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message, chatroomId);
        }
    }
}