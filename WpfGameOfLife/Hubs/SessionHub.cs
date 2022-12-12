using GameOfLifeApi;
using GameOfLifeClient.Utils;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using WpfGameOfLife;
using WpfGameOfLife.Utils;

namespace GameOfLifeClient.Hubs
{
    public static class SessionHub
    {

        public static async Task Start()
        {
            addListeners();
        }
        /// <summary>
        /// Inicializa la conexión con el servidor.
        /// </summary>
        public static HubConnection sessionHub { get; } = new HubConnectionBuilder()
             .AddJsonProtocol(options => options.PayloadSerializerOptions.PropertyNamingPolicy = null)
             .WithUrl("http://localhost:5000/gameoflife")
             .Build();
        public static HubConnectionState hubConnectionState { get; set; } = HubConnectionState.Disconnected;

        public static async Task Invoke(string method, params object[] args)
        {
            try
            {
                await sessionHub.InvokeAsync(method, args);
            }
            catch (Exception err)
            {
                Log.Add(Brushes.Red, $"Error detectado ({method}): {err.Message}", Launcher.game.messageList);
            }
        }

        private static void addListeners()
        {
            sessionHub.On("SessionCreated", (string message) =>
            {
                Log.Add(Brushes.Blue, message, Launcher.game.messageList);
            });

            sessionHub.On("RecieveContextId", (string message) =>
            {
                Log.Add(Brushes.Aquamarine, $"Tu ID conexión asignada es {message}", Launcher.game.messageList);
            });

            sessionHub.On("UpdatePlayers", (int players) =>
            {
                Launcher.game.playersOnline.Text = players.ToString();
            });

            sessionHub.On("PlayerJoined", (string playername) =>
            {
                Log.Add(Brushes.GreenYellow, $"El jugador {playername} se ha unido.", Launcher.game.messageList);
            });

            sessionHub.On("UpdateCells", (string player, List<Person> data) =>
            {
                Board.LoadFromCollection(Launcher.game.gameBoard, data);
            });
        }

        public static async void BroadcastPlayerName(string _name)
        {
            await sessionHub.InvokeAsync("BroadcastPlayerName", _name);
        }

        public static async void BroadcastCells(List<Person> _obj)
        {
            await Invoke("BroadcastCells", _obj);
        }
    }
} 