using NUnit.Framework;
using FluentValidation.TestHelper;
using WSChat.Backend.Application.Validations;
using WSCHat.Backend.Tests.MockData;

namespace WSCHat.Backend.Tests.Messages
{
    [TestFixture]
    public class EnterRoomMessageTest
    {
        private MessageValidation validator;


        [SetUp]
        public void Setup()
        {

            validator = new MessageValidation();
        }

        #region .: Error :.

        [Test]
        public void Should_have_error_when_Event_is_null()
        {
            var message = EnterRoomMessageMockData.EventNull();
            validator.ShouldHaveValidationErrorFor(x => x.Event, message);
        }


        [Test]
        public void Should_have_error_when_RoomId_is_null()
        {
            var message = EnterRoomMessageMockData.RoomIdNull();
            validator.ShouldHaveValidationErrorFor(x => x.MessageText, message);
        }

        [Test]
        public void Should_have_error_when_RoomId_have_letters()
        {
            var message = EnterRoomMessageMockData.RoomIdLetters();
            validator.ShouldHaveValidationErrorFor(x => x.MessageText, message);
        }

        [Test]
        public void Should_have_error_when_RoomId_have_6_chars()
        {
            var message = EnterRoomMessageMockData.RoomId6chars();
            validator.ShouldHaveValidationErrorFor(x => x.MessageText, message);
        }

        [Test]
        public void Should_have_error_when_RoomId_have_special_chars()
        {
            var message = EnterRoomMessageMockData.RoomIdSpecialChars();
            validator.ShouldHaveValidationErrorFor(x => x.MessageText, message);
        }

        [Test]
        public void Should_have_error_when_User_is_not_registred()
        {
            var message = EnterRoomMessageMockData.UserNotRegistered();
            validator.ShouldHaveValidationErrorFor(x => x.User, message);
        }

        [Test]
        public void Should_have_error_when_User_is_already_in_room()
        {
            var message = EnterRoomMessageMockData.UserAlreadyInsideSomeRoom();
            validator.ShouldHaveValidationErrorFor(x => x.User.IdActiveRoom, message);
        }

        [Test]
        public void Should_have_error_when_User_OK_RoomId_Letters()
        {
            var message = EnterRoomMessageMockData.RoomIdLettersUserOK();
            validator.ShouldHaveValidationErrorFor(x => x.MessageText, message);
        }

        [Test]
        public void Should_have_error_when_User_OK_RoomId_6_Chars()
        {
            var message = EnterRoomMessageMockData.RoomId6charsUserOK();
            validator.ShouldHaveValidationErrorFor(x => x.MessageText, message);
        }

        [Test]
        public void Should_have_error_when_User_OK_RoomId_Special_Chars()
        {
            var message = EnterRoomMessageMockData.RoomIdSpecialCharsUserOK();
            validator.ShouldHaveValidationErrorFor(x => x.MessageText, message);
        }


        #endregion

        #region .: Success :.

        [Test]
        public void Should_Not_have_error_when_User_is_registred()
        {
            var message = EnterRoomMessageMockData.UserRegisteredOK();
            validator.ShouldNotHaveValidationErrorFor(x => x.User, message);
        }

        #endregion
    }
}