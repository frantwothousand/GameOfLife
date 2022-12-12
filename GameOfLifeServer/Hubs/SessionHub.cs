using Microsoft.AspNetCore.SignalR;
using GameOfLifeServer.Models;
using GameOfLifeServer.Services;
using GameOfLifeServer.Entities;
using GameOfLifeServer.Helpers;
using System.Linq;

namespace GameOfLifeServer.Hubs
{
    public class SessionHub : Hub
    {
        private SessionService _session;
        private UserService _user;

        private int _actualplayers = new int();

        public async Task OnConnectedAsync()
        {
            await PlayerHasJoined();
            base.OnConnectedAsync();
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
            await Clients.AllExcept(Context.ConnectionId).SendAsync("PlayerJoined", playername);
        }

        public async Task BroadcastCells(List<Person> _obj)
        {
            await Clients.All.SendAsync("UpdateCells", _obj);
        }

        public Task Send(string user, string message)
        {
            return Clients.AllExcept(Context.ConnectionId).SendAsync("SendMessage", $"[{DateTime.Now}] {user}: {message}");
        }
    }
}
