using System.ComponentModel.DataAnnotations;

namespace GameOfLifeServer.Models.Users;
public class UserRequest
{
    [Required]
    public string ConnectionId { get; set; }
    [Required]
    public string PlayerName { get; set; }
}

