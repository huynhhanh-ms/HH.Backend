using PI.Domain.Common.PagedLists;
using PI.Domain.Dto.Product;
using PI.Domain.Dto.Unit;
using PI.Domain.Models;
using PI.Domain.Repositories;
using PI.Domain.Repositories.Common;

namespace PI.Application.Service
{
    public class ProductUnitService : BaseService, IProductUnitService
    {
        private readonly IProductService _productService;

        public ProductUnitService(IUnitOfWork unitOfWork, IProductService productService) : base(unitOfWork)
        {
            _productService = productService;
        }

        //get all product unit
        public async Task<PagingApiResponse<ProductUnitResponse>> SearchProductUnit(string keySearch,
            PagingQuery pagingQuery, string orderBy)
        {
            try
            {
                var responses = await _unitOfWork.Resolve<IProductUnitRepository>()
                    .SearchProductUnit(keySearch, pagingQuery, orderBy);

                foreach (var item in responses)
                {
                    //get stock quantity
                    // item.Product = await _productService.GetProductDetail(item.Product.ProductId);
                    //item.StockQuantity =  await ConvertQuantity(item.ProductUnitId);

                    //get attribute mapping
                    // var productAttributeMappings = await _unitOfWork
                    //     .Resolve<ProductAttributeMapping, IProductAttributeMapRepository>()
                    //     .FindByProductId(item.Product.ProductId);
                    // item.Product.ProductAttributeMappings =
                    //     productAttributeMappings.Adapt<List<AttributeMappingResponse>>();
                }

                return Success(responses);
            }
            catch (Exception e)
            {
                return PagingFailed<ProductUnitResponse>(e.GetExceptionMessage());
            }
        }

        //get product unit with sku code
        public async Task<ApiResponse<ProductUnitResponse>> GetProductUnitBySkuCode(string skuCode)
        {
            try
            {
                var productUnit = await _unitOfWork.Resolve<ProductUnit>()
                    .FindAsync(p => p.SkuCode == skuCode);
                if (productUnit == null)
                {
                    throw new ArgumentException("Product unit not found");
                }

                var response = productUnit.Adapt<ProductUnitResponse>();
                var unit = await _unitOfWork.Resolve<Unit>().FindAsync(p => p.UnitId == productUnit.UnitId);
                response.Unit = unit.Adapt<UnitResponse>();
                //response.StockQuantity = await ConvertQuantity(productUnit.ProductUnitId);
                return Success(response);
            }
            catch (Exception e)
            {
                return Failed<ProductUnitResponse>(e.GetExceptionMessage(), HttpStatusCode.BadRequest);
            }
        }

        public async Task<PagingApiResponse<ProductLotResponse>> GetProductLot(string skuCode,
            PagingQuery pagingQuery, string orderBy, LotStatus? lotStatus)
        {
            try
            {
                var productUnit = await _unitOfWork.Resolve<ProductUnit>().FindAsync(p => p.SkuCode == skuCode);
                if (productUnit == null)
                {
                    throw new ArgumentException("Product unit not found");
                }

                var productLots = await _unitOfWork.Resolve<ILotRepository>()
                    .SearchAsync(productUnit.ProductUnitId, pagingQuery, orderBy, lotStatus);

                return Success(productLots);
            }
            catch (Exception e)
            {
                return PagingFailed<ProductLotResponse>(e.GetExceptionMessage());
            }
        }

        public async Task<PagingApiResponse<ProductHistoryResponse>> GetProductHistory(string skuCode,
            SearchProductHistory searchReq)
        {
            try
            {
                var productUnit = await _unitOfWork.Resolve<ProductUnit>().FindAsync(p => p.SkuCode == skuCode);
                if (productUnit == null)
                {
                    return PagingFailed<ProductHistoryResponse>("Product unit not found", HttpStatusCode.BadRequest);
                }

                var shipments = await _unitOfWork.Resolve<IShipmentRepository>()
                    .SearchShipmentAsync(productUnit.ProductUnitId, searchReq);

                return Success(shipments);
            }
            catch (Exception e)
            {
                return PagingFailed<ProductHistoryResponse>(e.GetExceptionMessage());
            }
        }

