using PI.Domain.Common;
using PI.Domain.Common.PagedLists;
using PI.Domain.Dto.Shipment;
using PI.Domain.Models;

namespace PI.Application.Service
{
    public interface IShipmentService
    {
        Task<ApiResponse<string>> CreateImportShipment(ImportShipmentRequest request);
        Task<PagingApiResponse<ImportShipmentResponse>> SearchImportShipment(SearchShipmentReq searchReq);
        Task<ApiResponse<string>> CreateExportShipment(ExportShipmentRequest request);
        Task<PagingApiResponse<ExportShipmentResponse>> SearchExportShipment(SearchShipmentReq searchReq);
        Task CreateImportShipment4Balancing(List<StockCheckDetail> stockCheckDetails);
        Task CreateExportShipment4Balancing(List<StockCheckDetail> stockCheckDetails);
    }
}