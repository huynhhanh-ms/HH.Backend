using PI.Domain.Common;
using PI.Domain.Enums;

namespace PI.Domain.Dto.Account
{
    public class SearchAccountReq : SearchBaseRequest
    {
        public bool? IsFree { get; set; }
        public string? Role { get; set; }
    }
}