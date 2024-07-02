using PI.Domain.Common;
using PI.Domain.Common.PagedLists;
using PI.Domain.Dto.ProductAttribute;

namespace PI.Application.Service.Attribute
{
    public interface IAttributeService
    {
        Task<ApiResponse<string>> CreateAttribute(CreateProductAttributeRequest request);
        Task<ApiResponse<IEnumerable<ProductAttributeResponse>>> GetAttributes();
    }
}