
using System.ComponentModel.DataAnnotations;

namespace PI.Domain.Dto.ImportRequest.MergeImportRequest
{
    public class MergeImportReqRequest
    {
        [Required]
        [MinLength(2)]
        public IEnumerable<int> ImportRequest { get; set; } = new List<int>();
    }

    public class MergeImportReqRequestValidator : AbstractValidator<MergeImportReqRequest>
    {
        public MergeImportReqRequestValidator()
        {
            RuleFor(x => x.ImportRequest)
                .NotEmpty()
                .Must(x => x.Count() > 1);
        }
    }
}
