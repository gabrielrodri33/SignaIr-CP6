using Microsoft.AspNetCore.SignalR;

namespace SignaIr_CP6.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            //Console.WriteLine($"Mensagem recebida no servidor: {user}: {message}");
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}
