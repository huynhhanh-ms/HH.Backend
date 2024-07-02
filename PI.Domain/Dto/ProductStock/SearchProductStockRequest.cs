using PI.Domain.Common;

namespace PI.Domain.Dto.ProductStock
{
    public class SearchProductStockRequest : SearchBaseRequest
    {
        public int[]? ProductUnitIds { get; set; }
    }
}
