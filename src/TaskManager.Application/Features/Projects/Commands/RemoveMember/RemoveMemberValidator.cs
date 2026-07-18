using FluentValidation;

namespace TaskManager.Application.Features.Projects.Commands.RemoveMember;

public class RemoveMemberValidator:AbstractValidator<RemoveMemberCommand>
{
    public RemoveMemberValidator()
    {
        RuleFor(x=>x.ProjectId).NotEmpty().WithMessage("You must select project");
        RuleFor(x => x.UserId).NotEmpty().WithMessage("you must select user to remove from project");
    }
}