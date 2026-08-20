using FluentValidation;

namespace TaskManager.Application.Features.Tasks.Queries.GetTaskById;

public class GetTaskByIdValidation : AbstractValidator<GetTaskByIdQuery>
{
    public GetTaskByIdValidation()
    {
        RuleFor(x => x.TaskId).NotEmpty().WithMessage("Please Select Your task!");
    }
}