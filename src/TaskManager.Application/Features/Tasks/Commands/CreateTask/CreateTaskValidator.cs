using FluentValidation;

namespace TaskManager.Application.Features.Tasks.Commands.CreateTask;

public class CreateTaskValidator : AbstractValidator<CreateTaskCommand>
{
    public CreateTaskValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(100).WithMessage("Title cannot exceed 100 characters.");
        RuleFor(x => x.Description)
            .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.");
        RuleFor(x => x.DueDate)
            .GreaterThan(DateTimeOffset.UtcNow).WithMessage("Due date must be in the future.")
            .When(x => x.DueDate.HasValue);
        RuleFor(x => x.Priority).IsInEnum().WithMessage("Invalid priority value.");
        RuleFor(x => x.ProjectId).NotEmpty().WithMessage("ProjectId is required.");
    }

}