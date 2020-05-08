using System;
using System.Diagnostics.CodeAnalysis;
using WSChat.Backend.Domain.Enums;

namespace WSChat.Backend.Application.DTO
{
    public class ChatPayloadDTO
    {
        public EventEnum Event { get; set; }
        public string UserId { get; set; }
        public int? RoomId { get; set; }
        public string TargetUserId { get; set; }
        public bool? IsPrivate { get; set; }
        public string Message { get; set; }
    }
}
