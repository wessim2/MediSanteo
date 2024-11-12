using FluentValidation;

namespace MediSanteo.Application.Users.RegisterUser
{
    internal sealed class RegisterUserValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserValidator() { 
            RuleFor( u => u.FirstName ).NotEmpty();
            RuleFor( u => u.LastName ).NotEmpty();
            RuleFor( u => u.Email ).EmailAddress();
            RuleFor( u =>  u.Password ).NotEmpty().MinimumLength(5);
            RuleFor( u => u.Role ).NotEmpty();
        }
    }
}
