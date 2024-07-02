using PI.Domain.Common;
using PI.Domain.Common.PagedLists;
using PI.Domain.Dto.ImportRequest;
using PI.Domain.Models;
using PI.Domain.Repositories.Common;

namespace PI.Domain.Repositories
{
    public interface IImportRequestRepository : IGenericRepository<ImportRequest>
    {
        Task<IPagedList<ImportRequestResponse>> SearchAsync(SearchImportReqRequest searchReq);
        Task<IEnumerable<ImportRequestDetail>?> GetImportRequestDetail(int importRequestId);
        Task<IEnumerable<ImportRequestDetailResponse>?> GetImportRequests(params int[] importRequestIds);
        Task<IEnumerable<ImportRequestProductResponse>> GetTotalImportRequestProductQuantity(SearchBaseRequest req);
        Task<bool> IsPendingImportRequests(params int[] importRequestIds);
    }
}
