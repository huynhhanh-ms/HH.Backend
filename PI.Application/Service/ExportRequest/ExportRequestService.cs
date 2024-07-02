using PI.Domain.Dto.ExportRequest;
using PI.Domain.Infrastructure.Auth;
using PI.Domain.Models;
using PI.Domain.Repositories;
using PI.Domain.Repositories.Common;

namespace PI.Application.Service
{
    public class ExportRequestService : BaseService, IExportRequestService
    {
        private readonly ICurrentAccount _currentAccount;
        private readonly IProductStockService _productStockService;
        public ExportRequestService(IUnitOfWork unitOfWork,
            ICurrentAccount currentAccount,
            IProductStockService productStockService) : base(unitOfWork)
        {
            _currentAccount = currentAccount;
            _productStockService = productStockService;
        }

        public async Task<PagingApiResponse<ExportRequestResponse>> SearchExportRequest(SearchExportReqRequest searchReq)
        {
            try
            {
                var responses = await _unitOfWork.Resolve<IExportRequestRepository>()
                   .SearchAsync(searchReq);

                return Success(responses);
            }
            catch (Exception ex)
            {
                return PagingFailed<ExportRequestResponse>(ex.GetExceptionMessage());
            }
        }

        public async Task<ApiResponse<ExportRequestDetailResponse>> GetExportRequestDetail(int exportRequestId)
        {
            try
            {
                var exportReq = await _unitOfWork.Resolve<ExportRequest>().FindAsync(exportRequestId);
                var exportReqDetail = await _unitOfWork.Resolve<IExportRequestDetailRepository>()
                    .GetByExportRequestId(exportRequestId);

                if (exportReq == null || exportReqDetail == null || !exportReqDetail.Any())
                    return Failed<ExportRequestDetailResponse>("Export request not found");

                return Success(new ExportRequestDetailResponse
                {
                    ExportRequestId = exportReq.ExportRequestId,
                    ExportStatus = exportReq.ExportStatus,
                    CreatedAt = exportReq.CreatedAt,
                    CreatedBy = exportReq.CreatedBy,
                    ExportRequestDetails = exportReqDetail.Select(x => new ExportRequestDetailItemResponse
                    {
                        ExportRequestDetailId = x.ExportRequestDetailId,
                        Name = x.ProductUnit.Name,
                        ProductUnitId = x.ProductUnitId,
                        SkuCode = x.ProductUnit.SkuCode,
                        UnitName = x.ProductUnit.Unit.Name,
                        Quantity = x.Quantity
                    })
                });
            }
            catch (Exception ex)
            {
                return Failed<ExportRequestDetailResponse>(ex.GetExceptionMessage());
            }
        }

        public async Task<ApiResponse<bool>> CreateExportRequest(CreateExportRequestReq request)
        {
            try
            {
                int result = 0;
                var isProductUnitValid = await _unitOfWork.Resolve<ProductUnit>()
                    .IsExist(request.ExportRequestDetails.Select(e => e.ProductUnitId).ToArray());

                if (!isProductUnitValid)
                    return Failed<bool>("Product unit not found", HttpStatusCode.BadRequest);

                await _unitOfWork.BeginTransactionAsync();
                {
                    var exportRequest = new ExportRequest
                    {
                        ExportStatus = ExportRequestStatus.Pending.ToString().ToLower()
                    };

                    var exportRequestDetails = request.ExportRequestDetails.Select(x => new ExportRequestDetail
                    {
                        ExportRequestId = exportRequest.ExportRequestId,
                        ProductUnitId = x.ProductUnitId,
                        Quantity = x.Quantity
                    });

                    exportRequest.ExportRequestDetails = exportRequestDetails.ToList();

                    await _unitOfWork.Resolve<ExportRequest>().CreateAsync(exportRequest);

                    result = await _unitOfWork.SaveChangesAsync();
                }
                await _unitOfWork.CommitTransactionAsync();
                return Success(result > 0);
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                return Failed<bool>(ex.GetExceptionMessage());
            }
        }

        public async Task<ApiResponse<bool>> DeleteExportRequest(params int[] exportRequestIds)
        {
            try
            {
                var isExist = await _unitOfWork.Resolve<ExportRequest>().IsExist(exportRequestIds);
                if (!isExist)
                    return Failed<bool>("Export request not found", HttpStatusCode.BadRequest);

                await _unitOfWork.Resolve<ExportRequest>().DeleteAsync(exportRequestIds);
                var result = await _unitOfWork.SaveChangesAsync();

                return Success(result > 0);
            }
            catch (Exception ex)
            {
                return Failed<bool>(ex.GetExceptionMessage());
            }
        }

        // for accept or reject export request
        public async Task<ApiResponse<bool>> ChangeExportRequestStatus(ExportRequestStatus exportRequestStatus,params int[] exportRequestIds)
        {
            try
            {
                var exportRequests = await _unitOfWork.Resolve<ExportRequest>()
                    .FindListAsync(p => exportRequestIds.Contains(p.ExportRequestId));

                if(exportRequests.Count() != exportRequestIds.Length)
                    return Failed<bool>("Export requests not found", HttpStatusCode.BadRequest);

                if (!exportRequests.All(p => p.ExportStatus == ExportRequestStatus.Pending.ToString().ToLower()))
                    return Failed<bool>("Export requests are not pending", HttpStatusCode.BadRequest);
                

                foreach (var exportRequest in exportRequests)
                {
                    exportRequest.ExportStatus = exportRequestStatus.ToString().ToLower();
                }

                await _unitOfWork.Resolve<ExportRequest>().UpdateAsync(exportRequests.ToArray());
                var result = await _unitOfWork.SaveChangesAsync();

                return Success(result > 0);
            }
            catch (Exception ex)
            {
                return Failed<bool>(ex.GetExceptionMessage());
            }
        }

        //for complete export request
        public async Task<(bool, string)> CompeleteExportRequest(int exportRequestId)
        {
            try
            {
                var exportRequest = await _unitOfWork.Resolve<ExportRequest>()
                                                    .FindAsync(exportRequestId);

                if (exportRequest == null)
                    return (false, "Export request not found");

                exportRequest.ExportStatus = ExportRequestStatus.Completed.ToString().ToLower();
                await _unitOfWork.Resolve<ExportRequest>().UpdateAsync(exportRequest);
                await _unitOfWork.SaveChangesAsync();

                return (true, string.Empty);
            }
            catch (Exception ex)
            {
                return (false, ex.GetExceptionMessage());
            }
        }
    }
}
