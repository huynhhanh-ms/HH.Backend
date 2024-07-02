using PI.Domain.Dto.StockCheck;

namespace PI.Application.Service.StockBalance
{
    public interface IStockBalanceService
    {
        Task<ApiResponse<bool>> BalanceStockByStockCheck(int stockCheckId);
        Task<PagingApiResponse<SearchStockCheckResponse>> SearchStockBalance(SearchStockCheckRequest request);
    }
}