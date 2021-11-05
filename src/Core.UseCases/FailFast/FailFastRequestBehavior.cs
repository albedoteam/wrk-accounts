namespace Core.UseCases.FailFast
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Abstractions;
    using Enums;
    using FluentValidation;
    using FluentValidation.Results;
    using MediatR;
    using Microsoft.Extensions.Logging;

    public class FailFastRequestBehavior<TRequest, TResult> : IPipelineBehavior<TRequest, TResult>
        where TRequest : IRequest<TResult>
        where TResult : class, IResult, new()
    {
        private readonly ILogger<FailFastRequestBehavior<TRequest, TResult>> _logger;
        private readonly IEnumerable<IValidator> _validators;

        public FailFastRequestBehavior(
            IEnumerable<IValidator<TRequest>> validators,
            ILogger<FailFastRequestBehavior<TRequest, TResult>> logger)
        {
            _validators = validators;
            _logger = logger;
        }

        public Task<TResult> Handle(
            TRequest request,
            CancellationToken cancellationToken,
            RequestHandlerDelegate<TResult> next)
        {
            var context = new ValidationContext<TRequest>(request);

            var failures = _validators
                .Select(v => v.Validate(context))
                .SelectMany(result => result.Errors)
                .Where(f => f != null)
                .ToList();

            return failures.Any()
                ? ValidationErrors(failures)
                : TryNext(next);
        }

        private Task<TResult> ValidationErrors(List<ValidationFailure> failures)
        {
            var errorResponse = new TResult();
            errorResponse.SetErrorType(ErrorType.BadRequest);

            foreach (var failure in failures)
                errorResponse.AddError(failure.ErrorCode, failure.ErrorMessage);

            return Task.FromResult(errorResponse);
        }

        private Task<TResult> TryNext(RequestHandlerDelegate<TResult> next)
        {
            try
            {
                var innerResult = next().Result;
                return Task.FromResult(innerResult);
            }
            catch (Exception e)
            {
                var errorResponse = new TResult();

                var guid = Guid.NewGuid();
                _logger.LogError(e, "{ErrorId}", guid);

                errorResponse.SetErrorType(ErrorType.InternalServerError);
                errorResponse.AddError(guid.ToString(), $"Pipeline execution error, please check the logs {guid}");

                return Task.FromResult(errorResponse);
            }
        }
    }
}