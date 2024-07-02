
using Newtonsoft.Json;
using PI.Domain.Dto.StockCheck;
using PI.Domain.Infrastructure.Auth;
using PI.Domain.Models;
using PI.Domain.Repositories;
using System.ComponentModel.DataAnnotations;
using PI.Application.Service.Notification;
using PI.Application.Service.StockBalance;
using PI.Domain.Dto.Notification;
using static PI.Domain.Enums.StockCheckEnum;
using PI.Domain.Constans;
using PI.Domain.Dto.ProductStock;

namespace PI.Application.Service.ProductStockCheck
{
    public class StockCheckService : BaseService, IStockCheckService
    {
        private readonly ICurrentAccount _currentAccount;
        //private readonly INotificationService _notificationService;
        private readonly IShipmentService _shipmentService;
        private readonly IStockBalanceService _stockBalanceService;

        public StockCheckService(IUnitOfWork unitOfWork, ICurrentAccount currentAccount,
            IShipmentService shipmentService, IStockBalanceService stockBalanceService) : base(unitOfWork)
        {
            _currentAccount = currentAccount;
            _shipmentService = shipmentService;
            _stockBalanceService = stockBalanceService;
            //_notificationService = notificationService;
        }

        //Get stock check
        public async Task<PagingApiResponse<SearchStockCheckResponse>> SearchStockCheck(SearchStockCheckRequest request)
        {
            var stockChecks = await _unitOfWork.Resolve<IStockCheckRepository>()
                .SearchAsync(request);

            return Success(stockChecks);
        }

        //Get stock check details have pagination 
        public async Task<ApiResponse<StockCheckDetailResponse>> SearchStockCheckDetail(int stockCheckId, SearchStockCheckDetailRequest request)
        {
            ValidateException.ThrowIf(stockCheckId <= 0, "StockCheckId is invalid");

            var stockCheckDetail = await _unitOfWork.Resolve<IStockCheckRepository>()
                .SearchStockCheckDetail(stockCheckId, request);

            return Success(stockCheckDetail);
        }

        //NOT USED
        //public async Task<ApiResponse<StockCheckDetailResponse>> GetStockCheckDetail(int stockCheckId)
        //{
        //    var stockCheckDetail = await _unitOfWork.Resolve<IStockCheckRepository>()
        //        .GetStockCheckDetail(stockCheckId);

        //    return Success(stockCheckDetail);
        //}

        //Create
        public async Task<ApiResponse<int>> CreateStockCheck(CreateStockCheckRequest request)
        {

            await ValidateCreateStockCheckRequest(request);

            //Validate Staff 
            Account? staff = null;
            if (request.StaffId != null)
            {
                staff = await _unitOfWork.Resolve<Account>().FindAsync(request.StaffId ?? -1);
                ValidateException.ThrowIfNull(staff, "Staff not found");
                ValidateException.ThrowIf(staff.Role != AccountRole.Staff.ToString(), "StaffId is not a staff");
            }

            var stockCheck = request.Adapt<StockCheck>();

            //Init tracking stock check information
            var trackingStockChecks = await UpsertTrackingStockCheck(
                trackingInfo: string.Empty,
                stockCheckStatus: StockCheckStatus.Todo,
                fullname: _currentAccount.GetFullName());

            //If productUnitIds is null or empty, load all product ids from product stock and set to stock check details
            if (request.ProductUnitIds == null || request.ProductUnitIds.Length == 0)
            {
                var productStocks = await _unitOfWork.Resolve<IProductStockRepository>().GetAllAsync<ProductStockResponse>();

                stockCheck.StockCheckDetails = productStocks.Select(x => new StockCheckDetail
                {
                    ProductUnitId = x.ProductUnitId,
                    Status = StockCheckDetailStatus.Todo.ToString(),
                }).ToList();

                stockCheck.StockCheckType = StockCheckType.Regular.ToString(); // magic
            }
            else
            {
                stockCheck.StockCheckType = StockCheckType.Spot.ToString(); // magic 
            }

            //If staffId is not null, set status, otherwise set to Todo
            if (request.StaffId != null)
            {
                trackingStockChecks = await UpsertTrackingStockCheck(
                   trackingInfo: trackingStockChecks,
                   stockCheckStatus: StockCheckStatus.Assigned,
                   fullname: staff.Fullname);

                //if piority is medium set to Assigned and if piority is high set to Accepted
                if (stockCheck.Priority == StockCheckPriority.Highest.ToString())
                {
                    trackingStockChecks = await UpsertTrackingStockCheck(
                        trackingInfo: trackingStockChecks,
                        stockCheckStatus: StockCheckStatus.Accepted,
                        fullname: staff.Fullname);

                    stockCheck.Status = StockCheckStatus.Accepted.ToString();
                }
                else
                {
                    stockCheck.Status = StockCheckStatus.Assigned.ToString();
                }
            }

            //Create stock check details
            if (request.ProductUnitIds?.Count() > 0)
            {
                stockCheck.StockCheckDetails = request.ProductUnitIds.Select(x => new StockCheckDetail
                {
                    ProductUnitId = x,
                    Status = StockCheckDetailStatus.Todo.ToString()
                }).ToList();
            }

            //Tracking stock check information
            stockCheck.Log = trackingStockChecks;

            await _unitOfWork.Resolve<StockCheck>().CreateAsync(stockCheck);
            var result = await _unitOfWork.SaveChangesAsync();

            var stockKeeperIds = await _unitOfWork.Resolve<IAccountRepository>()
                .GetAccountIdsByRoleAsync(AccountRole.Stockkeeper.ToString());

            return Success(stockCheck.StockCheckId);
        }

