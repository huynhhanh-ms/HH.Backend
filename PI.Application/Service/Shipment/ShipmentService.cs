using PI.Domain.Dto.Shipment;
using PI.Domain.Infrastructure.Auth;
using PI.Domain.Models;
using PI.Domain.Repositories;
using PI.Domain.Repositories.Common;

namespace PI.Application.Service
{
    public class ShipmentService : BaseService, IShipmentService
    {
        private readonly ICurrentAccount _currentAccount;
        private readonly IExportRequestService _exportReqService;

        public ShipmentService(IUnitOfWork unitOfWork, ICurrentAccount currentAccount,
            IExportRequestService exportReqService) : base(unitOfWork)
        {
            _currentAccount = currentAccount;
            _exportReqService = exportReqService;
        }

        //create
        public async Task<ApiResponse<string>> CreateImportShipment(ImportShipmentRequest request)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                {
                    //check if distributor exists
                    var distributor = await _unitOfWork.Resolve<Distributor>()
                        .FindAsync(d => d.DistributorId == request.DistributorId);

                    //check if create from import request 
                    ImportRequest? importRequest = null;
                    if (request.ImportRequestId != null)
                    {
                        importRequest = await _unitOfWork.Resolve<ImportRequest>()
                            .FindAsync(e => e.ImportRequestId == request.ImportRequestId);

                        if (importRequest == null)
                        {
                            throw new ArgumentException("Import request not found");
                        }
                    }

                    if (distributor == null)
                    {
                        throw new ArgumentException("Distributor not found");
                    }

                    //handle list product
                    var shipment = request.Adapt<Shipment>();

                    shipment.ShipmentType = ShipmentType.Import.ToString();
                    shipment.TotalUnitProductQuantity = request.ProductLots.Sum(p => p.Quantity);
                    shipment.TotalPrice = request.ProductLots.Sum(p => p.Cost * p.Quantity);
                    shipment.ShipmentDate = DateTime.Now;
                    shipment.ImportRequestId = request.ImportRequestId;
                    shipment.CreatedAt = DateTime.Now;
                    shipment.CreatedBy = _currentAccount.GetAccountId();
                    shipment.UpdatedAt = DateTime.Now;
                    shipment.UpdatedBy = _currentAccount.GetAccountId();

                    await _unitOfWork.Resolve<Shipment>().CreateAsync(shipment);
                    await _unitOfWork.SaveChangesAsync();

                    if (importRequest != null)
                    {
                        //update import request Status
                        importRequest.ImportRequestStatus = ImportRequestStatus.Accepted.ToString().ToLower();
                        await _unitOfWork.Resolve<ImportRequest>().UpdateAsync(importRequest);
                        await _unitOfWork.SaveChangesAsync();
                    }

                    await HandleImportProductLot(request.ProductLots, shipment.ShipmentId);
                }
                await _unitOfWork.CommitTransactionAsync();
                return Success<string>("Shipment created successfully!");
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                return Failed<string>(ex.GetExceptionMessage());
            }
        }

        //search shipment
        public async Task<PagingApiResponse<ImportShipmentResponse>> SearchImportShipment(SearchShipmentReq searchReq)
        {
            try
            {
                var responses = await _unitOfWork.Resolve<IShipmentRepository>()
                    .SearchImportShipmentAsync(searchReq.KeySearch, searchReq.FromDate, searchReq.ToDate,
                                        searchReq.PagingQuery, searchReq.OrderBy);

                return Success(responses);
            }
            catch (Exception ex)
            {
                return PagingFailed<ImportShipmentResponse>(ex.GetExceptionMessage());
            }
        }

        public async Task<ApiResponse<string>> CreateExportShipment(ExportShipmentRequest request)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                {
                    //check if export request exists
                    var isExportRequestExist = await _unitOfWork.Resolve<ExportRequest>()
                        .IsExist(request.ExportRequestId);

                    if (!isExportRequestExist)
                        return Failed<string>("Export request not found", HttpStatusCode.BadRequest);

                    var shipment = request.Adapt<Shipment>();

                    shipment.ShipmentType = ShipmentType.Export.ToString();
                    shipment.TotalUnitProductQuantity = request.ProductLots.Sum(p => p.ExportQuantity);
                    shipment.ExportRequestId = request.ExportRequestId;
                    shipment.ShipmentDate = DateTime.Now;

                    await _unitOfWork.Resolve<Shipment>().CreateAsync(shipment);
                    await _unitOfWork.SaveChangesAsync();

                    var (completeResult, message) = await _exportReqService.CompeleteExportRequest(request.ExportRequestId);
                    if (!completeResult)
                        return Failed<string>(message, HttpStatusCode.BadRequest);

                    await HandleExportProductLot(request.ProductLots, shipment.ShipmentId);
                }
                await _unitOfWork.CommitTransactionAsync();
                return Success<string>("Shipment created successfully!");
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                return Failed<string>(ex.GetExceptionMessage());
            }
        }

        public async Task<PagingApiResponse<ExportShipmentResponse>> SearchExportShipment(SearchShipmentReq searchReq)
        {
            try
            {
                var responses = await _unitOfWork.Resolve<IShipmentRepository>()
                    .SearchExportShipmentAsync(searchReq.KeySearch, searchReq.FromDate, searchReq.ToDate,
                                        searchReq.PagingQuery, searchReq.OrderBy);
                return Success(responses);
            }
            catch (Exception ex)
            {
                return PagingFailed<ExportShipmentResponse>(ex.GetExceptionMessage());
            }
        }


        private async Task HandleImportProductLot(List<ImportProductLotRequest> productLots, int shipmentId)
        {
            foreach (var productLot in productLots)
            {
                //check if product unit exists
                var productUnit = await _unitOfWork.Resolve<ProductUnit>()
                    .FindAsync(p => p.SkuCode == productLot.SkuCode);

                if (productUnit == null)
                {
                    throw new ArgumentException("Product unit not found");
                }

                //TODO: check valid lot
                var lotExists = await _unitOfWork.Resolve<Lot>()
                    .FindAsync(p => p.LotCode == productLot.LotCode);

                if (lotExists != null)
                {
                    throw new ArgumentException("Lot code already exists");
                }

                //create lot
                var lot = new Lot()
                {
                    ProductUnitId = productUnit.ProductUnitId,
                    LotCode = productLot.LotCode,
                    ManufacturingDate = productLot.ManufacturingDate,
                    ExpirationDate = productLot.ExpirationDate,
                    CreatedBy = _currentAccount.GetAccountId(),
                    CreatedAt = DateTime.Now,
                    UpdatedBy = _currentAccount.GetAccountId(),
                    UpdatedAt = DateTime.Now
                };
                if (lot.ManufacturingDate > lot.ExpirationDate)
                {
                    throw new ArgumentException("Manufacturing date cannot be greater than expiration date");
                }

                await _unitOfWork.Resolve<Lot>().CreateAsync(lot);
                await _unitOfWork.SaveChangesAsync();

                #region  db auto update product stock 2024-03-04 : khoabd
                //update product product stock
                //var productStock = await _unitOfWork.Resolve<ProductStock>()
                //    .FindAsync(p => p.ProductUnitId == productUnit.ProductUnitId);

                //create product stock if not exists 
                //if (productStock == null)
                //{
                //    productStock = new ProductStock
                //    {
                //        ProductUnitId = productUnit.ProductUnitId,
                //        StockQuantity = productLot.Quantity
                //    };
                //    await _unitOfWork.Resolve<ProductStock>().CreateAsync(productStock);
                //}
                //else
                //{
                //    productStock.StockQuantity += productLot.Quantity;
                //    await _unitOfWork.Resolve<ProductStock>().UpdateAsync(productStock);
                //}
                #endregion

                await _unitOfWork.SaveChangesAsync();

                //create shipment detail
                var shipmentDetail = new ShipmentDetail
                {
                    ProductUnitId = productUnit.ProductUnitId,
                    LotId = lot.LotId,
                    Quantity = productLot.Quantity,
                    Cost = productLot.Cost,
                    Price = productLot.Cost * productLot.Quantity, // ???
                    ShipmentId = shipmentId
                };

                await _unitOfWork.Resolve<ShipmentDetail>().CreateAsync(shipmentDetail);

                await _unitOfWork.SaveChangesAsync();
            }
        }

        private async Task HandleExportProductLot(List<ExportProductLotRequest> productLots, int shipmentId)
        {
            foreach (var productLot in productLots)
            {
                //check if product unit exists
                var productUnit = await _unitOfWork.Resolve<ProductUnit>()
                    .FindAsync(p => p.SkuCode == productLot.SkuCode);

                if (productUnit == null)
                {
                    throw new ArgumentException("Product unit not found");
                }

                //check if lot exists
                var lot = await _unitOfWork.Resolve<Lot>()
                    .FindAsync(p => p.LotCode == productLot.LotCode);
                if (lot == null)
                    throw new ArgumentException("Lot not found");

                //create shipment detail
                var shipmentDetail = new ShipmentDetail
                {
                    ProductUnitId = productUnit.ProductUnitId,
                    LotId = lot.LotId,
                    Quantity = productLot.ExportQuantity,
                    ShipmentId = shipmentId
                };
                await _unitOfWork.Resolve<ShipmentDetail>().CreateAsync(shipmentDetail);
                await _unitOfWork.SaveChangesAsync();
            }
        }

        public async Task CreateImportShipment4Balancing(List<StockCheckDetail> stockCheckDetails)
        {
            try
            {
                //create import shipment in shipment
                await _unitOfWork.BeginTransactionAsync();
                {
                    var importShipment = new Shipment
                    {
                        ShipmentType = ShipmentType.ImportBalance.ToString(),
                        TotalUnitProductQuantity = stockCheckDetails.Sum(x => x.ActualQuantity)
                    };
                    await _unitOfWork.Resolve<Shipment>().CreateAsync(importShipment);
                    await _unitOfWork.SaveChangesAsync();
                    //handle shipment detail
                    await CreateShipmentDetail4Balancing(stockCheckDetails, importShipment.ShipmentId);
                    //handle product stock
                }
                await _unitOfWork.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error when handle stock check import shipment", ex);
            }
        }

        public async Task CreateExportShipment4Balancing(List<StockCheckDetail> stockCheckDetails)
        {
            try
            {
                //create export shipment in shipment
                await _unitOfWork.BeginTransactionAsync();
                {
                    var exportShipment = new Shipment
                    {
                        ShipmentType = ShipmentType.ExportBalance.ToString(),
                        TotalUnitProductQuantity = stockCheckDetails.Sum(x => x.ActualQuantity)
                    };
                    await _unitOfWork.Resolve<Shipment>().CreateAsync(exportShipment);
                    await _unitOfWork.SaveChangesAsync();
                    //handle shipment detail
                    await CreateShipmentDetail4Balancing(stockCheckDetails, exportShipment.ShipmentId);
                    //handle product stock
                }
                await _unitOfWork.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error when handle stock check export shipment", ex);
            }
        }

        //handle shipment detail
        private async Task CreateShipmentDetail4Balancing(List<StockCheckDetail> stockCheckDetails, int shipmentId)
        {
            foreach (var item in stockCheckDetails)
            {
                var productUnit = await _unitOfWork.Resolve<ProductUnit>().FindAsync(item.ProductUnitId);

                //create new shipment detail
                var shipmentDetail = new ShipmentDetail()
                {
                    ProductUnitId = item.ProductUnitId,
                    //abs to get positive number
                    Quantity = Math.Abs(item.ActualQuantity - item.EstimatedQuantity ?? 0),
                    ShipmentId = shipmentId
                };
                await _unitOfWork.Resolve<ShipmentDetail>().CreateAsync(shipmentDetail);
                await _unitOfWork.SaveChangesAsync();
            }
        }
    }
}