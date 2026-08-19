using FluentValidation;
namespace TaskManager.Application.Features.Tasks.Commands.UnassignTask;

public class UnassignTaskValidator : AbstractValidator<UnassignTaskCommand>
{
    public UnassignTaskValidator()
    {
        RuleFor(x => x.TaskId).NotEmpty().WithMessage("TaskId is required");
    }
}