        private async Task ValidateCreateStockCheckRequest(CreateStockCheckRequest request)
        {
            if (request.StaffId != null)
            {
                var staff = await _unitOfWork.Resolve<Account>().FindAsync(request.StaffId ?? -1);
                ValidateException.ThrowIfNull(staff, "Staff not found");
                ValidateException.ThrowIf(staff.Role != AccountRole.Staff.ToString(), "StaffId is not a staff");
            }

            if (request.ProductUnitIds != null && request.ProductUnitIds.Count() > 0)
            {
                var isExist = await _unitOfWork.Resolve<ProductUnit>().IsExist(request.ProductUnitIds);
                ValidateException.ThrowIf(!isExist, "Product unit not found");
            }

            await Task.CompletedTask;
        }

        //Update
        public async Task<ApiResponse<bool>> UpdateStockCheck(UpdateStockCheckRequest request)
        {
            await ValidateUpdateStockCheckRequest(request);

            var stockCheck = await _unitOfWork.Resolve<StockCheck>().FindAsync(request.StockCheckId);

            ValidateException.ThrowIf(stockCheck.Status == StockCheckStatus.Completed.ToString(),
                "Stock check is completed");

            //Update stock check
            stockCheck = request.Adapt(stockCheck);

            await _unitOfWork.Resolve<StockCheck>().UpdateAsync(stockCheck);

            var result = await _unitOfWork.SaveChangesAsync();

            return Success(result > 0);
        }

        private async Task ValidateUpdateStockCheckRequest(UpdateStockCheckRequest request)
        {
            var isExist = await _unitOfWork.Resolve<Account>().IsExist(request.StaffId);
            ValidateException.ThrowIf(!isExist, "Staff not found");

            await Task.CompletedTask;
        }

        //Assign
        public async Task<ApiResponse<bool>> AssignStockCheck(int stockCheckId, int staffId)
        {
            var stockCheck = await _unitOfWork.Resolve<IStockCheckRepository>().FindAsync(stockCheckId);
            await ValidateAssignStockCheck(stockCheck);

            //Validate staffId
            var staff = await _unitOfWork.Resolve<IAccountRepository>().FindAsync(staffId);
            ValidateException.ThrowIfNull(staff, "Staff is not exist");
            ValidateException.ThrowIf(staff.Role != AccountRole.Staff.ToString(), "StaffId is not a staff");

            //Update stock check
            stockCheck.Log = await UpsertTrackingStockCheck(
                trackingInfo: stockCheck.Log,
                stockCheckStatus: StockCheckStatus.Assigned,
                fullname: staff.Fullname);
            stockCheck.StaffId = staffId;
            //Update stock check status
            if (stockCheck.Priority == StockCheckPriority.Highest.ToString())
            {
                stockCheck.Status = StockCheckStatus.Accepted.ToString();
            }
            else
            {
                stockCheck.Status = StockCheckStatus.Assigned.ToString();
            }

            await _unitOfWork.Resolve<IStockCheckRepository>().UpdateAsync(stockCheck);
            var effRow = await _unitOfWork.SaveChangesAsync();

            return Success(effRow > 0);
        }

        private async Task ValidateAssignStockCheck(StockCheck? stockCheck)
        {
            ValidateException.ThrowIfNull(stockCheck, "StockCheck is not exist");

            ValidateException.ThrowIf(
                !(stockCheck.Status == StockCheckStatus.Todo.ToString()
                || stockCheck.Status == StockCheckStatus.AssignmentDeclined.ToString()
                || stockCheck.Status == StockCheckStatus.Assigned.ToString()
                || stockCheck.Status == StockCheckStatus.Accepted.ToString()),
                "StockCheck Status is invalid");

            await Task.CompletedTask;
        }

