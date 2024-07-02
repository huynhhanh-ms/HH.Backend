using PI.Domain.Common;
using PI.Domain.Dto.ProductStock;
using PI.Domain.Dto.ProductStock.ProductStockEstimate;
using PI.Domain.Models;

namespace PI.Application.Service
{
    public interface IProductStockService : IDisposable
    {
        Task<ApiResponse<IEnumerable<ProductStockResponse>>> GetProductStockReponseByExportRequestId(int exportRequestId);
        Task<ApiResponse<IEnumerable<CheckStock4ExportReqResponse>>> CheckStock4ExportRequest(int exportRequestId);
        Task<PagingApiResponse<EstimateProductStockResponse>> SearchEstProductStock(SearchProductStockEstimateRequest request);
        Task<PagingApiResponse<EstimateProductStockWithImportRequestResponse>> SearchEstProductStockWithImportRequest(SearchProductStockEstimateRequest request);
        Task<PagingApiResponse<ProductStockResponse>> SearchProductStock(SearchProductStockRequest request);
        Task<IList<ProductStock>> GetProductStock(params int[] productUnitIds);
    }
}
