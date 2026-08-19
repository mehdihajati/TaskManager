using FluentValidation;

namespace TaskManager.Application.Features.Tasks.Commands.ChangeTaskStatus;

public class ChangeTaskStatusValidator:AbstractValidator<ChangeTaskStatusCommand>
{
    public ChangeTaskStatusValidator()
    {
        RuleFor(x => x.TaskId).NotEmpty().WithMessage("Task Id is required");
        RuleFor(x => x.NewStatus).IsInEnum().WithMessage("Invalid Status");
    }
}