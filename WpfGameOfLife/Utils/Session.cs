using GameOfLifeApi;
using GameOfLifeClient.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLifeClient.Utils;

public class Match
{
    public Player playerInfo { get; set; }
    public List<Person> _data { get; set; }


    // Matriz que almacene el nombre del jugador y la lista.
    public static List<Match> Info { get; set; } = new List<Match>();
}