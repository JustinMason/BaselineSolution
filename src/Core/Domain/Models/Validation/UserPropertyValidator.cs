using FluentValidation;

namespace Core.Domain.Models.Validation
{
    public class UserPropertyValidator: AbstractValidator<User>
    {
        public UserPropertyValidator()
        {
            RuleFor(x => x.FirstName).NotNull().WithMessage("First name is required.");
            RuleFor(x => x.LastName).NotNull().WithMessage("Last name is required.");

            RuleFor(x => x.EmailAddress)
                .NotNull().WithMessage("Email address is required.")
                .EmailAddress().WithMessage("Email address is not valid.");

            RuleFor(x => x.Phone).NotNull().WithMessage("Phone number is required.");
        }
    }
}
