using FluentValidation;

namespace TaskManager.Application.Features.Projects.Commands.AddMember;

public class AddMemberValidator : AbstractValidator<AddMemberCommand>
{
    public AddMemberValidator()
    {
        RuleFor(x => x.NewMemberId).NotEmpty().WithMessage("You must specify which user to add.");
        RuleFor(x => x.NewMemberRole).IsInEnum().WithMessage("new member should have a role.");
        RuleFor(x => x.ProjectId).NotEmpty().WithMessage("You should choos a project to add members to that.");

    }
}