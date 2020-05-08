using System;
using System.Collections.Generic;

namespace WSChat.Backend.Domain.Models
{
    public class Room
    {
        public Room(int id, string name)
        {
            Id = id;
            Name = name;
            Users = new List<ChatUser>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public IList<ChatUser> Users { get; set; }
        public ChatUser Owner { get; set; }
    }
}
