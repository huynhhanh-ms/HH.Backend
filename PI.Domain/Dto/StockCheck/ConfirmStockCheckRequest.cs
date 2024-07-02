using System.ComponentModel.DataAnnotations;

namespace PI.Domain.Dto.StockCheck
{
    public class ConfirmStockCheckRequest
    {
        [Required]
        public string? DocumentLink { get; set; }

        public string? Comment { get; set; }

        [Required]
        [StringLength(255)]
        public string StockkeeperSignature { get; set; } = null!;

        public IEnumerable<int> ConfirmedStockCheckDetailIds { get; set; } = new List<int>();
    }
}
