using PI.Domain.Common;
using PI.Domain.Common.PagedLists;
using PI.Domain.Dto.Product;
using PI.Domain.Models;

namespace PI.Application.Service
{
    public interface IWeighingHistoryService
    {
        Task<ApiResponse<string>> Create(WeighingHistory request);
        Task<PagingApiResponse<ProductResponse>> Search(WeighingHistory request);

        Task<ApiResponse<string>> UpdateProduct(UpdateProductRequest request, int productId);
        Task<ApiResponse<string>> UpdateMedicine(UpdateMedicineRequest request, int productId);
        Task<ApiResponse<ProductResponse>> GetProductDetail(int productId);

        Task<ApiResponse<bool>> DeleteProduct(int[] ids);
    }
}