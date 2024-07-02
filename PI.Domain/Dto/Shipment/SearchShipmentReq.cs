using PI.Domain.Common;
using PI.Domain.Common.PagedLists;

namespace PI.Domain.Dto.Shipment
{
    public class SearchShipmentReq : SearchBaseRequest
    {
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }
}