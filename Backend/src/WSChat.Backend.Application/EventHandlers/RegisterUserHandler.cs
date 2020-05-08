using System;
using WSChat.Backend.Application.Interfaces;
using WSChat.Backend.Domain.Enums;
using WSChat.Backend.Domain.Models;

namespace WSChat.Backend.Application.EventHandlers
{
    public class RegisterUserHandler : BaseHandler
    {
        public RegisterUserHandler(IChatUserService userService)
            : base(userService) { }

        protected override Message HandleMessage(string socketId, Message message)
        {
            var user = CreateUser(socketId, message.MessageText);
            AddUser(user);
            message.User = user;

            message.MessageText = $"Welcome {user.Nickname} to our chat.";

            return message;
        }

        protected override void ValidateMessage(string socketId, Message message)
        {
            isUserRegistered(socketId);
            isNicknameAlredyRegistered(message.MessageText);
        }

        private void isUserRegistered(string socketId)
        {
            if (GetUserById(socketId) != null)
            {
                throw new Exception("You are already registered in the system.");
            }
        }

        private void isNicknameAlredyRegistered(string nickname)
        {
            if(!IsUniqueNickname(nickname))
            {
                throw new Exception("This nickname is already registered. Please try another.");
            }
        }

        
    }
}
