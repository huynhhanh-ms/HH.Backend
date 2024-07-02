using PI.Domain.Dto.StockCheck;

namespace PI.Application.Service.ProductStockCheck
{
    public interface IStockCheckService
    {

        Task<PagingApiResponse<SearchStockCheckResponse>> SearchStockCheck(SearchStockCheckRequest request);
        Task<ApiResponse<StockCheckDetailResponse>> SearchStockCheckDetail(int stockCheckId, SearchStockCheckDetailRequest request);
        Task<ApiResponse<int>> CreateStockCheck(CreateStockCheckRequest request);
        Task<ApiResponse<bool>> UpdateStockCheck(UpdateStockCheckRequest request);
        Task<ApiResponse<bool>> AssignStockCheck(int stockCheckId, int staffId);
        Task<ApiResponse<bool>> AcceptStockCheckAssignmennt(int stockCheckId);
        Task<ApiResponse<bool>> DeclineStockCheckAssignmennt(int stockCheckId, DeclineStockCheckAssignmentRequest request);
        Task<ApiResponse<bool>> SubmitStockCheck(int stockCheckId, SubmitStockCheckRequest request, bool isDraft);
        Task<ApiResponse<bool>> ConfirmStockCheck(int stockCheckId, ConfirmStockCheckRequest request);
        Task<ApiResponse<bool>> RejectStockCheck(int stockCheckId, RejectStockCheckRequest request);
        Task<ApiResponse<bool>> CompleteStockCheck(int stockCheckId, CompleteStockCheckRequest request);
        Task<ApiResponse<bool>> DeleteStockCheck(int stockCheckId);

    }
}
