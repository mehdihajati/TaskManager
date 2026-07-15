using FluentValidation;

namespace TaskManager.Application.Features.Users.Commands.DeleteAccount;

public class DeleteAccountValidator : AbstractValidator<DeleteAccountCommand>
{
    public DeleteAccountValidator()
    {
        RuleFor(x => x.CurrentPassword).NotEmpty().WithMessage("You should Enter the current password");

    }
}