using PI.Domain.Dto.StockCheck;
using PI.Domain.Exceptions;
using PI.Domain.Extensions;
using PI.Domain.Models;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Eventing.Reader;
using System.Net.WebSockets;
using static PI.Domain.Enums.StockCheckEnum;

namespace PI.Persitence.Repository
{
    public class StockCheckRepository : GenericRepository<StockCheck>, IStockCheckRepository
    {
        public StockCheckRepository(DbContext context) : base(context)
        {
        }

        public override Task<StockCheck?> FindAsync(int entityId)
        {
            return _dbSet
                .Include(x => x.StockCheckDetails)
                .WhereWithExist(x => x.StockCheckId == entityId)
                .FirstOrDefaultAsync();
        }

        public override Task<IPagedList<StockCheck>> SearchAsync(string keySearch, PagingQuery pagingQuery,
            string orderBy)
        {
            throw new NotImplementedException();
        }

        public override async Task<IPagedList<TResult>> SearchAsync<TResult>(string keySearch, PagingQuery pagingQuery,
            string orderBy)
        {
            return await _dbSet.AsNoTracking()
                .WhereWithExist(p => string.IsNullOrEmpty(keySearch))
                .WithOrderByString(orderBy)
                .SelectWithField<StockCheck, TResult>()
                .ToPagedListAsync(pagingQuery);
        }

        public async Task<IPagedList<SearchStockCheckResponse>> SearchAsync(SearchStockCheckRequest request)
        {
            string[] priority = null;
            if (request.Priority != null)
            {
                priority = request.Priority.Select(x => x.ToString()).ToArray();
            }

            string[] status = null;
            if (request.Status != null)
            {
                status = request.Status.Select(x => x.ToString()).ToArray();
            }

            bool canParseId = int.TryParse(request.KeySearch, out int id);

            return await _dbSet.AsNoTracking()
                .Include(x => x.Staff)
                .Include(x => x.CreatedByNavigation)
                .WhereWithExist(p =>
                    (request.StaffId == null || p.StaffId == request.StaffId)
                    && (request.KeySearch == null
                        || (canParseId && p.StockCheckId == id)
                        || p.Title.Contains(request.KeySearch))
                    && (request.DateFrom == null || p.CreatedAt >= request.DateFrom)
                    && (request.DateTo == null || p.CreatedAt <= request.DateTo)
                    && (priority == null || priority.Contains(p.Priority))
                    && (request.Status == null || status.Contains(p.Status))
                    && (request.StockkeeperId == null || p.StockkeeperId == p.StockkeeperId)
                    && (request.IsUsedForBalancing == null || p.IsUsedForBalancing == request.IsUsedForBalancing)
                )
                .WithOrderByString(request.OrderBy)
                .SelectWithField<StockCheck, SearchStockCheckResponse>()
                .ToPagedListAsync(request.PagingQuery);
        }

        public async Task<IPagedList<SearchStockCheckResponse>> SearchStockBalanceAsync(SearchStockCheckRequest request)
        {
            string[] priority = null;
            if (request.Priority != null)
            {
                priority = request.Priority.Select(x => x.ToString()).ToArray();
            }

            string[] status = null;
            if (request.Status != null)
            {
                status = request.Status.Select(x => x.ToString()).ToArray();
            }

            return await _dbSet.AsNoTracking()
                .Include(x => x.Staff)
                .Include(x => x.CreatedByNavigation)
                .WhereWithExist(p =>
                    (request.StaffId == null || p.StaffId == request.StaffId)
                    && (request.DateFrom == null || p.CreatedAt >= request.DateFrom)
                    && (request.DateTo == null || p.CreatedAt <= request.DateTo)
                    && (priority == null || priority.Contains(p.Priority))
                    && (request.Status == null || status.Contains(p.Status))
                    && (request.StockkeeperId == null || p.StockkeeperId == p.StockkeeperId)
                    && (request.IsUsedForBalancing == null || p.IsUsedForBalancing == request.IsUsedForBalancing)
                )
                //check balance stock 
                .Where(p => p.Status == StockCheckStatus.Completed.ToString() && !p.IsUsedForBalancing
                                                                              && p.StockCheckDetails.Any(x =>
                                                                                  x.ActualQuantity !=
                                                                                  x.EstimatedQuantity))
                .WithOrderByString(request.OrderBy)
                .SelectWithField<StockCheck, SearchStockCheckResponse>()
                .ToPagedListAsync(request.PagingQuery);
        }

        public async Task<StockCheckDetailResponse> SearchStockCheckDetail(int stockCheckId, SearchStockCheckDetailRequest request)
        {
            var stockCheck = await _dbSet.AsNoTracking()
               .Include(x => x.Staff)
               .Include(x => x.Stockkeeper)
               .WhereWithExist(p => p.StockCheckId == stockCheckId)
               .SelectWithField<StockCheck, StockCheckDetailResponse>()
               .FirstOrDefaultAsync();

            ValidateException.ThrowIfNull(stockCheck, "Stock check not found");

            var command = _dbContext.Set<StockCheckDetail>()
                .AsNoTracking()
                .Include(x => x.ProductUnit)
                .WhereWithExist(p => p.StockCheckId == stockCheckId
                    && (request.StockCheckDetailStatus == null || p.Status == request.StockCheckDetailStatus.ToString())
                );

            if (request.StockDiff != null)
            {
                switch (request.StockDiff)
                {
                    case StockDiff.Less:
                        command = command.Where(x => x.ActualQuantity < x.EstimatedQuantity);
                        break;
                    case StockDiff.More:
                        command = command.Where(x => x.ActualQuantity > x.EstimatedQuantity);
                        break;
                    case StockDiff.LessAndMore:
                        command = command.Where(x => x.ActualQuantity != x.EstimatedQuantity);
                        break;
                    case StockDiff.Equal:
                        command = command.Where(x => x.ActualQuantity == x.EstimatedQuantity);
                        break;
                }
            }

            stockCheck.StockCheckDetails.Data = await command
                .WithOrderByString(request.OrderBy)
                .SelectWithField<StockCheckDetail, StockCheckDetailItem>()
                .ToPagedListAsync(request.PagingQuery);

            return stockCheck;
        }
    }
}