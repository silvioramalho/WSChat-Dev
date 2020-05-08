using System;
using WSChat.Backend.Domain.Enums;
using WSChat.Backend.Domain.Models;

namespace WSCHat.Backend.Tests.MockData
{
    /// <summary>
    /// Testing Message when Event is Register User.
    /// Required Fields:
    /// - Event
    /// - MessageText = idRoom
    /// - User = NULL
    /// </summary>
    public static class RegisterUserMessageMockData
    {
        private static readonly EventEnum eventTest = EventEnum.RegisterUser;

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

        public static Message NicknameNull()
        {
            var message = EventOK();
            return message;
        }

        public static Message Nickname2Chars()
        {
            var message = EventOK();
            message.MessageText = "Hi";
            return message;
        }

        public static Message Nickname21chars()
        {
            var message = EventOK();
            message.MessageText = "123456789012345678901";
            return message;
        }

        public static Message NicknameSpecialChars()
        {
            var message = EventOK();
            message.MessageText = "John@34";
            return message;
        }

        public static Message UserRegistered()
        {
            var message = NicknameOK();
            message.MessageText = "John";
            message.User = new ChatUser(Guid.NewGuid().ToString("N"), "Mary");

            return message;
        }

        #endregion

        #region .: Success :. 

        public static Message NicknameOK()
        {
            var message = EventOK();
            message.MessageText = "John";
            return message;
        }

        #endregion
    }
}