        //Accept or Decline Assignment (Staff)
        public async Task<ApiResponse<bool>> AcceptStockCheckAssignmennt(int stockCheckId)
        {
            var stockCheck = await _unitOfWork.Resolve<IStockCheckRepository>().FindAsync(stockCheckId);
            var currentAccountId = _currentAccount.GetAccountId();

            await ValidateAcceptOrRejectStockCheckAssignment(stockCheck, currentAccountId);

            //Update stock check
            stockCheck.Status = StockCheckStatus.Accepted.ToString();
            stockCheck.Log = await UpsertTrackingStockCheck(
                trackingInfo: stockCheck.Log,
                stockCheckStatus: StockCheckStatus.Accepted,
                fullname: _currentAccount.GetFullName());

            await _unitOfWork.Resolve<IStockCheckRepository>().UpdateAsync(stockCheck);
            var effRow = await _unitOfWork.SaveChangesAsync();

            return Success(effRow > 0);
        }

        public async Task<ApiResponse<bool>> DeclineStockCheckAssignmennt(int stockCheckId,
            DeclineStockCheckAssignmentRequest request)
        {
            var stockCheck = await _unitOfWork.Resolve<IStockCheckRepository>().FindAsync(stockCheckId);
            var currentAccountId = _currentAccount.GetAccountId();

            await ValidateAcceptOrRejectStockCheckAssignment(stockCheck, currentAccountId);

            //Update stock check
            stockCheck.Status = StockCheckStatus.AssignmentDeclined.ToString();
            stockCheck.StaffId = null;
            stockCheck.Log = await UpsertTrackingStockCheck(
                trackingInfo: stockCheck.Log,
                stockCheckStatus: StockCheckStatus.AssignmentDeclined,
                fullname: _currentAccount.GetFullName(),
                messsage: request.Reason);


            await _unitOfWork.Resolve<IStockCheckRepository>().UpdateAsync(stockCheck);
            var effRow = await _unitOfWork.SaveChangesAsync();

            //send notification to stockkeeper
            //await _notificationService.SendNotification(new NotificationRequest()
            //{
            //    Body = "Stock check assignment was declined",
            //    Payload = stockCheck.StockCheckId.ToString(),
            //    Title = "Stock check assignment ",
            //    Type = NotificationType.STOCKCHECK_REJECT_ASSIGN.ToString()
            //}, stockCheck.StockkeeperId ?? -1);

            return Success(effRow > 0);
        }

        private async Task ValidateAcceptOrRejectStockCheckAssignment(StockCheck? stockCheck, int staffId)
        {
            ValidateException.ThrowIfNull(stockCheck, "StockCheck is not exist");

            ValidateException.ThrowIf(
                stockCheck.Status != StockCheckStatus.Assigned.ToString() || stockCheck.StaffId == null,
                "Can not accept or reject this stock check assignment, please check status of this assignment again");

            ValidateException.ThrowIf(stockCheck.StaffId != staffId,
                "You was not assigned for this stock check assignment");

            await Task.CompletedTask;
        }

        //Submit full or submit a draft
        public async Task<ApiResponse<bool>> SubmitStockCheck(int stockCheckId, SubmitStockCheckRequest request, bool isDraft)
        {
            var stockCheck = await _unitOfWork.Resolve<StockCheck>().FindAsync(stockCheckId);

            await ValidateSubmitStockCheckRequest(request, stockCheck, isDraft);

            //Update stock check 
            request.Adapt(stockCheck);

            //Update stock check details
            var stockCheckDict = stockCheck.StockCheckDetails
                .Where(x => x.Status == StockCheckDetailStatus.Todo.ToString())
                .ToDictionary(x => x.ProductUnitId, x => x);

            foreach (var checkedProduct in request.CheckedProducts)
            {
                if (stockCheckDict.TryGetValue(checkedProduct.ProductUnitId, out _))
                {// Update existing stock check detail
                    stockCheckDict[checkedProduct.ProductUnitId] =
                        checkedProduct.Adapt(stockCheckDict[checkedProduct.ProductUnitId]);
                    stockCheckDict[checkedProduct.ProductUnitId].Status = StockCheckDetailStatus.Submitted.ToString();
                }
                else
                {// Add new stock check detail
                    var newStockCheckDetail = checkedProduct.Adapt<StockCheckDetail>();
                    newStockCheckDetail.Status = StockCheckDetailStatus.Submitted.ToString();
                    stockCheck.StockCheckDetails.Add(newStockCheckDetail);
                }
            }

            //Update stock check status
            if (isDraft)
                stockCheck.Status = StockCheckStatus.Draft.ToString();
            else
                stockCheck.Status = StockCheckStatus.Submitted.ToString();

            //Update tracking stock check information
            stockCheck.Log = await UpsertTrackingStockCheck(
                trackingInfo: stockCheck.Log,
                stockCheckStatus: Enum.Parse<StockCheckStatus>(stockCheck.Status),
                messsage: request.Comment,
                fullname: _currentAccount.GetFullName()
            );

            await _unitOfWork.Resolve<StockCheck>().UpdateAsync(stockCheck);

            var result = await _unitOfWork.SaveChangesAsync();

            return Success(result > 0);
        }

