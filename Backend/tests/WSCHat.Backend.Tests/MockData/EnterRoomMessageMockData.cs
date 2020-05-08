using System;
using WSChat.Backend.Domain.Enums;
using WSChat.Backend.Domain.Models;

namespace WSCHat.Backend.Tests.MockData
{
    /// <summary>
    /// Testing Message when Event is Enter Room.
    /// Required Fields:
    /// - Event
    /// - User (IdActiveRoom = null)
    /// - MessageText = idRoom 
    /// </summary>
    public static class EnterRoomMessageMockData
    {
        private static readonly EventEnum eventTest = EventEnum.EnterRoom;

        #region .: Errors :.

        public static Message EventNull()
        {
            return new Message();
        }

        public static Message EventOK()
        {
            var message = new Message();
            message.Event = eventTest;
            return message;
        }

        public static Message RoomIdNull()
        {
            var message = EventOK();
            return message;
        }

        public static Message RoomIdLetters()
        {
            var message = EventOK();
            message.MessageText = "Hi";
            return message;
        }

        public static Message RoomId6chars()
        {
            var message = EventOK();
            message.MessageText = "123456";
            return message;
        }

        public static Message RoomIdSpecialChars()
        {
            var message = EventOK();
            message.MessageText = "21@34";
            return message;
        }

        public static Message RoomIdOK()
        {
            var message = EventOK();
            message.MessageText = "1";
            return message;
        }

        public static Message UserNotRegistered()
        {
            var message = RoomIdOK();
            message.MessageText = "1";

            return message;
        }

        public static Message UserAlreadyInsideSomeRoom()
        {
            var message = UserRegisteredOK();
            message.User.IdActiveRoom = 2;

            return message;
        }

        public static Message RoomIdLettersUserOK()
        {
            var message = UserRegisteredOK();
            message.MessageText = "Hi";
            return message;
        }

        public static Message RoomId6charsUserOK()
        {
            var message = UserRegisteredOK();
            message.MessageText = "123456";
            return message;
        }

        public static Message RoomIdSpecialCharsUserOK()
        {
            var message = UserRegisteredOK();
            message.MessageText = "21@34";
            return message;
        }

        #endregion

        #region .: Success :.

        public static Message UserRegisteredOK()
        {
            var message = UserNotRegistered();
            message.User = new ChatUser(Guid.NewGuid().ToString("N"), "Mary");

            return message;
        }

        #endregion


    }
}
