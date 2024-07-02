using PI.Domain.Common;
using static PI.Domain.Enums.StockCheckEnum;

namespace PI.Domain.Dto.StockCheck
{
    public class SearchStockCheckDetailRequest : SearchBaseRequest
    {
        public StockCheckDetailStatus? StockCheckDetailStatus { get; set; } = null; 
        public StockDiff? StockDiff { get; set; } = null;
    }
}
