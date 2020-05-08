using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using WSChat.Backend.Domain.Enums;

namespace WSChat.Backend.Domain.Models
{
    public class Message
    {
        public Message()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; }
        public EventEnum Event { get; set; }
        public DateTime SendDate => DateTime.Now;
        public string MessageText { get; set; }
        public ChatUser User { get; set; }
        public Room Room { get; set; }
        public IList<Room> AvailableRooms { get; set; }
        public string TargetUserId { get; set; }
        public bool? IsPrivate { get; set; }
        public ChatUser TargetUser { get; set; }
    }
}
