
using PI.Domain.Dto.ImportRequest;
using PI.Domain.Dto.ImportRequest.MergeImportRequest;
using PI.Domain.Infrastructure.Auth;
using PI.Domain.Models;
using PI.Domain.Repositories;
using PI.Domain.Repositories.Common;

namespace PI.Application.Service
{
    public class ImportRequestService : BaseService, IImportRequestService
    {
        private readonly ICurrentAccount _currentAccount;
        public ImportRequestService(IUnitOfWork unitOfWork,
            ICurrentAccount currentAccount) : base(unitOfWork)
        {
            _currentAccount = currentAccount;
        }

        public async Task<ApiResponse<ImportRequestDetailResponse>> GetImportRequestDetail(int importRequestId)
        {
            var importRequest = await _unitOfWork.Resolve<ImportRequest>().FindAsync(importRequestId);

            if (importRequest == null)
                return Failed<ImportRequestDetailResponse>("Import request not found");

            var importRequestDetail = await _unitOfWork.Resolve<IImportRequestRepository>()
                .GetImportRequestDetail(importRequestId);

            return Success(new ImportRequestDetailResponse
            {
                ImportRequestId = importRequest.ImportRequestId,
                ImportRequestStatus = importRequest.ImportRequestStatus,
                CreatedAt = importRequest.CreatedAt,
                CreatedBy = importRequest.CreatedBy,
                ImportRequestDetails = importRequestDetail.Select(x => new ImportRequestDetailItemResponse
                {
                    ImportRequestDetailId = x.ImportRequestDetailId,
                    Name = x.ProductUnit.Name,
                    ProductUnitId = x.ProductUnitId,
                    SkuCode = x.ProductUnit.SkuCode,
                    UnitName = x.ProductUnit.Unit.Name,
                    Quantity = x.Quantity
                })
            });
        }

        public async Task<PagingApiResponse<ImportRequestResponse>> SearchImportRequest(SearchImportReqRequest searchReq)
        {
            try
            {
                var responses = await _unitOfWork.Resolve<IImportRequestRepository>()
                   .SearchAsync(searchReq);

                return Success(responses);
            }
            catch (Exception ex)
            {
                return PagingFailed<ImportRequestResponse>(ex.GetExceptionMessage());
            }
        }

