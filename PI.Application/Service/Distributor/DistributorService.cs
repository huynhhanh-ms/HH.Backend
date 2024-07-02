using PI.Domain.Common.PagedLists;
using PI.Domain.Dto.Distributor;
using PI.Domain.Models;
using PI.Domain.Repositories.Common;

namespace PI.Application.Service
{
    public class DistributorService : BaseService, IDistributorService
    {
        public DistributorService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<ApiResponse<string>> Create(CreateDistributorRequest request)
        {
            try
            {
                var distributor = request.Adapt<Distributor>();
                await _unitOfWork.Resolve<Distributor>().CreateAsync(distributor);
                await _unitOfWork.SaveChangesAsync();
                return Success<string>("Create distributor success!");
            }
            catch (Exception e)
            {
                return Failed<string>(e.GetExceptionMessage());
            }
        }

        //search distributor
        public async Task<ApiResponse<IPagedList<DistributorResponse>>> Search(string? keySearch,
            PagingQuery? pagingQuery,
            string? orderByStr)
        {
            try
            {
                var distributors = await _unitOfWork.Resolve<Distributor>()
                    .SearchAsync<DistributorResponse>(keySearch, pagingQuery, orderByStr);
                return Success<IPagedList<DistributorResponse>>(distributors);
            }
            catch (Exception e)
            {
                return Failed<IPagedList<DistributorResponse>>(e.GetExceptionMessage());
            }
        }
    }
}