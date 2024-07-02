using PI.Domain.Models;
using PI.Domain.Repositories;
using System.Linq.Dynamic.Core;

namespace PI.Persitence.Repository
{
    public class ProductAttributeMapRepository : GenericRepository<ProductAttributeMapping>,
        IProductAttributeMapRepository
    {
        public ProductAttributeMapRepository(DbContext context) : base(context)
        {
        }

        public override Task<IPagedList<ProductAttributeMapping>> SearchAsync(string keySearch, PagingQuery pagingQuery,
            string orderBy)
        {
            throw new NotImplementedException();
        }

        public override Task<IPagedList<TResult>> SearchAsync<TResult>(string keySearch, PagingQuery pagingQuery,
            string orderBy)
        {
            throw new NotImplementedException();
        }


        public async Task<IEnumerable<ProductAttributeMapping>> FindByProductId(int productId)
        {
            return await _dbSet.AsNoTracking()
                .Where(p => p.ProductId == productId)
                .Include(p => p.ProductAttribute)
                .ToListAsync();
        }
    }
}