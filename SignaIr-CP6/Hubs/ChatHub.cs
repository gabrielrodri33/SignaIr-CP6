using Microsoft.AspNetCore.SignalR;
using SignaIr_CP6.Model;
using System.Collections.Concurrent;

namespace SignaIr_CP6.Hubs
{
    public class ChatHub : Hub
    {
        //Lista para armazenar as mensagens enviadas.
        private static readonly List<ChatMessage> _messageHistory = new List<ChatMessage>();

        //Dict para armazenar o nome de user por ConnectionId
        private static readonly ConcurrentDictionary<string, string> _userName = new ConcurrentDictionary<string, string>();

        //Método para notificar quando um usuário entra
        public async Task UserJoined(string userName)
        {
            _userName[Context.ConnectionId] = userName;

            await Clients.All.SendAsync("ReceiveMessage", "Sistema", $"{userName} entrou do chat.");

            foreach (var user in _userName)
            {
                Console.WriteLine($"ConnectionId: {user.Key}, Username: {user.Value}");
            }

            foreach (var message in _messageHistory)
            {
                await Clients.Caller.SendAsync("ReceiveMessage", message.User, message.Message);
            }
            await Clients.Caller.SendAsync("ReceiveMessageHistory", _messageHistory);

            //PrintMessageHistory();
        }

        //Método para enviar mensagens
        public async Task SendMessage(string user, string message)
        {
            if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(message))
            {
                Console.WriteLine("Erro: O usuário ou a mensagem estão vazios.");
                return;
            }

            Console.WriteLine($"Mensagem recebida no servidor: {user}: {message}");

            var chatMessage = new ChatMessage { User = user, Message = message };
            _messageHistory.Add(chatMessage);

            Console.WriteLine($"Mensagem adicionada ao histórico: {chatMessage.User}: {chatMessage.Message}");

            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        //Método para enviar o histórico de mensagem ao novo usuário
        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
        }

        //Método para notificar quando um usuário sai
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            string userName = _userName.TryRemove(Context.ConnectionId.ToString(), out var name) ? name : "Usuário desconhecido";

            Console.WriteLine($"{userName} saiu no chat");

            await Clients.All.SendAsync("ReceiveMessage", "Sistema", $"{userName} saiu do chat.");

            await base.OnDisconnectedAsync(exception);
        }

        //Método apenas para mostrar o histórico de mensagem no console (Debug apenas)
        //public void PrintMessageHistory()
        //{
        //    if (_messageHistory.Count > 0)
        //    {
        //        foreach (var msg in _messageHistory)
        //        {
        //            Console.WriteLine($"{msg.User}: {msg.Message}"); 
        //        }
        //    }
        //    else
        //    {
        //        Console.WriteLine("O histórico de mensagens está vazio.");
        //    }
        //}
    }
}
