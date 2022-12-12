namespace GameOfLifeServer.Entities;

public class User
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string lastConnectionId { get; set; }
}