namespace WpfGameOfLife.Models;

public enum CellState
{
    Dead,
    Alive
}

public class Cell
{
    public int Row { get; set; }
    public int Column { get; set; }
    public bool IsAlive { get; set; }
}