        private async Task ValidateSubmitStockCheckRequest(SubmitStockCheckRequest request, StockCheck? stockCheck, bool isDrafted)
        {
            ValidateException.ThrowIf(!isDrafted && string.IsNullOrEmpty(request.StaffSignature),
                "StaffSignature is required");

            ValidateException.ThrowIfNull(stockCheck, "Stock check not found");

            ValidateException.ThrowIf(stockCheck.Status != StockCheckStatus.Accepted.ToString()
                && stockCheck.Status != StockCheckStatus.Draft.ToString(),
                "Can not submmit data for this stock check assignment, please check status of this again");

            ValidateException.ThrowIf(request.CheckedProducts.Count() == 0, "Stock check detail is required");

            //if (stockCheck.StockCheckDetails?.Count() > 0)
            //{
            //    var checkProductIds = request.CheckedProducts.Select(x => x.ProductUnitId).ToHashSet();
            //    ValidateException.ThrowIf(
            //        !stockCheck.StockCheckDetails.All(x => checkProductIds.Contains(x.ProductUnitId)),
            //        "The submitted product list is missing some required products.");
            //}

            var checkedProductIds = request.CheckedProducts.Select(x => x.ProductUnitId).ToArray();
            var isExist = await _unitOfWork.Resolve<ProductUnit>().IsExist(checkedProductIds);
            ValidateException.ThrowIf(!isExist, "Product unit not found");

            await Task.CompletedTask;
        }

        //Approve (Stockkeeper) or Reject (Stockkeeper or Staff)
        public async Task<ApiResponse<bool>> ConfirmStockCheck(int stockCheckId, ConfirmStockCheckRequest request)
        {
            var currentAccountId = _currentAccount.GetAccountId();
            var stockCheck = await _unitOfWork.Resolve<IStockCheckRepository>().FindAsync(stockCheckId);

            await ValidateStockCheckIsFinished(stockCheck);

            //Update stock check
            request.Adapt(stockCheck);

            stockCheck.StockkeeperId = currentAccountId;
            stockCheck.Status = StockCheckStatus.Confirmed.ToString();

            stockCheck.Log = await UpsertTrackingStockCheck(
                trackingInfo: stockCheck.Log,
                stockCheckStatus: StockCheckStatus.Confirmed,
                fullname: _currentAccount.GetFullName(),
                messsage: request.Comment);

            //Update stock check details
            var confirmedSet = request.ConfirmedStockCheckDetailIds.ToHashSet();
            foreach (var stockCheckDetail in stockCheck.StockCheckDetails)
            {
                if (confirmedSet.Contains(stockCheckDetail.StockCheckDetailId))
                    stockCheckDetail.Status = StockCheckDetailStatus.Confirmed.ToString();
                else
                    stockCheckDetail.Status = StockCheckDetailStatus.Rejected.ToString();
            }

            await _unitOfWork.Resolve<IStockCheckRepository>().UpdateAsync(stockCheck);

            var effRow = await _unitOfWork.SaveChangesAsync();

            return Success(effRow > 0);
        }

        public async Task ValidateStockCheckIsFinished(StockCheck? stockCheck)
        {
            ValidateException.ThrowIfNull(stockCheck, "Stock check not found");

            ValidateException.ThrowIf(
                !(stockCheck.Status == StockCheckStatus.Submitted.ToString() ||
                  stockCheck.Status == StockCheckStatus.Confirmed.ToString()),
                "Please check status of stock check again");

            ValidateException.ThrowIf(stockCheck.StockCheckDetails.Count == 0, "Stock check is not submitted yet");

            //ValidateException.ThrowIf(
            //    stockCheck.StockCheckDetails.Any(x => x.Status != StockCheckDetailStatus.Submitted.ToString()),
            //    "Some product is not submitted yet");

            if (stockCheck.StockCheckDetails.Count() != 0)
            {
                var isExistProductUnit = await _unitOfWork.Resolve<ProductUnit>().IsExist(
                                       stockCheck.StockCheckDetails.Select(x => x.ProductUnitId).ToArray());
                ValidateException.ThrowIf(!isExistProductUnit, "Product unit not found");
            }

            await Task.CompletedTask;
        }

