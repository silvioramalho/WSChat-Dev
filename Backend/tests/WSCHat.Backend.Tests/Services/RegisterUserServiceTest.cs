using NUnit.Framework;
using FluentValidation.TestHelper;
using WSChat.Backend.Application.Interfaces;
using WSChat.Backend.Application.Validations;
using WSCHat.Backend.Tests.MockData;
using WSChat.Backend.Application.Services;
using WSChat.Backend.Application.Extensions;
using WSChat.Backend.Domain.Models;
using WSChat.Backend.Domain.Enums;
using WSChat.Backend.Application.DTO;

namespace WSCHat.Backend.Tests.Services
{
    [TestFixture]
    public class RegisterUserServiceTest
    {
        private IMessageService _messageService;
        private IRoomService _roomService;
        private IChatUserService _userService;
        private string socketId = "a6cf4f3b80f748a69cd1463eea292c9a";


        [SetUp]
        public void Setup()
        {
            _roomService = new RoomService();
            _userService = new ChatUserService();
            _messageService = new MessageService(_userService, _roomService);
        }

        #region .: Error :.

        [Test]
        public void Should_have_error_when_Event_is_null()
        {
            var message = RegisterUserMessageMockData.EventNull().SerializeMessage();
            var result = _messageService.HandleRequest(socketId, message);
            Assert.That(result[0].Event, Is.EqualTo(EventEnum.Error));
        }


        [Test]
        public void Should_have_error_when_Nickname_is_null()
        {
            var message = RegisterUserMessageMockData.NicknameNull().SerializeMessage();
            var result = _messageService.HandleRequest(socketId, message);
            Assert.That(result[0].Event, Is.EqualTo(EventEnum.Error));
        }

        [Test]
        public void Should_have_error_when_Nickname_have_2_chars()
        {
            var message = RegisterUserMessageMockData.Nickname2Chars().SerializeMessage();
            var result = _messageService.HandleRequest(socketId, message);
            Assert.That(result[0].Event, Is.EqualTo(EventEnum.Error));
        }

        [Test]
        public void Should_have_error_when_Nickname_have_21_chars()
        {
            var message = RegisterUserMessageMockData.Nickname21chars().SerializeMessage();
            var result = _messageService.HandleRequest(socketId, message);
            Assert.That(result[0].Event, Is.EqualTo(EventEnum.Error));
        }

        [Test]
        public void Should_have_error_when_Nickname_have_special_chars()
        {
            var message = RegisterUserMessageMockData.NicknameSpecialChars().SerializeMessage();
            var result = _messageService.HandleRequest(socketId, message);
            Assert.That(result[0].Event, Is.EqualTo(EventEnum.Error));
        }

        [Test]
        public void Should_have_error_when_User_is_registred()
        {
            var message = RegisterUserMessageMockData.UserRegistered().SerializeMessage();
            var result = _messageService.HandleRequest(socketId, message);
            Assert.That(result[0].Event, Is.EqualTo(EventEnum.Error));
        }

        #endregion

        #region .: Success :.

        [Test]
        public void Should_not_have_error_when_Nickname_is_specified()
        {
            var message = RegisterUserMessageMockData.NicknameOK().SerializeMessage();
            var result = _messageService.HandleRequest(socketId, message);
            Assert.That(result[0].Event, !Is.EqualTo(EventEnum.Error));
        }

        #endregion
    }
}
