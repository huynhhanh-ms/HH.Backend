using FluentValidation;

namespace PI.Domain.Dto.Account
{
    public class RegisterAccountRequest
    {
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Fullname { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string? Email { get; set; }
    }

    public class RegisterRequestValidator : AbstractValidator<RegisterAccountRequest>
    {
        public RegisterRequestValidator()
        {
            RuleFor( x => x.Username).NotEmpty().WithMessage("Username is required");
            RuleFor( x => x.Password).NotEmpty().WithMessage("Password is required");
            RuleFor( x => x.Fullname).NotEmpty().WithMessage("Fullname is required");
            RuleFor( x => x.Phone).NotEmpty().WithMessage("Phone is required");
            RuleFor( x => x.Email).EmailAddress().When(x => !string.IsNullOrEmpty(x.Email)).WithMessage("Email is invalid");
        }
    }
    
}