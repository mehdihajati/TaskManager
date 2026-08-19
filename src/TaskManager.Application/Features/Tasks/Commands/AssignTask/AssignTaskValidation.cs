using FluentValidation;

namespace TaskManager.Application.Features.Tasks.Commands.AssignTask;

public class AssignTaskValidation : AbstractValidator<AssignTaskCommand>
{
    public AssignTaskValidation()
    {
        RuleFor(x => x.TaskId).NotEmpty().WithMessage("You should select a task");
        RuleFor(x => x.AssigneeId).NotEmpty().WithMessage("You should select an assignee");
    }
}