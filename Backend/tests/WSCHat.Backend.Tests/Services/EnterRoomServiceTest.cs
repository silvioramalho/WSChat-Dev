using NUnit.Framework;
using WSCHat.Backend.Tests.MockData;
using WSChat.Backend.Application.Interfaces;
using WSChat.Backend.Domain.Enums;
using WSChat.Backend.Application.Extensions;
using WSChat.Backend.Application.Services;

namespace WSCHat.Backend.Tests.Services
{
    [TestFixture]
    public class EnterRoomServiceTest
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
            var message = EnterRoomMessageMockData.EventNull().SerializeMessage();
            var result = _messageService.HandleRequest(socketId, message);
            Assert.That(result[0].Event, Is.EqualTo(EventEnum.Error));
        }


        [Test]
        public void Should_have_error_when_RoomId_is_null()
        {
            var message = EnterRoomMessageMockData.RoomIdNull().SerializeMessage();
            var result = _messageService.HandleRequest(socketId, message);
            Assert.That(result[0].Event, Is.EqualTo(EventEnum.Error));
        }

        [Test]
        public void Should_have_error_when_RoomId_have_letters()
        {
            var message = EnterRoomMessageMockData.RoomIdLetters().SerializeMessage();
            var result = _messageService.HandleRequest(socketId, message);
            Assert.That(result[0].Event, Is.EqualTo(EventEnum.Error));
        }

        [Test]
        public void Should_have_error_when_RoomId_have_6_chars()
        {
            var message = EnterRoomMessageMockData.RoomId6chars().SerializeMessage();
            var result = _messageService.HandleRequest(socketId, message);
            Assert.That(result[0].Event, Is.EqualTo(EventEnum.Error));
        }

        [Test]
        public void Should_have_error_when_RoomId_have_special_chars()
        {
            var message = EnterRoomMessageMockData.RoomIdSpecialChars().SerializeMessage();
            var result = _messageService.HandleRequest(socketId, message);
            Assert.That(result[0].Event, Is.EqualTo(EventEnum.Error));
        }

        [Test]
        public void Should_have_error_when_User_is_not_registred()
        {
            var message = EnterRoomMessageMockData.UserNotRegistered().SerializeMessage();
            var result = _messageService.HandleRequest(socketId, message);
            Assert.That(result[0].Event, Is.EqualTo(EventEnum.Error));
        }

        [Test]
        public void Should_have_error_when_User_is_already_in_room()
        {
            var message = EnterRoomMessageMockData.UserAlreadyInsideSomeRoom().SerializeMessage();
            var result = _messageService.HandleRequest(socketId, message);
            Assert.That(result[0].Event, Is.EqualTo(EventEnum.Error));
        }

        [Test]
        public void Should_have_error_when_User_OK_RoomId_Letters()
        {
            var message = EnterRoomMessageMockData.RoomIdLettersUserOK().SerializeMessage();
            var result = _messageService.HandleRequest(socketId, message);
            Assert.That(result[0].Event, Is.EqualTo(EventEnum.Error));
        }

        [Test]
        public void Should_have_error_when_User_OK_RoomId_6_Chars()
        {
            var message = EnterRoomMessageMockData.RoomId6charsUserOK().SerializeMessage();
            var result = _messageService.HandleRequest(socketId, message);
            Assert.That(result[0].Event, Is.EqualTo(EventEnum.Error));
        }

        [Test]
        public void Should_have_error_when_User_OK_RoomId_Special_Chars()
        {
            var message = EnterRoomMessageMockData.RoomIdSpecialCharsUserOK().SerializeMessage();
            var result = _messageService.HandleRequest(socketId, message);
            Assert.That(result[0].Event, Is.EqualTo(EventEnum.Error));
        }


        #endregion

        #region .: Success :.

        [Test]
        public void Should_Not_have_error_when_User_is_registred()
        {
            var message = EnterRoomMessageMockData.UserRegisteredOK().SerializeMessage();
            var result = _messageService.HandleRequest(socketId, message);
            Assert.That(result[0].Event, Is.EqualTo(EventEnum.Error));
        }

        #endregion
    }
}