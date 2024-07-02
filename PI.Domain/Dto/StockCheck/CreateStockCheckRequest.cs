using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using PI.Domain.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using static PI.Domain.Enums.StockCheckEnum;

namespace PI.Domain.Dto.StockCheck
{
    public class CreateStockCheckRequest
    {
        [Required]
        public string Title { get; set; } = null!;

        public string? Note { get; set; } = null!;

        public string? Description { get; set; } = null!;

        [StringLength(255)]
        public string? DocumentLink { get; set; }

        [Required]
        public DateTime DueDate { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        public int? StaffId { get; set; } = null;

        [Required]
        public StockCheckType StockCheckType { get; set; }

        public StockCheckPriority? Priority { get; set; } = StockCheckPriority.Medium;

        public int[]? ProductUnitIds { get; set; } = new int[0];
    }

    public class CreateStockCheckRequestValidator : AbstractValidator<CreateStockCheckRequest>
    {
        public CreateStockCheckRequestValidator()
        {
            RuleFor(x => x.DueDate).Must(x => x.Date >= DateTime.Now.Date);
            RuleFor(x => x.StartDate).Must(x => x.Date >= DateTime.Now.Date);
        }
    }

   
}
