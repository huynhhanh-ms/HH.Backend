using PI.Domain.Dto.ExportRequest;
using PI.Domain.Dto.ProductStock;
using PI.Domain.Dto.ProductStock.ProductStockEstimate;
using PI.Domain.Models;
using PI.Domain.Repositories;
using PI.Domain.Repositories.Common;

namespace PI.Application.Service
{
    public class ProductStockService : BaseService, IProductStockService
    {
        public ProductStockService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<ApiResponse<IEnumerable<ProductStockResponse>>> GetProductStockReponseByExportRequestId(int exportRequestId)
        {
            try
            {
                var productStocks = await _unitOfWork.Resolve<IProductStockRepository>()
                                    .GetProductStockReponseByExportRequestId(exportRequestId);
                return Success(productStocks);
            }
            catch (Exception ex)
            {
                return Failed<IEnumerable<ProductStockResponse>>(ex.Message);
            }
        }

        public async Task<ApiResponse<IEnumerable<CheckStock4ExportReqResponse>>> CheckStock4ExportRequest(int exportRequestId)
        {
            try
            {
                var isValidStockList = await _unitOfWork.Resolve<IProductStockRepository>()
                                    .CheckStock4ExportReq(exportRequestId);

                return Success(isValidStockList);
            }
            catch (Exception ex)
            {
                return Failed<IEnumerable<CheckStock4ExportReqResponse>>(ex.Message);
            }
        }

        public async Task<PagingApiResponse<EstimateProductStockResponse>> SearchEstProductStock(SearchProductStockEstimateRequest request)
        {
            try
            {
                var result = await _unitOfWork.Resolve<IProductStockRepository>()
                    .SearchEstProductStock(request);

                return Success(result);
            }
            catch (Exception ex)
            {
                return PagingFailed<EstimateProductStockResponse>(ex.GetExceptionMessage());
            }
        }

        public async Task<PagingApiResponse<EstimateProductStockWithImportRequestResponse>> SearchEstProductStockWithImportRequest(SearchProductStockEstimateRequest request)
        {
            try
            {
                var result = await _unitOfWork.Resolve<IProductStockRepository>()
                    .SearchEstProductStockWithImportRequest(request);

                return Success(result);
            }
            catch (Exception ex)
            {
                return PagingFailed<EstimateProductStockWithImportRequestResponse>(ex.GetExceptionMessage());
            }
        }

        public async Task<PagingApiResponse<ProductStockResponse>> SearchProductStock(SearchProductStockRequest request)
        {
            try
            {
                var productStocks = await _unitOfWork.Resolve<IProductStockRepository>()
                    .SearchProductStock(request);

                return Success(productStocks);
            }
            catch (Exception ex)
            {
                return PagingFailed<ProductStockResponse>(ex.GetExceptionMessage());
            }
        }

        public async Task<IList<ProductStock>> GetProductStock(params int[] productUnitIds)
        { 
            return await _unitOfWork.Resolve<IProductStockRepository>()
                                    .GetProductStocks(productUnitIds);
        }
    }
}
