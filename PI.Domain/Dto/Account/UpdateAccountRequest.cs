using FluentValidation;

namespace PI.Domain.Dto.Account
{
    public class UpdateAccountRequest
    {
        public string Fullname { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string? Email { get; set; }
    }
    
    public class UpdateAccountRequestValidator : AbstractValidator<UpdateAccountRequest>
    {
        public UpdateAccountRequestValidator()
        {
            RuleFor( x => x.Fullname).NotEmpty().WithMessage("Fullname is required");
            RuleFor( x => x.Phone).NotEmpty().WithMessage("Phone is required");
            RuleFor( x => x.Email).EmailAddress().When(x => !string.IsNullOrEmpty(x.Email)).WithMessage("Email is invalid");
        }
    }
}