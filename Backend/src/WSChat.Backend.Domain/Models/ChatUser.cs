using System;

namespace WSChat.Backend.Domain.Models
{
    public class ChatUser
    {
        public ChatUser(string id, string name)
        {
            Nickname = name;
            Id = id;
        }

        public string Nickname { get; set; }
        public string Id { get; set; }
        public int? IdActiveRoom { get; set; }
    }
}
