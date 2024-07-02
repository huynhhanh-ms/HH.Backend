using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PI.Domain.Dto.ExportRequest
{
    public class CreateExportRequestReq
    {
        public IEnumerable<CreateExportRequestItem> ExportRequestDetails { get; set; } = default!;
    }

    public class CreateExportRequestItem
    {
        public int ProductUnitId { get; set; }
        public int Quantity { get; set; }
    }


    public class CreateExportRequestReqValidation : AbstractValidator<CreateExportRequestReq>
    {
        public CreateExportRequestReqValidation()
        {
            RuleFor(x => x.ExportRequestDetails).NotEmpty();
            RuleForEach(x => x.ExportRequestDetails).SetValidator(new CreateExportRequestItemValidation());
        }
    }

    public class CreateExportRequestItemValidation : AbstractValidator<CreateExportRequestItem>
    {
        public CreateExportRequestItemValidation()
        {
            RuleFor(x => x.ProductUnitId).NotEmpty();
            RuleFor(x => x.Quantity).NotEmpty().Must(x => x > 0);
        }
    }
}
