using PI.Domain.Common.PagedLists;
using PI.Domain.Dto.Product;
using PI.Domain.Dto.Shipment;
using PI.Domain.Enums;
using PI.Domain.Models;
using PI.Domain.Repositories.Common;

namespace PI.Domain.Repositories
{
    public interface IShipmentRepository : IGenericRepository<Shipment>
    {
        Task<IPagedList<ImportShipmentResponse>> SearchImportShipmentAsync(string keySearch, DateTime? fromDate, DateTime? endDate, PagingQuery pagingQuery, string orderBy);

        Task<IEnumerable<Shipment>> FindAllShipmentAsync(int productUnitId);
        Task<IPagedList<ProductHistoryResponse>> SearchShipmentAsync(int productUnitId, SearchProductHistory searchReq);

        Task<IPagedList<ExportShipmentResponse>> SearchExportShipmentAsync(string keySearch, DateTime? fromDate, DateTime? endDate, PagingQuery pagingQuery, string orderBy);

        Task<int> SearchProductStatisticAsync(string sku, DateTime? fromDate, DateTime? endDate, ProductStatistic statistic, ShipmentType status = ShipmentType.Import);


    }
}