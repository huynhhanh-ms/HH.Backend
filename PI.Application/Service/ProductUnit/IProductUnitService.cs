using PI.Domain.Common.PagedLists;
using PI.Domain.Dto.Product;

namespace PI.Application.Service
{
    public interface IProductUnitService
    {
        Task<PagingApiResponse<ProductUnitResponse>> SearchProductUnit(string keySearch,
            PagingQuery pagingQuery, string orderBy);

        Task<ApiResponse<ProductUnitResponse>> GetProductUnitBySkuCode(string skuCode);

        Task<PagingApiResponse<ProductLotResponse>> GetProductLot(string skuCode, PagingQuery pagingQuery,
            string orderBy, LotStatus? lotStatus);

        Task<PagingApiResponse<ProductHistoryResponse>> GetProductHistory(string skuCode,
            SearchProductHistory searchReq);

        Task<ApiResponse<string>> DeleteProductUnit(int productUnitId);

        Task<ApiResponse<IEnumerable<ProductStatisticResponse>>> StatisticProductUnit(string skuCode,
            SearchProductStatisticReq req);
    }
}