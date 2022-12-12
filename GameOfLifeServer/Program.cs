using GameOfLifeServer.Entities;
using GameOfLifeServer.Helpers;
using GameOfLifeServer.Hubs;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;

namespace GameOfLifeServer
{
    class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();

            while(true)
            {
                lock (GameOfLifeHub.Locker)
                {
                    GameOfLifeHub._cells.AddAdjacentCellsForEachCell();

                    foreach (var cell in GameOfLifeHub._cells)
                    {
                        var aliveCells = cell.GetNumberOfLivingAdjacentCells(GameOfLifeHub._cells);

                        if (cell.IsAlive() && (aliveCells < 2 || aliveCells > 3))
                        {
                            GameOfLifeHub._cellsNexGeneration.Add(new Cell(cell.X, cell.Y, GameOfLifeHub.DeadCellColor));
                        }

                        else if (cell.IsAlive() && (aliveCells == 2 || aliveCells == 3))
                        {
                            GameOfLifeHub._cellsNexGeneration.Add(new Cell(cell.X, cell.Y, cell.Color));
                        }

                        else if (!cell.IsAlive() && aliveCells == 3)
                        {
                            GameOfLifeHub._cellsNexGeneration.Add(new Cell(cell.X, cell.Y, Cell.MixColorsOfRebornCell(GameOfLifeHub._cells, cell)));
                        }

                        GameOfLifeHub._hubContext.Clients.All.SendAsync("setCells", GameOfLifeHub._cellsNexGeneration.Where(c => c.Color != null));

                        GameOfLifeHub._cells = GameOfLifeHub._cellsNexGeneration.Where(c => c.IsAlive() 
                            && c.X >= -2 
                            && c.X <= (GameOfLifeHub.WidthCanvas / GameOfLifeHub.SizeCell) + 2 
                            && c.Y >= -2 && c.Y <= (GameOfLifeHub.HeightCanvas / GameOfLifeHub.SizeCell) + 2)
                            .ToList();

                        GameOfLifeHub._cells.AddRangeOfCells(GameOfLifeHub._newCells);

                        GameOfLifeHub._newCells.Clear();
                        GameOfLifeHub._cellsNexGeneration.Clear();

                        Thread.Sleep(1000);
                    }
                }
            }
        }

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
        
    }
}