using PI.Domain.Common.PagedLists;
using PI.Domain.Dto.ExportRequest;
using PI.Domain.Models;
using PI.Domain.Repositories.Common;

namespace PI.Domain.Repositories
{
    public interface IExportRequestRepository : IGenericRepository<ExportRequest>
    {
        Task<IPagedList<ExportRequestResponse>> SearchAsync(SearchExportReqRequest searchReq);
    }
}
