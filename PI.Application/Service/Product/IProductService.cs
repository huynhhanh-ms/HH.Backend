using PI.Domain.Common;
using PI.Domain.Common.PagedLists;
using PI.Domain.Dto.Product;

namespace PI.Application.Service
{
    public interface IProductService
    {
        Task<ApiResponse<string>> CreateProduct(CreateProductRequest request);
        Task<ApiResponse<string>> CreateMedicine(CreateProductMedicineRequest request);
        Task<PagingApiResponse<ProductResponse>> SearchProduct(SearchProductRequest request);

        Task<ApiResponse<string>> UpdateProduct(UpdateProductRequest request, int productId);
        Task<ApiResponse<string>> UpdateMedicine(UpdateMedicineRequest request, int productId);
        Task<ApiResponse<ProductResponse>> GetProductDetail(int productId);

        Task<ApiResponse<bool>> DeleteProduct(int[] ids);
    }
}