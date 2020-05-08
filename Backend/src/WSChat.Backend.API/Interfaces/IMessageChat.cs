using System;
using System.Collections.Generic;
using WSChat.Backend.Application.DTO;

namespace WSChat.Backend.API.Interfaces
{
    public interface IMessageChat
    {
        public IList<ChatPayloadDTO> HandleRequest(string socketId, string message);

        public ChatPayloadDTO HandleDisconnection(string userId);
    }
}