        public async Task<ApiResponse<bool>> CreateImportRequest(CreateImportReqRequest createReq)
        {
            try
            {
                int result = 0;

                var (isValiadRequest, message) = await ValidateCreateImportReqRequest(createReq);
                if (!isValiadRequest)
                    return Failed<bool>(message, HttpStatusCode.BadRequest);

                await _unitOfWork.BeginTransactionAsync();
                {
                    var importRequest = new ImportRequest
                    {
                        ImportRequestStatus = ImportRequestStatus.Pending.ToString(),
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now,
                        CreatedBy = _currentAccount.GetAccountId(),
                        UpdatedBy = _currentAccount.GetAccountId()
                    };

                    var importRequestDetails = createReq.ImportRequestDetails.Select(x => new ImportRequestDetail
                    {
                        ImportRequestId = importRequest.ImportRequestId,
                        ProductUnitId = x.ProductUnitId,
                        Quantity = x.Quantity,
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now,
                        CreatedBy = _currentAccount.GetAccountId(),
                        UpdatedBy = _currentAccount.GetAccountId(),
                    });

                    importRequest.ImportRequestDetails = importRequestDetails.ToList();

                    await _unitOfWork.Resolve<ImportRequest>().CreateAsync(importRequest);

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

        public async Task<(bool, string)> ValidateCreateImportReqRequest(CreateImportReqRequest request)
        {
            try
            {
                var isProductUnitValid = await _unitOfWork.Resolve<ProductUnit>()
                .IsExist(request.ImportRequestDetails.Select(e => e.ProductUnitId).ToArray());

                if (!isProductUnitValid)
                    return (false, "Product unit not found");

                return (true, string.Empty);
            }
            catch (Exception ex)
            {
                return (false, ex.GetExceptionMessage());
            }
        }

        public async Task<ApiResponse<IEnumerable<ImportRequestProductResponse>>> GetTotalImportRequestQuantity(SearchBaseRequest searchRequest)
        {
            try
            {
                var totalImportRequestQuantity = await _unitOfWork.Resolve<IImportRequestRepository>()
                    .GetTotalImportRequestProductQuantity(searchRequest);

                return Success(totalImportRequestQuantity);
            }
            catch (Exception ex)
            {
                return Failed<IEnumerable<ImportRequestProductResponse>>(ex.GetExceptionMessage());
            }
        }

        public async Task<ApiResponse<bool>> MergeImportRequest(MergeImportReqRequest request)
        {
            try
            {
                var (validateResult, errorMess) = await ValidateMergeImportReqRequest(request);
                if (!validateResult)
                    return Failed<bool>(errorMess, HttpStatusCode.BadRequest);

                var importRequests = await _unitOfWork.Resolve<IImportRequestRepository>()
                    .GetImportRequests(request.ImportRequest.ToArray());

                var importRequestDetailGroups = importRequests.SelectMany(x => x.ImportRequestDetails)
                                                         .GroupBy(x => x.ProductUnitId);

                var createImportReqItems = new List<CreateImportRequestItem>();
                foreach (var importRequestDetail in importRequestDetailGroups)
                {
                    var totalQuantity = importRequestDetail.Sum(x => x.Quantity);
                    createImportReqItems.Add(new CreateImportRequestItem
                    {
                        ProductUnitId = importRequestDetail.Key,
                        Quantity = totalQuantity
                    });
                }

                // insert new import request and delete old import request

                var res = await CreateImportRequest(new CreateImportReqRequest() { ImportRequestDetails = createImportReqItems });

                if (res.StatusCode == HttpStatusCode.OK)
                {
                    await _unitOfWork.Resolve<IImportRequestRepository>()
                        .DeleteAsync(request.ImportRequest.ToArray());

                    await _unitOfWork.SaveChangesAsync();
                }

                return res;
            }
            catch (Exception ex)
            {
                return Failed<bool>(ex.GetExceptionMessage());
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns>
        ///     1. bool: true if request is valid, false otherwise
        ///     2. string: error message if request is invalid
        /// </returns>
        private async Task<(bool, string)> ValidateMergeImportReqRequest(MergeImportReqRequest request)
        {
            try
            {
                var result = await _unitOfWork.Resolve<IImportRequestRepository>()
                    .IsExist(request.ImportRequest.ToArray());

                if (!result)
                    return (false, "Import request not found");

                result = await _unitOfWork.Resolve<IImportRequestRepository>()
                    .IsPendingImportRequests(request.ImportRequest.ToArray());

                return result ? (true, string.Empty)
                              : (false, "Import request is not pending");
            }
            catch (Exception ex)
            {
                return (false, ex.GetExceptionMessage());
            }
        }

        public async Task<ApiResponse<bool>> DeleteImportRequest(params int[] importRequestIds)
        {
            try
            {
                var areExist = await _unitOfWork.Resolve<ImportRequest>().IsExist(importRequestIds);
                if (areExist == false)
                    return Failed<bool>("Import request not found", HttpStatusCode.BadRequest);

                await _unitOfWork.Resolve<ImportRequest>().DeleteAsync(importRequestIds);
                var result = await _unitOfWork.SaveChangesAsync();

                return Success(result > 0);
            }
            catch (Exception ex)
            {
                return Failed<bool>(ex.GetExceptionMessage());
            }
        }

        public async Task<ApiResponse<bool>> ChangeImportRequestStatus(ImportRequestStatus importRequestStatus, params int[] importRequestIds)
        {
            try
            {
                var importRequests = await _unitOfWork.Resolve<ImportRequest>()
                    .FindListAsync(p => importRequestIds.Contains(p.ImportRequestId));

                if (importRequests.Count() != importRequestIds.Length)
                    return Failed<bool>("Import requests not found", HttpStatusCode.BadRequest);

                if (!importRequests.All(x => x.ImportRequestStatus == ImportRequestStatus.Pending.ToString().ToLower()))
                    return Failed<bool>("Import requests are not pending", HttpStatusCode.BadRequest);

                foreach (var importRequest in importRequests)
                {
                    importRequest.ImportRequestStatus = importRequestStatus.ToString().ToLower();
                }

                await _unitOfWork.Resolve<ImportRequest>().UpdateAsync(importRequests.ToArray());
                var result = await _unitOfWork.SaveChangesAsync();

                return Success(result > 0);
            }
            catch (Exception ex)
            {
                return Failed<bool>(ex.GetExceptionMessage());
            }
        }
    }
}
