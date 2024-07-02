using Microsoft.EntityFrameworkCore;
using PI.Domain.Common.PagedLists;
using PI.Domain.Models;
using PI.Domain.Repositories;
using PI.Persitence.Repository.Common;
using PI.Persitence.Repository.Helper;

namespace PI.Persitence.Repository
{
    public class ExportRequestDetailRepository : GenericRepository<ExportRequestDetail>, IExportRequestDetailRepository
    {
        public ExportRequestDetailRepository(DbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<ExportRequestDetail>> GetByExportRequestId(int exportRequestId)
        {
            return await _dbSet
                         .AsNoTracking()
                         .WhereWithExist(x => x.ExportRequestId == exportRequestId)
                         .Include(x => x.ProductUnit)
                             .ThenInclude(x => x.Unit)
                         .ToListAsync();
        }

        public override Task<IPagedList<ExportRequestDetail>> SearchAsync(string keySearch, PagingQuery pagingQuery, string orderBy)
        {
            throw new NotImplementedException();
        }

        public override Task<IPagedList<TResult>> SearchAsync<TResult>(string keySearch, PagingQuery pagingQuery, string orderBy)
        {
            throw new NotImplementedException();
        }
    }
}
