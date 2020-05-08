using System;
using WSChat.Backend.Domain.Enums;
using WSChat.Backend.Domain.Models;

namespace WSCHat.Backend.Tests.MockData
{
    /// <summary>
    /// Testing Message when Event is Create Room.
    /// Required Fields:
    /// - Event
    /// - MessageText = Room Name
    /// - User Not Null, IdActiveRoom Null
    /// </summary>
    public static class CreateRoomMessageMockData
    {
        private static readonly EventEnum eventTest = EventEnum.CreateRoom;

        #region .: Error :.

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

        public static Message UserOutsideRoomWithoutRoomName()
        {
            var message = EventOK();
            message.User = new ChatUser(Guid.NewGuid().ToString("N"), "Mary");
            return message;
        }

        public static Message UserInsideRoomWithoutRoomName()
        {
            var message = UserOutsideRoomWithoutRoomName();
            message.User.IdActiveRoom = 1;
            return message;
        }

        public static Message UserOutsideRoomRoomName2Chars()
        {
            var message = UserOutsideRoomWithoutRoomName();
            message.MessageText = "Hi";
            return message;
        }

        public static Message UserOutsideRoomRoomName11chars()
        {
            var message = UserOutsideRoomWithoutRoomName();
            message.MessageText = "12345678901";
            return message;
        }

        public static Message UserOutsideRoomRoomNameSpecialChars()
        {
            var message = UserOutsideRoomWithoutRoomName();
            message.MessageText = "John@34";
            return message;
        }

        public static Message UserInsideRoomWithRoomName()
        {
            var message = UserOutsideRoomRoomNameOK();
            message.User.IdActiveRoom = 1;
            return message;
        }


        #endregion

        #region .: Success :. 

        public static Message UserOutsideRoomRoomNameOK()
        {
            var message = UserOutsideRoomWithoutRoomName();
            message.MessageText = "general2";
            return message;
        }

        public static Message UserOutsideRoomRoomNameOKOtherFieldsOK()
        {
            var message = UserOutsideRoomRoomNameOK();
            message.IsPrivate = true;
            message.TargetUserId = "any";
            return message;
        }

        #endregion
    }
}
