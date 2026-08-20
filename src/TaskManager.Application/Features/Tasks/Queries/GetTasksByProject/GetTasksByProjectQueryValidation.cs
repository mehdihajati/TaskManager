
using FluentValidation;

namespace TaskManager.Application.Features.Tasks.Queries.GetTasksByProject;

public class GetTasksByProjectQueryValidation : AbstractValidator<GetTasksByProjectQuery>
{
    public GetTasksByProjectQueryValidation()
    {
        RuleFor(x => x.ProjectId).NotEmpty().WithMessage("ProjectId is required");
    }
}