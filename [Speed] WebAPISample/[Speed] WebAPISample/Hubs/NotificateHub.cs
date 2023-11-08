using Microsoft.AspNetCore.SignalR;

namespace _Speed__WebAPISample.Hubs
{
    public class NotificateHub : Hub
    {
        public async Task SendMessage( string message)
        {
            await Clients.All.SendAsync("ReceiveStatus",  message);
        }
    }
}
