using FluentValidation;

namespace BddShop.Features.Registration
{
    internal sealed class RegisterUserInputValidator : AbstractValidator<RegisterUser>
    {
        public RegisterUserInputValidator()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required");
        }
    }
}
