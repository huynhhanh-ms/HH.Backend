using Microsoft.EntityFrameworkCore;
using PI.Domain.Common.PagedLists;
using PI.Domain.Models;
using PI.Persitence.Repository.Common;

namespace PI.Persitence.Repository
{
    public class ProductAttributeRepository : GenericRepository<ProductAttribute>
    {
        public ProductAttributeRepository(DbContext context) : base(context)
        {
        }

        public override Task<IPagedList<ProductAttribute>> SearchAsync(string keySearch, PagingQuery pagingQuery, string orderBy)
        {
            throw new NotImplementedException();
        }

        public override Task<IPagedList<TResult>> SearchAsync<TResult>(string keySearch, PagingQuery pagingQuery, string orderBy)
        {
            throw new NotImplementedException();
        }
    }
}