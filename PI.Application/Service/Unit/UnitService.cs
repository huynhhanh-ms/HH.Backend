using PI.Domain.Common.PagedLists;
using PI.Domain.Dto.Unit;
using PI.Domain.Models;
using PI.Domain.Repositories.Common;

namespace PI.Application.Service
{
    public class UnitService : BaseService, IUnitService
    {
        public UnitService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<ApiResponse<IPagedList<UnitResponse>>> SearchUnit(string keySearch, PagingQuery pagingQuery, string orderBy)
        {
            var responses = await _unitOfWork.Resolve<Unit>().SearchAsync<UnitResponse>(keySearch, pagingQuery, orderBy);
            return Success<IPagedList<UnitResponse>>(responses);
        }
    }
}