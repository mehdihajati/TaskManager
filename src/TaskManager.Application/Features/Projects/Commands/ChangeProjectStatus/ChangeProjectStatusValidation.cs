using FluentValidation;

namespace TaskManager.Application.Features.Projects.Commands.ChangeProjectStatus;

public class ChangeProjectStatusValidation : AbstractValidator<ChangeProjectStatusCommand>
{
    public ChangeProjectStatusValidation()
    {
        RuleFor(x => x.ProjectId).NotEmpty().WithMessage("You must select project");
        RuleFor(x => x.NewStatus).IsInEnum().WithMessage("new Status should be assinged.");
    }
}