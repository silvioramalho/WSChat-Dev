using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WSChat.Backend.Application.DTO;

namespace WSChat.Backend.Application.Interfaces
{
    public interface IMessageService
    {
        public IList<ChatPayloadDTO> HandleRequest(string socketId, string message);

        public ChatPayloadDTO HandleDisconnection(string userId);
    }
}
