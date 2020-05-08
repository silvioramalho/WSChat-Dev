using System;
using FluentValidation;
using WSChat.Backend.Domain.Models;

namespace WSChat.Backend.Application.Validations
{
    public class ChatUserValidation : AbstractValidator<ChatUser>
    {
        public ChatUserValidation()
        {
            RuleFor(expression: f => f.Nickname)
                .NotEmpty()
                .Length(min: 3, max: 20)
                .Matches(@"^[a-zA-Z0-9]+$").WithMessage("The {PropertyName} must contain only numbers and letters");
        }
    }
}
