using FluentValidation;

namespace TaskManager.Application.Features.Users.Commands.ChangePassword
{
    public class ChangePasswordValidator : AbstractValidator<ChangePasswordCommand>
    {
        public ChangePasswordValidator()
        {
            RuleFor(x => x.CurrentPassword).NotEmpty().WithMessage("Currentpassword should not be Empty");
            RuleFor(x => x.NewPassword).NotEmpty().WithMessage("newpassword should not be Empty")
                                        .MinimumLength(8).WithMessage("new password must be at leaset 8 charecters")
                                        .NotEqual(x => x.CurrentPassword).WithMessage("new password should be different from current password");

        }
    }
}