using Microsoft.EntityFrameworkCore;
using PI.Domain.Dto.Distributor;
using PI.Domain.Dto.ExportRequest;
using PI.Domain.Dto.Product;
using PI.Domain.Dto.Shipment;
using PI.Domain.Models;
using PI.Domain.Repositories;


namespace PI.Persitence.Repository
{
    public class ShipmentRepository : GenericRepository<Shipment>, IShipmentRepository
    {
        public ShipmentRepository(DbContext context) : base(context)
        {
        }

        public override Task<IPagedList<Shipment>> SearchAsync(string keySearch, PagingQuery pagingQuery,
            string orderBy)
        {
            return _dbSet.AsNoTracking()
                .WhereWithExist(p => string.IsNullOrEmpty(keySearch) ||
                                     p.ShipmentId.ToString().Contains(keySearch))
                .WithOrderByString(orderBy)
                .ToPagedListAsync(pagingQuery);
        }

        public override async Task<IPagedList<TResult>> SearchAsync<TResult>(string keySearch, PagingQuery pagingQuery,
            string orderBy)
        {
            return await _dbSet.AsNoTracking()
                .WhereWithExist(p => string.IsNullOrEmpty(keySearch) ||
                                     p.ShipmentId.ToString().Contains(keySearch))
                .WithOrderByString(orderBy)
                .SelectWithField<Shipment, TResult>()
                .ToPagedListAsync(pagingQuery);
        }

        public async Task<IPagedList<ImportShipmentResponse>> SearchImportShipmentAsync(string keySearch,
            DateTime? fromDate,
            DateTime? toDate, PagingQuery pagingQuery, string orderBy)
        {
            return await _dbSet.AsNoTracking()
                .WhereWithExist(p => (string.IsNullOrEmpty(keySearch) ||
                                      p.ShipmentId.ToString().Contains(keySearch))
                                     && (fromDate == null || p.ShipmentDate >= fromDate)
                                     && (toDate == null || p.ShipmentDate <= toDate)
                                     && (p.ShipmentType == ShipmentType.Import.ToString()  || p.ShipmentType == ShipmentType.ImportBalance.ToString())
                )
                .WithOrderByString(orderBy)
                .SelectWithField<Shipment, ImportShipmentResponse>()
                .ToPagedListAsync(pagingQuery);
        }

        //find all shipment
        public async Task<IEnumerable<Shipment>> FindAllShipmentAsync(int productUnitId)
        {
            return await _dbSet.AsNoTracking()
                .Where(p => p.ShipmentDetails.Any(s => s.ProductUnitId == productUnitId))
                .Include(p => p.ShipmentDetails)
                .ToListAsync();
        }

        public async Task<IPagedList<ProductHistoryResponse>> SearchShipmentAsync(int productUnitId,
            SearchProductHistory searchReq)
        {
            return await _dbSet.AsNoTracking()
                .WhereWithExist(p => p.ShipmentDetails.Any(s => s.ProductUnitId == productUnitId)
                                     && p.ShipmentDetails.Any(s => s.ProductUnitId == productUnitId)
                                     && (searchReq.ShipmentStatus == null
                                         || p.ShipmentType.ToLower() == searchReq.ShipmentStatus.ToString().ToLower())
                                     && (searchReq.fromDate == null || p.ShipmentDate >= searchReq.fromDate)
                                     && (searchReq.toDate == null || p.ShipmentDate <= searchReq.toDate)
                )
                .Include(p => p.ShipmentDetails)
                .WithOrderByString(searchReq.OrderBy)
                .SelectWithField<Shipment, ProductHistoryResponse>(
                    p => new ProductHistoryResponse
                    {
                        ShipmentId = p.ShipmentId,
                        ShipmentDate = p.ShipmentDate,
                        ShipmentStatus = Enum.Parse<ShipmentType>(p.ShipmentType),
                        ProductUnitId = productUnitId,
                        ReceiptUrl = p.ReceiptUrl,
                        Quantity = p.ShipmentDetails.Where(s => s.ProductUnitId == productUnitId).Sum(s => s.Quantity),
                        Price = p.ShipmentDetails.Where(s => s.ProductUnitId == productUnitId).Sum(s => s.Price ?? 0),
                        EndingStocks = p.ShipmentDetails.Where(s => s.ProductUnitId == productUnitId).Sum(s => s.Quantity),
                        Distributor = p.Distributor.Adapt<DistributorResponse>(),
                        ExportRequest = p.ExportRequest.Adapt<ExportRequestResponse>()
                    }
                )
                .ToPagedListAsync(searchReq.PagingQuery);
        }

        public Task<IPagedList<ExportShipmentResponse>> SearchExportShipmentAsync(string keySearch, DateTime? fromDate,
            DateTime? toDate, PagingQuery pagingQuery,
            string orderBy)
        {
            return _dbSet.AsNoTracking() 
                .WhereWithExist(p => (p.ShipmentType == ShipmentType.Export.ToString() || p.ShipmentType == ShipmentType.ExportBalance.ToString())
                                     && (string.IsNullOrEmpty(keySearch) || p.ShipmentId.ToString().Contains(keySearch))
                                     && (fromDate == null || p.ShipmentDate >= fromDate)
                                     && (toDate == null || p.ShipmentDate <= toDate)
                )
                .WithOrderByString(orderBy)
                .SelectWithField<Shipment, ExportShipmentResponse>()
                .ToPagedListAsync(pagingQuery);
        }

        public async Task<int> SearchProductStatisticAsync(string sku, DateTime? fromDate, DateTime? endDate,
            ProductStatistic statistic, ShipmentType status = ShipmentType.Import)
        {
            // follow statistic month or quarter
            return await _dbSet.AsNoTracking()
                .WhereWithExist(p => p.ShipmentType == status.ToString().ToLower()
                                     && p.ShipmentDate >= fromDate
                                     && p.ShipmentDate <= endDate
                )
                .SumAsync(p => p.ShipmentDetails
                    .Where(s => s.ProductUnit.SkuCode == sku)
                    .Sum(s => s.Quantity)
                );
        }
    }
}