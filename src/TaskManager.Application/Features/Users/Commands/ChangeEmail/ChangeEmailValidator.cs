using FluentValidation;

namespace TaskManager.Application.Features.Users.Commands.ChangeEmail;

public class ChangeEmailValidator : AbstractValidator<ChangeEmailCommand>
{
    public ChangeEmailValidator()
    {
        RuleFor(x => x.NewEmail).NotEmpty().WithMessage("New email is reqired")
                                .EmailAddress().WithMessage("Invalid email format.");

    }
}