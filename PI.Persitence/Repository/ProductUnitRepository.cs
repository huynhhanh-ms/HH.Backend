using Microsoft.EntityFrameworkCore;
using PI.Domain.Dto.Product;
using PI.Domain.Models;
using PI.Domain.Repositories;

namespace PI.Persitence.Repository
{
    public class ProductUnitRepository : GenericRepository<ProductUnit>, IProductUnitRepository
    {
        public ProductUnitRepository(DbContext context) : base(context)
        {
        }

        public override Task<IPagedList<ProductUnit>> SearchAsync(string keySearch, PagingQuery pagingQuery, string orderBy)
        {
            return _dbSet.AsNoTracking()
                .WhereWithExist(p => string.IsNullOrEmpty(keySearch) ||
                                      p.Name.Contains(keySearch) ||
                                      p.SkuCode.Contains(keySearch))
                .WithOrderByString(orderBy)
                .ToPagedListAsync(pagingQuery);
        }

        public override Task<IPagedList<TResult>> SearchAsync<TResult>(string keySearch, PagingQuery pagingQuery, string orderBy)
        {
            return _dbSet.AsNoTracking()
                .WhereWithExist(p => string.IsNullOrEmpty(keySearch) ||
                                      p.Name.Contains(keySearch) ||
                                      p.SkuCode.Contains(keySearch))
                .WithOrderByString(orderBy)
                .SelectWithField<ProductUnit, TResult>()
                .ToPagedListAsync(pagingQuery);
        }


        public Task<IPagedList<ProductUnit>> SearchProductLot(string skuCode, PagingQuery pagingQuery, string orderBy, LotStatus? lotStatus)
        {
            return _dbSet.AsNoTracking()
                .WhereWithExist(p => p.SkuCode == skuCode)
                .WhereWithExist(p => lotStatus == null || p.Lots.Any(l => l.LotStatus == lotStatus.ToString().ToLower()))
                .WithOrderByString(orderBy)
                .ToPagedListAsync(pagingQuery);
        }

        public Task<IPagedList<ProductUnit>> SearchProductHistory(string skuCode, PagingQuery pagingQuery, string orderBy, ShipmentType? shipmentStatus)
        {
            return _dbSet.AsNoTracking()
                .WhereWithExist(p => p.SkuCode == skuCode)
                .WhereWithExist(p => shipmentStatus == null || p.ShipmentDetails.Any(l => l.Shipment.ShipmentType == shipmentStatus.ToString().ToLower()))
                .WithOrderByString(orderBy)
                .ToPagedListAsync(pagingQuery);
        }

        public Task<bool> IsExist(string skuCode)
        {
            return _dbSet.AnyAsync(p => p.SkuCode == skuCode);
        }

        public Task<IPagedList<ProductUnitResponse>> SearchProductUnit(string keySearch, PagingQuery pagingQuery, string orderBy)
        {
            return _dbSet.AsNoTracking()
                .Include(p => p.Product)
                    .ThenInclude(p => p.ProductImages)
                .WhereWithExist(p => string.IsNullOrEmpty(keySearch) 
                                || p.Name.Contains(keySearch) 
                                || p.SkuCode.Contains(keySearch))
                .WithOrderByString(orderBy)
                .SelectWithField<ProductUnit, ProductUnitResponse>()
                .ToPagedListAsync(pagingQuery);
        }
    }
}