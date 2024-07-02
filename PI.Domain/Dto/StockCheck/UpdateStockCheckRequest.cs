using static PI.Domain.Enums.StockCheckEnum;

namespace PI.Domain.Dto.StockCheck
{
    public class UpdateStockCheckRequest
    {
        public int StockCheckId { get; set; }
        public string Title { get; set; } = null!;
        public string? Note { get; set; }

        public string? Description { get; set; }
        public string? DocumentLink { get; set; }
        public int StaffId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime DueDate { get; set; }
        public StockCheckPriority Priority { get; set; }
    }

    public class UpdateStockCheckRequestValidator : AbstractValidator<UpdateStockCheckRequest>
    {
        public UpdateStockCheckRequestValidator()
        {
            RuleFor(x => x.DueDate).Must(x => x > DateTime.Now);
            RuleFor(x => x.StartDate).Must(x => x > DateTime.Now);
        }
    }
}
