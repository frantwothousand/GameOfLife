using Microsoft.AspNetCore.SignalR;
using GameOfLifeServer.Models;
using GameOfLifeServer.Services;
using GameOfLifeServer.Entities;
using GameOfLifeServer.Helpers;

namespace GameOfLifeServer.Hubs
{
    public class SessionHub : Hub
    {
        private SessionService _session;
        private UserService _user;

        private int _actualplayers = new int();

        public override Task OnConnectedAsync()
        {
            //Random _rnd = new Random();
            return base.OnConnectedAsync();
        }

        public async Task GetContextId()
        {
            await Clients.Caller.SendAsync("RecieveContextId", Context.ConnectionId);
        }

        public async Task PlayerHasJoined() {
            _actualplayers++;
            await Clients.All.SendAsync("UpdatePlayers", _actualplayers);
        }

        public async Task BroadcastPlayerName(string playername)
        {
            //await Clients.AllExcept
            await Clients.AllExcept(Context.ConnectionId).SendAsync("PlayerJoined", playername);
        }

        public async Task BroadcastCells(List<Person> _obj)
        {
            await Clients.All.SendAsync("UpdateCells", _obj);
        }

            /* Session Hub.
             * Esto permite que los clientes se conecten a la sesión, y que se comuniquen entre ellos.
             * 
             * Features:
             * - Crear una sesión.
             * - Enviar mensajes a la sesion
             * - Enviar mensajes a un usuario en particular? (No se si sea necesario)
             */

        /*public SessionHub(SessionService session, UserService user)
        {
            _session = session;
            _user = user;
        }
        
        public async Task CreateSession(Session model)
        {
            _session.Create(model);
            await Clients.Caller.SendAsync("SessionCreated", $"Se ha creado la sala ${model.Name} y su anfitrión es ${model.OwnerName}.");
        }

        public Task Send(MessageType message)
        {
            if(!_session.SessionExists(message.Group))
            {
                //throw new System.Exception("No se puede enviar un mensaje a una sesión que no existe.")
                return Clients.Caller.SendAsync("Error", $"La sesión {message.Group} no existe.");
            }
            _session.CreateNewMessage(message);
            return Clients.GroupExcept(message.Group, Context.ConnectionId).SendAsync("SendGroup", $"[{DateTime.Now}] {message.Author}: {message.Content}");
        }

        public async Task JoinGroup(string groupName)
        {
            if (!_session.SessionExists(groupName))
            {
                await Clients.Caller.SendAsync("Error", "La sesión no existe.");
                return;
            }
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
            await Clients.Group(groupName).SendAsync("JoinGroup", groupName);

            var history = _session.GetSessions();
            await Clients.Client(Context.ConnectionId).SendAsync("History", history); // TODO: Send only the session history (messages history)
        }

        public async Task LeaveGroup(string groupName)
        {
            if (!_session.SessionExists(groupName))
            {
                await Clients.Caller.SendAsync("Error", "La sesión no existe."); 
                return;
            }
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
            await Clients.Group(groupName).SendAsync("LeaveGroup", groupName);
        }

        //public Task Send()

        /*public Task SendMessageToGroupExcept(params object[] args)
        {
            string groupName = Convert.ToString(args[0])!;
            string playerName = Convert.ToString(args[1])!;
            string message = Convert.ToString(args[2])!;

            return Clients.GroupExcept(groupName, Context.ConnectionId).SendAsync("SendGroup", $"{playerName} dice: {message}");
        }*/

        /*public async Task AddToGroup(params object[] args)
        {
            string groupName = Convert.ToString(args[0])!;
            string playerName = Convert.ToString(args[1])!;

            if (groupName.Equals(""))
            {
                await Clients.Caller.SendAsync("SendException", "Error al unirse a una sala: No se ha especificado el nombre de la sala.");
                return;
            }

            if (!sessions.SessionExists(groupName))
            {
                sessions.CreateSession(groupName, playerName, Context.ConnectionId);
                await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
                await Clients.Group(groupName).SendAsync("Send", $"Se ha creado la sala {groupName} y su anfitrión es {playerName}");
            }
            else
            {
                sessions.JoinSession(groupName, new User { ConnectionId = Context.ConnectionId, playerName = playerName });
                await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
                await Clients.Group(groupName).SendAsync("Send", $"{playerName} se ha unido a la sala {groupName}");
            }
        }

        public async Task GetUsersInGroup(string groupName)
        {
            await Clients.All.SendAsync("Send", sessions.GetAllUsersInSession(groupName).Users.FindAll(x => x.playerName != ""));
        }

        public async Task RemoveFromGroup(string groupName, string playerName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
            
            await Clients.Group(groupName).SendAsync("Send", $"{playerName} ha abandonado la sala {groupName}.");   
        }*/
    }
}
