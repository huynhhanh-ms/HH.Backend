using Microsoft.EntityFrameworkCore;
using PI.Domain.Common;
using PI.Domain.Common.PagedLists;
using PI.Domain.Dto.ProductStock;
using PI.Domain.Dto.ProductStock.ProductStockEstimate;
using PI.Domain.Models;
using PI.Domain.Repositories.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PI.Domain.Repositories
{
    public interface IProductStockRepository : IGenericRepository<ProductStock>
    {
        Task<IPagedList<EstimateProductStockResponse>> SearchEstProductStock(SearchProductStockEstimateRequest request);
        Task<IPagedList<EstimateProductStockWithImportRequestResponse>> SearchEstProductStockWithImportRequest(SearchProductStockEstimateRequest request);
        Task<IEnumerable<ProductStockResponse>> GetProductStockReponseByExportRequestId(int exportRequestId);
        Task<IEnumerable<CheckStock4ExportReqResponse>> CheckStock4ExportReq(int exportRequestId);
        Task<IList<ProductStock>> GetProductStocks(params int[] productUnitIds);
        Task<IPagedList<ProductStockResponse>> SearchProductStock(SearchProductStockRequest request);
    }
}
