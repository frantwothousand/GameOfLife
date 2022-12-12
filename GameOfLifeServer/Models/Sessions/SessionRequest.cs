using GameOfLifeServer.Entities;
using System.ComponentModel.DataAnnotations;

namespace GameOfLifeServer.Models.Sessions;

public class SessionRequest
{
    [Required]
    public string Name { get; set; }
    
    
    public List<User> Users { get; set; }
}