        public async Task<ApiResponse<string>> DeleteProductUnit(int productUnitId)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                await _unitOfWork.Resolve<ProductUnit>().DeleteAsync(productUnitId);
                await _unitOfWork.SaveChangesAsync();
                return Success<string>("Success delete product unit");
            }
            catch (Exception e)
            {
                await _unitOfWork.RollbackTransactionAsync();
                return Failed<string>(e.GetExceptionMessage());
            }
        }

        //statistic product unit
        //for 12 months
        public async Task<ApiResponse<IEnumerable<ProductStatisticResponse>>> StatisticProductUnit(string skuCode,
            SearchProductStatisticReq req)
        {
            try
            {
                var isExist = await _unitOfWork.Resolve<IProductUnitRepository>().IsExist(skuCode);

                if (!isExist)
                    throw new ArgumentException("Product unit not found");

                IEnumerable<ProductStatisticResponse> statistics = new List<ProductStatisticResponse>();

                var currentYear = DateTime.Now.Year;
                var currentMonth = DateTime.Now.Month;
                var currentQuarter = (DateTime.Now.Month - 1) / 3 + 1;
                switch (req.By)
                {

                    case ProductStatistic.MONTH:


                        for (var i = 0; i < 12; i++)
                        {
                            var startDate = new DateTime(currentYear, currentMonth, 1);
                            var endDate = startDate.AddMonths(1).AddDays(-1);
                            var total = await _unitOfWork.Resolve<IShipmentRepository>()
                                .SearchProductStatisticAsync(skuCode, startDate, endDate, req.By, req.Type);
                            var label = startDate.ToString("MMM yyyy");
                            statistics = statistics.Append(new ProductStatisticResponse
                            {
                                Label = label,
                                Value = total
                            });
                            currentMonth--;
                            if (currentMonth == 0)
                            {
                                currentMonth = 12;
                                currentYear--;
                            }
                        }
                        break;

                    case ProductStatistic.QUARTER:
                        for (var i = 0; i < 4; i++)
                        {
                            var fromDate = new DateTime(currentYear, (currentQuarter - 1) * 3 + 1, 1);
                            var toDate = fromDate.AddMonths(3).AddDays(-1);

                            var total = await _unitOfWork.Resolve<IShipmentRepository>()
                                .SearchProductStatisticAsync(skuCode, fromDate, toDate, req.By, req.Type);
                            var label = string.Format("Q{0} {1}", currentQuarter, currentYear);
                            statistics = statistics.Append(new ProductStatisticResponse
                            {
                                Label = label,
                                Value = total
                            });

                            currentQuarter--;
                            if (currentQuarter == 0)
                            {
                                currentQuarter = 4;
                                currentYear--;
                            }
                        }
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                return Success(statistics);

            }
            catch (Exception e)
            {
                return Failed<IEnumerable<ProductStatisticResponse>>(e.GetExceptionMessage());
            }
        }

        private async Task<long> GetStockQuantity(int productUnitId)
        {
            var productStock = await _unitOfWork.Resolve<ProductStock>()
                .FindListAsync( p => p.ProductUnitId == productUnitId);
            return productStock.Sum(p => p.StockQuantity);
        }

        private async Task<double> ConvertQuantity(int productUnitId)
        {
            double sum = 0;
            //check if product unit is base unit
            var productUnit = await _unitOfWork.Resolve<ProductUnit>().FindAsync(p => p.ProductUnitId == productUnitId);
            if (productUnit.ParentId == null)
            {
                var childProductUnits = await _unitOfWork.Resolve<ProductUnit>().FindListAsync(p => p.ParentId == productUnitId);
                foreach (var childProductUnit in childProductUnits)
                {
                    sum += (double) (await GetStockQuantity(childProductUnit.ProductUnitId) / double.Parse(childProductUnit.ConversionValue.ToString()));
                }
                sum += await GetStockQuantity(productUnitId);
            } else {
                var parentProductUnit = await _unitOfWork.Resolve<ProductUnit>().FindAsync(p => p.ProductUnitId == productUnit.ParentId);
                var parentStockQuantity = await GetStockQuantity(parentProductUnit.ProductUnitId);
                sum += parentStockQuantity * int.Parse(productUnit.ConversionValue.ToString());
                sum += await GetStockQuantity(productUnitId);
            }
            return sum;
        }
    }
}