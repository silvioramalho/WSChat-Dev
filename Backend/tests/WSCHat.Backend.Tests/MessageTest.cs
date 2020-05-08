using NUnit.Framework;
using FluentValidation;
using FluentValidation.TestHelper;
using WSChat.Backend.Application.Validations;
using WSChat.Backend.Domain.Models;
using WSChat.Backend.Domain.Enums;

namespace WSCHat.Backend.Tests
{
    [TestFixture]
    public class MessageTester
    {
        private MessageValidation validator;

        [SetUp]
        public void Setup()
        {

            validator = new MessageValidation();
        }

        [Test]
        public void Should_have_error_when_Name_is_null()
        {
            var message = new Message();
            message.Event = EventEnum.RegisterUser;
            message.MessageText = "asd";
            var result = validator.TestValidate(message);
            validator.ShouldNotHaveValidationErrorFor(x => x.MessageText, message);

            //result.ShouldHaveValidationErrorFor(x => x.MessageText);
            //validator.ShouldHaveValidationErrorFor(message => message.MessageText, null as string);
            //Assert.Pass();
        }

        [Test]
        public void Should_not_have_error_when_Name_is_null_specified()
        {
            var message = new Message();
            message.Event = EventEnum.RegisterUser;
            message.MessageText = "asd";
            var result = validator.TestValidate(message);
            validator.ShouldNotHaveValidationErrorFor(x => x.MessageText, message);
        }
    }
}