using System;
using WSChat.Backend.Application.Interfaces;
using WSChat.Backend.Domain.Enums;
using WSChat.Backend.Domain.Models;

namespace WSChat.Backend.Application.EventHandlers
{
    public class ExitRoomHandler: BaseHandler
    {

        public ExitRoomHandler(            
            IChatUserService userService,
            IRoomService roomService)
            : base(userService,roomService) { }

        protected override Message HandleMessage(string socketId, Message message)
        {
            message.User.IdActiveRoom = null;
            UpdateUser(message.User);

            message.Event = EventEnum.GoodbyeMessage;
            message.User = message.User;
            message.MessageText = $"Goodbye. You left the room.";

            return message;
        }

        protected override void ValidateMessage(string socketId, Message message)
        {
            // IsRegisterUser(message.User);
            // IsInsideARoom(message.User);
        }

        //private void IsRegisterUser(ChatUser user)
        //{
        //    if (user == null)
        //    {
        //        throw new Exception($"You are not registered.");
        //    }
        //}

        //private void IsInsideARoom(ChatUser user)
        //{
        //    if (!user.IdActiveRoom.HasValue)
        //    {
        //        throw new Exception($"You are not inside a room.");
        //    }
        //}
    }
}
