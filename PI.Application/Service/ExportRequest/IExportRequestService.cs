using PI.Domain.Common.PagedLists;
using PI.Domain.Common;
using PI.Domain.Dto.ExportRequest;

namespace PI.Application.Service
{
    public interface IExportRequestService : IDisposable
    {

        Task<PagingApiResponse<ExportRequestResponse>> SearchExportRequest(SearchExportReqRequest searchReq);
        Task<ApiResponse<ExportRequestDetailResponse>> GetExportRequestDetail(int exportRequestId);
        Task<ApiResponse<bool>> CreateExportRequest(CreateExportRequestReq request);
        Task<ApiResponse<bool>> DeleteExportRequest(params int[] exportRequestIds);
        Task<(bool, string)> CompeleteExportRequest(int exportRequestId);
        Task<ApiResponse<bool>> ChangeExportRequestStatus(ExportRequestStatus exportRequestStatus, params int[] exportRequestIds);
    }
}
