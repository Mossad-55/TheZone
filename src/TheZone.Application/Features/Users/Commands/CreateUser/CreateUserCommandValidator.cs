using FluentValidation;

namespace TheZone.Application.Features.Users.Commands.CreateUser;

public sealed class CreateUserCommandValidator 
    : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(x => x.PhoneNumber)
            .NotEmpty()
            .MaximumLength(20);

        RuleFor(x => x.DisplayName)
            .NotEmpty()
            .MaximumLength(50);
    }
}