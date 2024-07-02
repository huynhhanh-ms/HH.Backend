using PI.Domain.Common.PagedLists;
using PI.Domain.Dto.StockCheck;
using PI.Domain.Models;
using PI.Domain.Repositories.Common;

namespace PI.Domain.Repositories
{
    public interface IStockCheckRepository : IGenericRepository<StockCheck>
    {
        Task<IPagedList<SearchStockCheckResponse>> SearchAsync(SearchStockCheckRequest request);
        Task<IPagedList<SearchStockCheckResponse>> SearchStockBalanceAsync(SearchStockCheckRequest request);
        Task<StockCheckDetailResponse> SearchStockCheckDetail(int stockCheckId, SearchStockCheckDetailRequest request); 
    }
}
