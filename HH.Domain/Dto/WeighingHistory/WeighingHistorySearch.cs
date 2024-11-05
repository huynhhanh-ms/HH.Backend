using HH.Domain.Common;

namespace HH.Domain.Dto.WeighingHistory
{
    public class WeighingHistorySearch : SearchBaseRequest
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }

    public class WeighingHistorySearchValidator : AbstractValidator<WeighingHistorySearch>
    {
        public WeighingHistorySearchValidator()
        {
            RuleFor(x => x.StartDate)
                          .NotEmpty().WithMessage("Start date is required.");

            RuleFor(x => x.EndDate)
                .NotEmpty().WithMessage("End date is required.")
                .GreaterThanOrEqualTo(x => x.StartDate)
                .WithMessage("End date must be greater than or equal to start date.");
        }
    }

}
