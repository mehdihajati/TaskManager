using FluentValidation;

namespace TaskManager.Application.Features.Tasks.Commands.ChangeTaskPriority;

public class ChangeTaskPriorityValidation : AbstractValidator<ChangeTaskPriorityCommand>
{
    public ChangeTaskPriorityValidation()
    {
        RuleFor(x => x.TaskId).NotEmpty().WithMessage("you should select a task");
        RuleFor(x => x.NewPriority).IsInEnum().WithMessage("Select a valid priority");

    }
}
