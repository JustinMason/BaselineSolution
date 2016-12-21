using Core.Validation;

namespace Infrastructure.Validation
{
    public interface IRequestValidator
    {
        ValidationSummary Validate(object request);
        ValidationSummary Validate<TBody>(IHaveABody<TBody> request);
    }
}