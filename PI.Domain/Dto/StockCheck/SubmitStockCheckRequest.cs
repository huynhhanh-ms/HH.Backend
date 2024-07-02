using System.ComponentModel.DataAnnotations;

namespace PI.Domain.Dto.StockCheck
{
    public class SubmitStockCheckRequest
    {
        [StringLength(255)]
        public string StaffSignature { get; set; } = null!;

        public string? Comment { get; set; }

        [StringLength(255)]
        public string? DocumentLink { get; set; }

        [Required]
        public IEnumerable<SubmitStockCheckDetailRequest> CheckedProducts { get; set; } = new List<SubmitStockCheckDetailRequest>();
    }
}
