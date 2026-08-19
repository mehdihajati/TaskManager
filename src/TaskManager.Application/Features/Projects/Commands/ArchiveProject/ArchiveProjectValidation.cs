using FluentValidation;

namespace TaskManager.Application.Features.Tasks.Commands.ArchiveProject;

public class ArchiveProjectValidation : AbstractValidator<ArchiveProjectCommand>
{
    public ArchiveProjectValidation()
    {
        RuleFor(x => x.ProjectId).NotEmpty().WithMessage("you need to select project");
    }
}