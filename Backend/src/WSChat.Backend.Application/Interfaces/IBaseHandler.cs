using System;
using WSChat.Backend.Domain.Models;

namespace WSChat.Backend.Application.Interfaces
{
    public interface IBaseHandler
    {
        public Message HandleMessage(string socketId, Message message);
        protected Message ValidateMessage(string socketId, Message message);
    }
}
