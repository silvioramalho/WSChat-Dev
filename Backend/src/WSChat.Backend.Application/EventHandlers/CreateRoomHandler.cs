using System;
using WSChat.Backend.Application.Interfaces;
using WSChat.Backend.Domain.Enums;
using WSChat.Backend.Domain.Models;

namespace WSChat.Backend.Application.EventHandlers
{
    public class CreateRoomHandler : BaseHandler
    {
        public CreateRoomHandler(
            IChatUserService userService,
            IRoomService roomService)
            : base(userService, roomService) { }


        protected override Message HandleMessage(string userId, Message message)
        {

            int roomId = CreateRoom(message.MessageText);
            var room = GetRoomById(roomId);
            room.Owner = message.User;
            UpdateRoom(room);

            message.Room = room;
            message.MessageText = $"Your room #{room.Name} was created.";

            return message;
        }

        protected override void ValidateMessage(string userId, Message message)
        {
            ValidateUniqueRoom(userId);
            ValidateUniqueRoomName(message.MessageText);
        }

        private void ValidateUniqueRoom(string idUser)
        {
            if (ExistOtherUserRoom(idUser))
            {
                throw new Exception("You already have your own room.");
            }
        }

        private void ValidateUniqueRoomName(string roomName)
        {
            if (!IsUniqueRoomName(roomName))
            {
                throw new Exception("This room name is already registered. Try another one.");
            }
        }

    }
}