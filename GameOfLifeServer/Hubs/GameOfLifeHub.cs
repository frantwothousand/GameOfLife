using GameOfLifeServer.Entities;
using Microsoft.AspNetCore.SignalR;

namespace GameOfLifeServer.Hubs;

public class GameOfLifeHub : Hub
{
    /* Celulas del Juego de la Vida
         * 
         * Features:
         * - Crear una celula
         * - Blablabla.
         */

    public static int WidthCanvas = 640;
    public static int HeightCanvas = 480;
    public const int SizeCell = 10;
    public const string DeadCellColor = "#FAFAFA";

    public static List<User> _players = new List<User>();
    public static List<Cell> _newCells = new List<Cell>();
    public static List<Cell> _cells = new List<Cell>();
    public static List<Cell> _cellsNexGeneration = new List<Cell>();

    public static readonly Thread? Game;

    public static readonly object Locker = new object();

    public static readonly IHubContext<GameOfLifeHub> _hubContext;
}
