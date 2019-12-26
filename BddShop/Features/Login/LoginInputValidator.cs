using FluentValidation;

namespace BddShop.Features.Login
{
    public class LoginInputValidator : AbstractValidator<LoginRequest>
    {
        public LoginInputValidator()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required.");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required.");
        }
    }
}
