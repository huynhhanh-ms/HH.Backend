using PI.Domain.Common;

namespace PI.Domain.Dto.Account
{
    public class SearchStaffReq : SearchBaseRequest
    {
        public bool? IsFree { get; set; }
    }
}