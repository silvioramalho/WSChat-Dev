using NUnit.Framework;
using WSChat.Backend.Application.Extensions;
using WSChat.Backend.Application.Interfaces;
using WSChat.Backend.Application.Services;
using WSChat.Backend.Domain.Enums;
using WSCHat.Backend.Tests.MockData;

namespace WSCHat.Backend.Tests.Services
{
    [TestFixture]
    public class ExitRoomServiceTest
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
            var message = ExitRoomMessageMockData.EventNull().SerializeMessage();
            var result = _messageService.HandleRequest(socketId, message);
            Assert.That(result[0].Event, Is.EqualTo(EventEnum.Error));
        }


        [Test]
        public void Should_have_error_when_User_is_outside_room()
        {
            var message = ExitRoomMessageMockData.UserOutsideRoom().SerializeMessage();
            var result = _messageService.HandleRequest(socketId, message);
            Assert.That(result[0].Event, Is.EqualTo(EventEnum.Error));
        }

        #endregion

        #region .: Success :.

        [Test]
        public void Should_Not_have_error_when_User_is_inside_room()
        {
            var message = ExitRoomMessageMockData.UserInsideRoomOK().SerializeMessage();
            var result = _messageService.HandleRequest(socketId, message);
            Assert.That(result[0].Event, Is.EqualTo(EventEnum.Error));
        }

        [Test]
        public void Should_Not_have_error_when_User_is_inside_room_with_MessageText()
        {
            var message = ExitRoomMessageMockData.UserInsideRoomWithMessageTextOK().SerializeMessage();
            var result = _messageService.HandleRequest(socketId, message);
            Assert.That(result[0].Event, Is.EqualTo(EventEnum.Error));
        }

        [Test]
        public void Should_Not_have_error_when_User_is_inside_room_with_other_fields()
        {
            var message = ExitRoomMessageMockData.UserInsideRoomWithOtherFieldsOK().SerializeMessage();
            var result = _messageService.HandleRequest(socketId, message);
            Assert.That(result[0].Event, Is.EqualTo(EventEnum.Error));
        }

        #endregion
    }
}