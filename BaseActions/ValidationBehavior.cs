using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseActions
{
    public class ValidationBehavior<TRequest, TResponse>
        : IPipelineBehavior<TRequest, TResponse>
            where TRequest : IRequest<TResponse>
    {

        private readonly IEnumerable<IValidator<TRequest>> Validators = null!;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators) =>
            Validators = validators;

        public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var context = new ValidationContext<TRequest>(request);
            
            var failures = Validators
                .Select(x => x.Validate(context))
                .SelectMany(result => result.Errors)
                .Where(failure => failure != null)
                .ToArray();

            if(failures.Length != 0)
            {
                throw new FluentValidation.ValidationException(failures);
            }

            return next();
        }
    }
}
