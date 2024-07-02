using PI.Domain.Dto;
using PI.Domain.Models;
using PI.Domain.Repositories.Common;

namespace PI.Domain.Repositories
{
    public interface IProductAttributeMapRepository : IGenericRepository<ProductAttributeMapping>
    {
        Task<IEnumerable<ProductAttributeMapping>> FindByProductId(int productId);
    }
}