        public async Task<ApiResponse<bool>> RejectStockCheck(int stockCheckId, RejectStockCheckRequest request)
        {
            var currentAccountId = _currentAccount.GetAccountId();
            var stockCheck = await _unitOfWork.Resolve<IStockCheckRepository>().FindAsync(stockCheckId);

            await ValidateStockCheckIsFinished(stockCheck);

            //Update stock check
            stockCheck.Status = StockCheckStatus.Rejected.ToString();
            stockCheck.Log = await UpsertTrackingStockCheck(
                trackingInfo: stockCheck.Log,
                stockCheckStatus: StockCheckStatus.Rejected,
                fullname: _currentAccount.GetFullName(),
                messsage: request.Reason);

            if (_currentAccount.GetAccountRole() == AccountRole.Stockkeeper.ToString())
                stockCheck.StockkeeperId = currentAccountId;

            await _unitOfWork.Resolve<IStockCheckRepository>().UpdateAsync(stockCheck);
            var effRow = await _unitOfWork.SaveChangesAsync();

            // ISSUE
            //send notification to manager, staff, stockkeeper
            //var receiverIds = new List<int>();
            //receiverIds.Add(stockCheck.StaffId ?? -1);
            //receiverIds.Add(stockCheck.StockkeeperId ?? -1);
            //receiverIds.Add(stockCheck.CreatedBy);
            //var receiverIdsArray = receiverIds.Distinct().ToArray();
            //await _notificationService.SendNotification(new NotificationRequest()
            //{
            //    Body = "Stock check was rejected",
            //    Payload = stockCheck.StockCheckId.ToString(),
            //    Title = "Stock check rejection",
            //    Type = NotificationType.STOCKCHECK_REJECT.ToString()
            //}, receiverIdsArray);

            return Success(effRow > 0);
        }

        //Complete (Manager)
        public async Task<ApiResponse<bool>> CompleteStockCheck(int stockCheckId, CompleteStockCheckRequest request)
        {
            var stockCheck = await _unitOfWork.Resolve<IStockCheckRepository>().FindAsync(stockCheckId);

            await ValidateStockCheckIsFinished(stockCheck);

            //Update stock check
            stockCheck.DocumentLink = request.DocumentLink;
            stockCheck.Status = StockCheckStatus.Completed.ToString();
            stockCheck.Log = await UpsertTrackingStockCheck(
                trackingInfo: stockCheck.Log,
                stockCheckStatus: StockCheckStatus.Completed,
                fullname: _currentAccount.GetFullName(),
                messsage: request.Comment);

            await _unitOfWork.Resolve<IStockCheckRepository>().UpdateAsync(stockCheck);
            var effRow = await _unitOfWork.SaveChangesAsync();

            //await _stockBalanceService.BalanceStockByStockCheck(stockCheckId);

            return Success(effRow > 0);
        }

        //Tracking Stock Check
        private async Task<string> UpsertTrackingStockCheck(
            string? trackingInfo,
            StockCheckStatus stockCheckStatus,
            string fullname,
            string? messsage = null
        )
        {
            var trackingStockChecks = JsonConvert.DeserializeObject<List<StockCheckLogDto>>(trackingInfo ?? string.Empty);

            if (trackingStockChecks == null)
                trackingStockChecks = new List<StockCheckLogDto>();

            trackingStockChecks.Add(new StockCheckLogDto
            {
                Title = TrackingStockCheckTitleDict.GetTitle(stockCheckStatus, fullname),
                Datetime = DateTime.Now.ToString("HH:mm yyyy-MM-dd"),
                Content = messsage,
                StockCheckStatus = stockCheckStatus.ToString()
            });

            trackingInfo = JsonConvert.SerializeObject(trackingStockChecks);

            await Task.CompletedTask;

            return trackingInfo;
        }

        //Delete
        public async Task<ApiResponse<bool>> DeleteStockCheck(int stockCheckId)
        {
            var isExist = await _unitOfWork.Resolve<StockCheck>().IsExist(stockCheckId);
            ValidateException.ThrowIf(!isExist, "Stock check not found");

            await _unitOfWork.Resolve<StockCheck>().DeleteAsync(stockCheckId);

            var result = await _unitOfWork.SaveChangesAsync();

            return Success(result > 0);
        }


    }
}