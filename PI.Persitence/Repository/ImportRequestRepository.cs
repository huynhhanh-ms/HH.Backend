using Microsoft.EntityFrameworkCore;
using PI.Domain.Dto.ImportRequest;
using PI.Domain.Models;
using PI.Domain.Repositories;
using System.Linq.Dynamic.Core;

namespace PI.Persitence.Repository
{
    public class ImportRequestRepository : GenericRepository<ImportRequest>, IImportRequestRepository
    {
        public ImportRequestRepository(DbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<ImportRequestDetail>?> GetImportRequestDetail(int importRequestId)
        {
            return await _dbContext.Set<ImportRequestDetail>()
                .WhereWithExist(p => p.ImportRequestId == importRequestId)
                .Include(p => p.ProductUnit)
                    .ThenInclude(p => p.Unit)
                .ToListAsync();
        }

        public async Task<IEnumerable<ImportRequestDetailResponse>?> GetImportRequests(params int[] importRequestIds)
        {
            return await _dbSet
                .WhereWithExist(p => importRequestIds.Contains(p.ImportRequestId))
                .Include(p => p.ImportRequestDetails)
                    .ThenInclude(p => p.ProductUnit)
                    .ThenInclude(p => p.Unit)
                .SelectWithField<ImportRequest, ImportRequestDetailResponse>()
                .ToListAsync();
        }

        public async Task<IEnumerable<ImportRequestProductResponse>> GetTotalImportRequestProductQuantity(SearchBaseRequest req)
        {
            int.TryParse(req.KeySearch, out int productUnitId);
            return await _dbContext.Set<ImportRequestDetail>().AsNoTracking()
                .WhereWithExist(p => (string.IsNullOrEmpty(req.KeySearch) || p.ProductUnitId == productUnitId)
                                    && p.ImportRequest.ImportRequestStatus == ImportRequestStatus.Pending.ToString()
                )
                .Include(p => p.ProductUnit)
                .GroupBy(p => p.ProductUnit)
                .Select(p => new ImportRequestProductResponse
                {
                    ProductUnitId = p.Key.ProductUnitId,
                    ProductSku = p.Key.SkuCode,
                    ProductName = p.Key.Name,
                    Quantity = p.Sum(x => x.Quantity)
                })
                .WithOrderByString(req.OrderBy ?? "Quantity:desc")
                .ToListAsync();
        }

        public async Task<bool> IsPendingImportRequests(params int[] importRequestIds)
        {
            return await _dbSet
                .WhereWithExist(p => importRequestIds.Contains(p.ImportRequestId))
                .AllAsync(p => p.ImportRequestStatus == ImportRequestStatus.Pending.ToString());
        }

        public override Task<IPagedList<ImportRequest>> SearchAsync(string keySearch, PagingQuery pagingQuery, string orderBy)
        {
            throw new NotImplementedException();
        }

        public override Task<IPagedList<TResult>> SearchAsync<TResult>(string keySearch, PagingQuery pagingQuery, string orderBy)
        {
            throw new NotImplementedException();
        }

        public async Task<IPagedList<ImportRequestResponse>> SearchAsync(SearchImportReqRequest searchReq)
        {
            int.TryParse(searchReq.KeySearch, out int id);

            return await _dbSet.AsNoTracking()
                .Include(x => x.ImportRequestDetails)
                    .ThenInclude(x => x.ProductUnit)
                .WhereWithExist(a =>
                                (string.IsNullOrEmpty(searchReq.KeySearch) || a.ImportRequestId == id)
                                && (searchReq.ImportStatus == null
                                    || a.ImportRequestStatus.ToLower() == searchReq.ImportStatus.ToString().ToLower())
                                && (searchReq.CreateDateFrom == null || a.CreatedAt >= searchReq.CreateDateFrom)
                                && (searchReq.CreateDateTo == null || a.CreatedAt <= searchReq.CreateDateTo))
                .WithOrderByString(searchReq.OrderBy)
                .ToPagedListAsync<ImportRequest, ImportRequestResponse>(searchReq.PagingQuery);
        }
    }
}
