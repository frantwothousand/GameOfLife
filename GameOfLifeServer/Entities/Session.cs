namespace GameOfLifeServer.Entities;

public class Session
{
    public string Name { get; set; }
    //public string OwnerId { get; set; }
    public string OwnerName { get; set; }
    
    public List<User> Users { get; set; }
}