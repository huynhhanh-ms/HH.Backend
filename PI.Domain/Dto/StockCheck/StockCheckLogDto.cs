using System.Text.Json.Serialization;

namespace PI.Domain.Dto.StockCheck
{
    public class StockCheckLogDto
    {
        public string Title { get; set; }
        public string Datetime { get; set; }
        public string? Content { get; set; } = null;
        public string StockCheckStatus { get; set; }
    }
}
