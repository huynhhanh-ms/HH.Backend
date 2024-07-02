using PI.Domain.Common;
using PI.Domain.Common.PagedLists;
using PI.Domain.Enums;

namespace PI.Domain.Dto.Product
{
    public class SearchProductLot
    {
        public PagingQuery PagingQuery { get; set; } = new PagingQuery();
        public string? OrderBy { get; set; } = null;
        public LotStatus? LotStatus { get; set; }
        
    }
}