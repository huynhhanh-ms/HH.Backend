using Dapper;
using PI.Domain.Dto.ProductStock;
using PI.Domain.Dto.ProductStock.ProductStockEstimate;
using System.Data;

namespace PI.Persitence.Repository
{
    public class ProductStockRepository : GenericRepository<ProductStock>, IProductStockRepository
    {
        public ProductStockRepository(DbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<ProductStockResponse>> GetProductStockReponseByExportRequestId(int exportRequestId)
        {
            return await _dbContext
                .Set<ExportRequestDetail>()
                .WhereWithExist(x => x.ExportRequestId == exportRequestId)
                .Join(_dbContext.Set<ProductStock>(), x => x.ProductUnitId, y => y.ProductUnitId,
                 (x, y) => y)
                .Where(y => y.IsDeleted == false && y.IsCurrent == true)
                .Select(y => new ProductStockResponse
                {
                    ProductUnitId = y.ProductUnitId,
                    StockQuantity = y.StockQuantity,
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<CheckStock4ExportReqResponse>> CheckStock4ExportReq(int exportRequestId)
        {
            return await _dbContext
                .Set<ExportRequestDetail>()
                .WhereWithExist(x => x.ExportRequestId == exportRequestId)
                .Join(_dbContext.Set<ProductStock>(), x => x.ProductUnitId, y => y.ProductUnitId,
                                   (exportRequestDetail, productStock) => new { exportRequestDetail, productStock })
                .Where(z => z.productStock.IsDeleted == false && z.productStock.IsCurrent == true)
                .Select(z => new CheckStock4ExportReqResponse
                {
                    ProductUnitId = z.exportRequestDetail.ProductUnitId,
                    IsValid = z.productStock.StockQuantity >= z.exportRequestDetail.Quantity
                })
                .ToListAsync();
        }

        public override Task<IPagedList<ProductStock>> SearchAsync(string keySearch, PagingQuery pagingQuery, string orderBy)
        {
            throw new NotImplementedException();
        }

        public override async Task<IPagedList<TResult>> SearchAsync<TResult>(string keySearch, PagingQuery pagingQuery, string orderBy)
        {
            throw new NotImplementedException();
        }

        public async Task<IPagedList<EstimateProductStockResponse>> SearchEstProductStock(SearchProductStockEstimateRequest request)
        {
            var param = new DynamicParameters();
            param.Add("p_page_size", request.PagingQuery.PageSize);
            param.Add("p_page_number", request.PagingQuery.PageNumber);
            param.Add("p_key_search", request.KeySearch ?? ""); // search with sku code
            param.Add("p_total", dbType: DbType.Int32, direction: ParameterDirection.Output);

            string sql = "sp_get_est_product_quantities";
            var (data, nbTotalData) = await DapperDAO.QueryStoredProcMySql<EstimateProductStockResponse, int>(sql, param, "p_total");

            var result = new PagedList<EstimateProductStockResponse>(data.ToList(), nbTotalData, request.PagingQuery.PageNumber, request.PagingQuery.PageSize);

            return result;
        }

        public async Task<IPagedList<EstimateProductStockWithImportRequestResponse>> SearchEstProductStockWithImportRequest(
            SearchProductStockEstimateRequest request)
        {
            var param = new DynamicParameters();
            param.Add("p_page_size", request.PagingQuery.PageSize);
            param.Add("p_page_number", request.PagingQuery.PageNumber);
            param.Add("p_key_search", request.KeySearch ?? "");
            param.Add("p_total", dbType: DbType.Int32, direction: ParameterDirection.Output);

            string sql = "sp_get_est_product_quantities_with_import_req";
            var (data, nbTotalData) = await DapperDAO.QueryStoredProcMySql<EstimateProductStockWithImportRequestResponse, int>(sql, param, "p_total");

            var result = new PagedList<EstimateProductStockWithImportRequestResponse>(data.ToList(), nbTotalData, request.PagingQuery.PageNumber, request.PagingQuery.PageSize);

            return result;
        }

        public async Task<IList<ProductStock>> GetProductStocks(params int[] productUnitIds)
        {
            return await _dbSet.AsNoTracking()
                            .Where(x => productUnitIds.Contains(x.ProductUnitId))
                            .ToListAsync();
        }

        public async Task<IPagedList<ProductStockResponse>> SearchProductStock(SearchProductStockRequest request)
        {
            return await _dbSet.AsNoTracking()
                            .Include(x => x.ProductUnit)
                            .WhereWithExist(x => x.IsCurrent == true
                                && (string.IsNullOrEmpty(request.KeySearch)
                                                || x.ProductUnit.SkuCode.Equals(request.KeySearch, StringComparison.OrdinalIgnoreCase))
                                && (request.ProductUnitIds == null
                                                || request.ProductUnitIds.Contains(x.ProductUnitId))
                            )
                            .WithOrderByString(request.OrderBy)
                            .SelectWithField<ProductStock, ProductStockResponse>()
                            .ToPagedListAsync(request.PagingQuery);
        }

        public override async Task<IEnumerable<TResult>> GetAllAsync<TResult>()
        {
            return await _dbSet.AsNoTracking()
                .WhereWithExist(x => x.IsCurrent == true)
                .SelectWithField<ProductStock, TResult>()
                .ToListAsync();
        }
    }
}