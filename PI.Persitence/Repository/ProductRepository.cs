using Microsoft.EntityFrameworkCore;
using PI.Domain.Common.PagedLists;
using PI.Domain.Dto.Product;
using PI.Domain.Models;
using PI.Domain.Repositories;
using PI.Persitence.Repository.Common;
using PI.Persitence.Repository.Helper;

namespace PI.Persitence.Repository
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(DbContext context) : base(context)
        {
        }

        public async Task<ProductResponse> GetProductDetail(int id)
        {
            return await _dbSet.AsNoTracking()
                .WhereWithExist(p => p.ProductId == id)
                .SelectWithField<Product, ProductResponse>()
                .FirstOrDefaultAsync();
        }

        public override Task<IPagedList<Product>> SearchAsync(string keySearch, PagingQuery pagingQuery, string orderBy)
        {
            return _dbSet.AsNoTracking()
                .WhereWithExist(p => string.IsNullOrEmpty(keySearch) ||
                                      p.Name.Contains(keySearch))
                .WithOrderByString(orderBy)
                .ToPagedListAsync(pagingQuery);
        }


        public override Task<IPagedList<TResult>> SearchAsync<TResult>(string keySearch, PagingQuery pagingQuery,
            string orderBy)
        {
            return _dbSet.AsNoTracking()
                .WhereWithExist(p => string.IsNullOrEmpty(keySearch) ||
                                      p.Name.Contains(keySearch))
                .WithOrderByString(orderBy)
                .SelectWithField<Product, TResult>()
                .ToPagedListAsync(pagingQuery);
        }

        public Task<IPagedList<ProductResponse>> SearchAsync(SearchProductRequest request)
        {
            return _dbSet.AsNoTracking()
                .WhereWithExist(p => (string.IsNullOrEmpty(request.KeySearch)
                                        || p.Name.Contains(request.KeySearch))
                                    && (request.CategoryId == null || p.CategoryId == request.CategoryId)
                                    && (request.IsAvailable == null || p.IsAvailable == request.IsAvailable)
                                    &&
                //check is low on stock in (sum productUnitQuantity in product unit in productStock < maxQuantity in categorySetting)
                //if categorySetting is null, then return all product
                (request.IsLowOnStock == null
                ||
                request.IsLowOnStock == true && p.ProductUnits
                                                         .Sum(pu => pu.ProductStocks
                                                                      .Sum(ps => ps.StockQuantity)) < p.Category.CategorySettings
                                                                                                                .Sum(p => p.MinQuantity)
                ||
                request.IsLowOnStock == false && p.ProductUnits
                                                   .Sum(pu => pu.ProductStocks
                                                                .Sum(ps => ps.StockQuantity)) >= p.Category.CategorySettings
                                                                                                  .Sum(p => p.MinQuantity)
                )
                )
                .Include(p => p.Medicine)
                    .ThenInclude(p => p.Manufacturer)
                .Include(p => p.Category)
                .Include(p => p.ProductUnits)
                .WithOrderByString(request.OrderBy)
                .SelectWithField<Product, ProductResponse>()
                .ToPagedListAsync(request.PagingQuery);
        }
    }
}