using PI.Domain.Enums;

namespace PI.Domain.Dto.Account
{
    public class UpdateAccountStatusRequest
    {
        public AccountStatus Status { get; set; } = AccountStatus.Active;
    }
}