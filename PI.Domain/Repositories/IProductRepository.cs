using PI.Domain.Common.PagedLists;
using PI.Domain.Dto.Product;
using PI.Domain.Models;
using PI.Domain.Repositories.Common;

namespace PI.Domain.Repositories
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<IPagedList<ProductResponse>> SearchAsync(SearchProductRequest request);

        Task<ProductResponse> GetProductDetail(int id);
    }
}