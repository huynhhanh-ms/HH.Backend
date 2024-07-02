using PI.Domain.Common;
using PI.Domain.Common.PagedLists;
using PI.Domain.Dto.Unit;

namespace PI.Application.Service
{
    public interface IUnitService
    {
        Task<ApiResponse<IPagedList<UnitResponse>>> SearchUnit(string keySearch, PagingQuery pagingQuery, string orderBy);
    }
}