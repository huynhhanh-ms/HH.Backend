using Microsoft.EntityFrameworkCore;
using PI.Domain.Common.PagedLists;
using PI.Domain.Models;
using PI.Persitence.Repository.Common;

namespace PI.Persitence.Repository
{
    public class ShipmentDetailRepository : GenericRepository<ShipmentDetail>
    {
        public ShipmentDetailRepository(DbContext context) : base(context)
        {
        }

        public override Task<IPagedList<ShipmentDetail>> SearchAsync(string keySearch, PagingQuery pagingQuery, string orderBy)
        {
            throw new NotImplementedException();
        }

        public override Task<IPagedList<TResult>> SearchAsync<TResult>(string keySearch, PagingQuery pagingQuery, string orderBy)
        {
            throw new NotImplementedException();
        }
    }
}