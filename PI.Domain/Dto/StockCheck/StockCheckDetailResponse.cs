
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Text.Json.Serialization;
using PI.Domain.Common.PagedLists;


namespace PI.Domain.Dto.StockCheck
{
    public class StockCheckDetailResponse
    {
        public int StockCheckId { get; set; }

        public string? Title { get; set; }

        public string? StockCheckType { get; set; }

        public string? Priority { get; set; }

        public string? Status { get; set; }

        public string? Note { get; set; }

        public string? Description { get; set; }

        public string? DocumentLink { get; set; }

        public int StaffId { get; set; }

        public string StaffName { get; set; } // Staf

        public int? StockkeeperId { get; set; }

        public string? StockkeeperName { get; set; } = null; // Stockkeeper

        public bool IsUsedForBalancing { get; set; }

        public DateTime DueDate { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime CreatedAt { get; set; }

        public int CreatedById { get; set; }

        public string CreatedByName { get; set; } = null!;

        public string? StaffSignature { get; set; }

        public string? StockkeeperSignature { get; set; }

        public string? ManagerSignature { get; set; }

        public PagingResponse<StockCheckDetailItem>? StockCheckDetails { get; set; } = new PagingResponse<StockCheckDetailItem>();

        [JsonIgnore]
        public string? Log { get; set; }
        public IEnumerable<StockCheckLogDto> StockCheckLogs
        {
            get
            {
                if (string.IsNullOrEmpty(Log))
                {
                    return new List<StockCheckLogDto>();
                }

                return JsonSerializer.Deserialize<IEnumerable<StockCheckLogDto>>(Log ?? "");
            }
        }

    }

    public class StockCheckDetailItem
    {
        public int StockCheckDetailId { get; set; }

        public int StockCheckId { get; set; }

        public int ProductUnitId { get; set; }

        public string ProductSkuCode { get; set; } // ProductUnit

        public string ProductName { get; set; } // ProductUnit

        public int EstimatedQuantity { get; set; }

        public int ActualQuantity { get; set; }

        public string Note { get; set; } = null!;

        public string Status { get; set; } = null!;
    }
}
