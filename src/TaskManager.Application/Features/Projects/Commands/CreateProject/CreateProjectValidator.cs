using FluentValidation;

namespace TaskManager.Application.Features.Projects.Commands.CreateProject;

public class CreateProjectValidator : AbstractValidator<CreateProjectCommand>
{
    public CreateProjectValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Project name could not be empty")
                          .MaximumLength(70).WithMessage("Project name could not be longer than 70 charecters");
        RuleFor(x => x.Deadline).NotEmpty().WithMessage("Project must have deadline")
                              .GreaterThan(DateTimeOffset.UtcNow).WithMessage("deadline could not be earlier than today");
        RuleFor(x => x.Description).MaximumLength(1000).WithMessage("your project description should not be longer than 1000 charecters");
    }
}