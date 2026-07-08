using FluentValidation;
using MediatR;
//This file creates a pipeline for our project mediator
namespace TaskManager.Application.Common.Behaviors
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;
        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }
        public async Task<TResponse> Handle(
          TRequest request,
          RequestHandlerDelegate<TResponse> next,
          CancellationToken cancellationToken
        )
        {
            if (_validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);
                var validationResults = await Task.WhenAll(
                    _validators.Select(x => x.ValidateAsync(context, cancellationToken)));
                var failures = validationResults.SelectMany(e => e.Errors)
                                                .Where(x => x != null)
                                                .ToList();
                if (failures.Any())
                    throw new Exceptions.ValidationException(failures);
            }
            var response = await next();
            return response;
        }
    }
}