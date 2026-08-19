using FluentValidation;

namespace TaskManager.Application.Features.Tasks.Commands.ChangeMemberRole;

public class ChangeMemberRoleValidation : AbstractValidator<ChangeMemberRoleCommand>
{
    public ChangeMemberRoleValidation()
    {
        RuleFor(x => x.ProjectId).NotEmpty().WithMessage("You must select project");
        RuleFor(x => x.NewMemberRole).IsInEnum().WithMessage("new role should be assinged.");
        RuleFor(x => x.UserId).NotEmpty().WithMessage("you must select user to assign a role for this project");

    }
}