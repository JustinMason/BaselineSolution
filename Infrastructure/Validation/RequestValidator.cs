using System.Text.RegularExpressions;
using Core.Validation;
using FluentValidation;
using FluentValidation.Results;
using StructureMap;

namespace Infrastructure.Validation
{
    public class RequestValidator : IRequestValidator
    {
        private readonly IContainer _container;

        public RequestValidator(IContainer container)
        {
            _container = container;
        }

        public ValidationSummary Validate(object request)
        {
            return GenerateValidationSummary(request);
        }

        public ValidationSummary Validate<TBody>(IHaveABody<TBody> request)
        {
            return GenerateValidationSummary(request, true);
        }

        private ValidationSummary GenerateValidationSummary<TRequest>(TRequest request, bool cullBodyPropertyNameSegment = false)
        {
            var summary = new ValidationSummary();
            var validatorType = typeof(IValidator<>).MakeGenericType(request.GetType());

            var validators = _container.GetAllInstances(validatorType);

            foreach (var validator in validators)
            {
                var result = (ValidationResult)validatorType.GetMethod(nameof(IValidator<object>.Validate)).Invoke(validator, new object[] {request});

                foreach (var error in result.Errors)
                {
                    summary.Errors.Add(new ValidationSummary.ValidationError {PropertyName = error.PropertyName, Message = error.ErrorMessage});
                }
            }

            if (cullBodyPropertyNameSegment)
                RemoveBodyPropertyName(summary);

            return summary;
        }

        private void RemoveBodyPropertyName(ValidationSummary summary)
        {
            var rootModelPropertyName = nameof(IHaveABody<object>.Body);

            foreach (var validationError in summary.Errors)
            {
                validationError.PropertyName = new Regex(string.Format("^{0}\\.", rootModelPropertyName)).Replace(validationError.PropertyName, "", 1);
                validationError.Message = new Regex(string.Format("^'{0}\\. ", rootModelPropertyName)).Replace(validationError.Message, "'", 1); // dealing with the auto-generated messages from fluent validation that prepend with "Body. SomeProp"
            }
        }
    }
}