using Microsoft.AspNetCore.SignalR;
using KoalitionServer.Models;

namespace KoalitionServer.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(Message message)
        {
            await Clients.All.SendAsync("ReceiveMessage", message);
        }
    }
}
