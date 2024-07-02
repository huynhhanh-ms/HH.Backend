
using PI.Domain.Dto.ExportRequest;
using PI.Domain.Models;
using PI.Domain.Repositories;

namespace PI.Persitence.Repository
{
    public class ExportRequestRepository : GenericRepository<ExportRequest>, IExportRequestRepository
    {
        public ExportRequestRepository(Microsoft.EntityFrameworkCore.DbContext context) : base(context)
        {
        }

        public override Task<IPagedList<ExportRequest>> SearchAsync(string keySearch, PagingQuery pagingQuery, string orderBy)
        {
            throw new NotImplementedException();
        }

        public override async Task<IPagedList<TResult>> SearchAsync<TResult>(string keySearch, PagingQuery pagingQuery, string orderBy)
        {
            return await _dbSet.AsNoTracking()
               .WhereWithExist(a => string.IsNullOrEmpty(keySearch) || a.ExportRequestId.ToString() == keySearch)
               .WithOrderByString(orderBy)
               .ToPagedListAsync<ExportRequest, TResult>(pagingQuery);
        }

        public async Task<IPagedList<ExportRequestResponse>> SearchAsync(SearchExportReqRequest searchReq)
        {
            return await _dbSet.AsNoTracking()
                .Include(e => e.ExportRequestDetails)
                    .ThenInclude(e => e.ProductUnit)
                    .ThenInclude(e => e.Product)
                .Include(e => e.Shipments)
                .WhereWithExist(a => (string.IsNullOrEmpty(searchReq.KeySearch)
                                    || a.ExportRequestId.ToString() == searchReq.KeySearch)
                                && (searchReq.ExportStatus == null
                                    || a.ExportStatus.ToLower() == searchReq.ExportStatus.ToString().ToLower()))
                .WithOrderByString(searchReq.OrderBy)
                .ToPagedListAsync<ExportRequest, ExportRequestResponse>(searchReq.PagingQuery);
        }
    }
}
