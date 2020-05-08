using NUnit.Framework;
using FluentValidation.TestHelper;
using WSChat.Backend.Application.Validations;
using WSCHat.Backend.Tests.MockData;

namespace WSCHat.Backend.Tests.Messages
{
    [TestFixture]
    public class ExitRoomMessageTest
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
            var message = ExitRoomMessageMockData.EventNull();
            validator.ShouldHaveValidationErrorFor(x => x.Event, message);
        }


        [Test]
        public void Should_have_error_when_User_is_outside_room()
        {
            var message = ExitRoomMessageMockData.UserOutsideRoom();
            validator.ShouldHaveValidationErrorFor(x => x.User.IdActiveRoom.HasValue, message);
        }

        #endregion

        #region .: Success :.

        [Test]
        public void Should_Not_have_error_when_User_is_inside_room()
        {
            var message = ExitRoomMessageMockData.UserInsideRoomOK();
            validator.ShouldNotHaveValidationErrorFor(x => x.User.IdActiveRoom, message);
        }

        [Test]
        public void Should_Not_have_error_when_User_is_inside_room_with_MessageText()
        {
            var message = ExitRoomMessageMockData.UserInsideRoomWithMessageTextOK();
            validator.ShouldNotHaveValidationErrorFor(x => x.User.IdActiveRoom, message);
        }

        [Test]
        public void Should_Not_have_error_when_User_is_inside_room_with_other_fields()
        {
            var message = ExitRoomMessageMockData.UserInsideRoomWithOtherFieldsOK();
            validator.ShouldNotHaveValidationErrorFor(x => x.User.IdActiveRoom, message);
        }

        #endregion
    }
}