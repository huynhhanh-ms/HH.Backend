using System.ComponentModel.DataAnnotations;

namespace PI.Domain.Dto.StockCheck
{
    public class SubmitStockCheckDetailRequest
    {
        [Required]
        public int ProductUnitId { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int EstimatedQuantity { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int ActualQuantity { get; set; }

        public string? Note { get; set; }
    }

    public class CreateStockCheckDetailRequestValidator : AbstractValidator<SubmitStockCheckDetailRequest>
    {
        public CreateStockCheckDetailRequestValidator()
        {
            RuleFor(x => x.ProductUnitId).NotEmpty();
            RuleFor(x => x.EstimatedQuantity).NotEmpty();
            RuleFor(x => x.ActualQuantity).NotEmpty();
        }
    }
}
