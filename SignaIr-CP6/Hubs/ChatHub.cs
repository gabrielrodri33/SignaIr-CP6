using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;

namespace SignaIr_CP6.Hubs
{
    public class ChatHub : Hub
    {
        //Dict para armazenar o nome de user por ConnectionId
        private static readonly ConcurrentDictionary<string, string> _userName = new ConcurrentDictionary<string, string>();
        public async Task SendMessage(string user, string message)
        {
            //Console.WriteLine($"Mensagem recebida no servidor: {user}: {message}");
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        //Método para notificar quando um usuário entra
        public async Task UserJoined(string userName)
        {
            //Console.WriteLine($"{userName} entrou no chat");
            _userName[Context.ConnectionId] = userName;

            await Clients.All.SendAsync("ReceiveMessage", "Sistema", $"{userName} entrou do chat.");

            foreach(var user in _userName)
            {
                Console.WriteLine($"ConnectionId: {user.Key}, Username: {user.Value}");
            }
        }

        //Método para notificar quando um usuário sai
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            string userName = _userName.TryRemove(Context.ConnectionId.ToString(), out var name) ? name : "Usuário desconhecido";

            Console.WriteLine($"{userName} saiu no chat");

            await Clients.All.SendAsync("ReceiveMessage", "Sistema", $"{userName} saiu do chat.");

            await base.OnDisconnectedAsync(exception);
        }
    }
}
