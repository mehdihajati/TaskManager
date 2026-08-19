using MediatR;

namespace TaskManager.Application.Features.Tasks.Commands.AssignTask;

public class AssignTaskHandler : IRequestHandler<AssignTaskCommand, Unit>
{
    public Task<Unit> Handle(AssignTaskCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}