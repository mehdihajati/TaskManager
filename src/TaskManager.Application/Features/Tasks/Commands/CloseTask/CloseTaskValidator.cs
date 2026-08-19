using FluentValidation;
using TaskManager.Application.Features.Tasks.Commands.UnassignTask;

namespace TaskManager.Application.Features.Tasks.Commands.CloseTask;

public class CloseTaskValidator : AbstractValidator<CloseTaskCommand>
{
    public CloseTaskValidator()
    {
        RuleFor(x => x.TaskId).NotEmpty().WithMessage("TaskId is required");
    }
}