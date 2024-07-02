using PI.Domain.Dto.StockCheck;
using PI.Domain.Models;
using PI.Domain.Repositories;
using static PI.Domain.Enums.StockCheckEnum;

namespace PI.Application.Service.StockBalance
{
    public class StockBalanceService : BaseService, IStockBalanceService
    {
        private readonly IShipmentService _shipmentService;
        public StockBalanceService(IUnitOfWork unitOfWork, IShipmentService shipmentService) : base(unitOfWork)
        {
            _shipmentService = shipmentService;
        }
        
        public async Task<PagingApiResponse<SearchStockCheckResponse>> SearchStockBalance(SearchStockCheckRequest request)
        {
            var stockChecks = await _unitOfWork.Resolve<IStockCheckRepository>()
                .SearchStockBalanceAsync(request);
            return Success(stockChecks);
        }
        
        public async Task<ApiResponse<bool>> BalanceStockByStockCheck(int stockCheckId)
        {
            var stockCheck = await _unitOfWork.Resolve<IStockCheckRepository>().FindAsync(stockCheckId);

            ValidateException.ThrowIfNull(stockCheck, "Stock check not found");

            ValidateException.ThrowIf(stockCheck.Status != StockCheckStatus.Completed.ToString() || stockCheck.IsUsedForBalancing == true, 
                "Stock check have been used for stock balancing");

            //Update stock balance
            stockCheck.IsUsedForBalancing = true;
            await _unitOfWork.Resolve<IStockCheckRepository>().UpdateAsync(stockCheck);

            //check in stock detail and update stock balances
            var stockCheckDetails = stockCheck.StockCheckDetails;
            foreach (var item in stockCheckDetails)
            {
                //set export stock when stock check is used for balancing and stock check detail is submitted and quantity is < actual quantity

                var exportStockList = new List<StockCheckDetail>();
                var importStockList = new List<StockCheckDetail>();
                if (item.Status == StockCheckEnum.StockCheckDetailStatus.Confirmed.ToString() &&
                    item.EstimatedQuantity > item.ActualQuantity)
                {
                    exportStockList.Add(item);
                }
                else if (item.Status == StockCheckEnum.StockCheckDetailStatus.Confirmed.ToString() &&
                         item.EstimatedQuantity < item.ActualQuantity)
                {
                    importStockList.Add(item);
                }

                if (exportStockList.Count > 0)
                {
                    await _shipmentService.CreateExportShipment4Balancing(exportStockList);
                }

                if (importStockList.Count > 0)
                {
                    await _shipmentService.CreateImportShipment4Balancing(importStockList);
                }

            }

            var effRow = await _unitOfWork.SaveChangesAsync();

            return Success(effRow > 0);
        }
        
        
    }
}