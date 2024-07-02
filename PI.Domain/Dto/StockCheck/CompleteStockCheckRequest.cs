using System.ComponentModel.DataAnnotations;

namespace PI.Domain.Dto.StockCheck
{
    public class CompleteStockCheckRequest
    {
        [Required]
        public string? DocumentLink { get; set; }

        public string? Comment { get; set; }

        [Required]
        [StringLength(255)]
        public string ManagersSignature { get; set; } = null!;
    }
}
