using PI.Domain.Common.PagedLists;
using PI.Domain.Dto.Product;
using PI.Domain.Enums;
using PI.Domain.Models;
using PI.Domain.Repositories.Common;

namespace PI.Domain.Repositories
{
    public interface ILotRepository : IGenericRepository<Lot>
    {
        Task<IPagedList<ProductLotResponse>> SearchAsync(int productUnitId, PagingQuery pagingQuery, string orderBy, LotStatus? lotStatus);
    }
}