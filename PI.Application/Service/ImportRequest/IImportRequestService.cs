using PI.Domain.Dto.ImportRequest;
using PI.Domain.Dto.ImportRequest.MergeImportRequest;

namespace PI.Application.Service
{
    public interface IImportRequestService : IDisposable
    {
        Task<PagingApiResponse<ImportRequestResponse>> SearchImportRequest(SearchImportReqRequest searchReq);
        Task<ApiResponse<ImportRequestDetailResponse>> GetImportRequestDetail(int importRequestId);
        Task<ApiResponse<bool>> CreateImportRequest(CreateImportReqRequest request);
        Task<ApiResponse<IEnumerable<ImportRequestProductResponse>>> GetTotalImportRequestQuantity(SearchBaseRequest searchRequest);
        Task<ApiResponse<bool>> MergeImportRequest(MergeImportReqRequest request);
        Task<ApiResponse<bool>> DeleteImportRequest(params int[] importRequestIds);
        Task<ApiResponse<bool>> ChangeImportRequestStatus(ImportRequestStatus importRequestStatus, params int[] importRequestIds);
    }
}
