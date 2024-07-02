using PI.Domain.Common.PagedLists;
using PI.Domain.Dto.Product;
using PI.Domain.Enums;
using PI.Domain.Models;
using PI.Domain.Repositories.Common;

namespace PI.Domain.Repositories
{
    public interface IProductUnitRepository : IGenericRepository<ProductUnit>
    {
        Task<IPagedList<ProductUnit>> SearchProductLot(string skuCode, PagingQuery pagingQuery, string orderBy, LotStatus? lotStatus);
        Task<IPagedList<ProductUnit>> SearchProductHistory(string skuCode, PagingQuery pagingQuery, string orderBy, ShipmentType? shipmentStatus);
        Task<IPagedList<ProductUnitResponse>> SearchProductUnit(string skuCode, PagingQuery pagingQuery, string orderBy);
        Task<bool> IsExist(string skuCode);
    }
}