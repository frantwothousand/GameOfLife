using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLifeApi.Models.Hubs
{
    public class MessageEntity
    {
        public string Author { get; set; }
        public string Content { get; set; }
        public string Group { get; set; }
    }
    
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string lastConnectionId { get; set; }
    }
    
    public class Session
    {
        public string Name { get; }
        //public string OwnerId { get; set; }
        public string OwnerName { get; }

        public List<User> Users { get; set; }


        public Session(string name, string owner)
        {
            Name = name;
            OwnerName = owner;
        }
    }
}
