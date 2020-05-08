using System;
using WSChat.Backend.Domain.Enums;
using WSChat.Backend.Domain.Models;

namespace WSCHat.Backend.Tests.MockData
{
    /// <summary>
    /// Testing Message when Event is Messaging.
    /// Required Fields:
    /// - Event
    /// - User (IdActiveRoom not null)
    /// - MessageText = message text 
    /// </summary>
    public static class MessagingMessageMockData
    {
        private static readonly EventEnum eventTest = EventEnum.Messaging;

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

        public static Message MessageWithouUser()
        {
            var message = EventOK();
            message.MessageText = "Hi";

            return message;
        }

        public static Message UserOutsideRoomWithouMessage()
        {
            var message = EventOK();
            message.User = new ChatUser(Guid.NewGuid().ToString("N"), "Mary");

            return message;
        }

        public static Message UserOutsideRoomWithMessage()
        {
            var message = UserOutsideRoomWithouMessage();
            message.MessageText = "Hi";

            return message;
        }

        public static Message UserInsideRoomWithoutMessage()
        {
            var message = UserOutsideRoomWithouMessage();
            message.User.IdActiveRoom = 1;
            return message;
        }

        public static Message UserInsideRoomWithMessage201Chars()
        {
            var message = UserInsideRoomWithoutMessage();
            message.MessageText = "123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901";

            return message;
        }

        #endregion

        #region .: Success :.

        public static Message UserInsideRoomWithMessageOK()
        {
            var message = UserInsideRoomWithoutMessage();
            message.MessageText = "Hi";

            return message;
        }

        public static Message UserInsideRoomWithMessageNumbersOK()
        {
            var message = UserInsideRoomWithoutMessage();
            message.MessageText = "123123123";
            return message;
        }

        public static Message UserInsideRoomWithMessage1CharOK()
        {
            var message = UserInsideRoomWithoutMessage();
            message.MessageText = "a";
            return message;
        }

        public static Message UserInsideRoomWithMessageSpecialCharOK()
        {
            var message = UserInsideRoomWithoutMessage();
            message.MessageText = "21@3ljhlkj&ˆ%*&ˆ%*ˆ&%jkjhlkjhljkh  kjhkjlh jhljkhlkjh   &ˆ*&4";
            return message;
        }

        public static Message UserInsideRoomWithMessageOtherFieldsOK()
        {
            var message = UserInsideRoomWithoutMessage();
            message.MessageText = "Hi";
            message.TargetUserId = "asd89as7d98a7sd9";
            message.IsPrivate = true;

            return message;
        }

        #endregion


    }
}
