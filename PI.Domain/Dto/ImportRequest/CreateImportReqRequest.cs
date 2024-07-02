
using FluentValidation;

namespace PI.Domain.Dto.ImportRequest
{
    public class CreateImportReqRequest
    {
        public IEnumerable<CreateImportRequestItem> ImportRequestDetails { get; set; } = default!;
    }

    public class CreateImportRequestItem
    {
        public int ProductUnitId { get; set; }
        public int Quantity { get; set; }
    }

    public class CreateImportRequestReqValidation : AbstractValidator<CreateImportReqRequest>
    {
        public CreateImportRequestReqValidation()
        {
            RuleFor(x => x.ImportRequestDetails).NotEmpty();
            RuleForEach(x => x.ImportRequestDetails).SetValidator(new CreateImportRequestItemValidation());
        }
    }

    public class CreateImportRequestItemValidation : AbstractValidator<CreateImportRequestItem>
    {
        public CreateImportRequestItemValidation()
        {
            RuleFor(x => x.ProductUnitId).NotEmpty();
            RuleFor(x => x.Quantity).NotEmpty().Must(x => x > 0);
        }
    }
}
