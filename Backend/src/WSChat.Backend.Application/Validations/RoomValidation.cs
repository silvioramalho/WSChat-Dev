using FluentValidation;
using WSChat.Backend.Domain.Models;

namespace WSChat.Backend.Application.Validations
{
    public class RoomValidation : AbstractValidator<Room>
    {
        public RoomValidation()
        {
            RuleFor(expression: f => f.Name)
                .NotEmpty()
                .Length(min: 3, max: 20)
                .Matches(@"^[a-zA-Z0-9]+$").WithMessage("The Room {PropertyName} must contain only numbers and letters.");
        }
    }
}
