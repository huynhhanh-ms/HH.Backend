using PI.Domain.Dto.Lot;
using PI.Domain.Infrastructure.Auth;
using PI.Domain.Models;
using PI.Domain.Repositories.Common;

namespace PI.Application.Service
{
    public class LotService : BaseService, ILotService
    {
        private readonly ICurrentAccount _currentAccount;

        public LotService(IUnitOfWork unitOfWork, ICurrentAccount currentAccount) : base(unitOfWork)
        {
            _currentAccount = currentAccount;
        }

        //create lot
        public async Task Create(CreateLotRequest request)
        {
            var lot = request.Adapt<Lot>();
            lot.CreatedBy = _currentAccount.GetAccountId();
            lot.CreatedAt = DateTime.Now;
            lot.UpdatedBy = _currentAccount.GetAccountId();
            lot.UpdatedAt = DateTime.Now;
            lot.LotStatus = LotStatus.ACTIVE.ToString().ToLower();
            if (request.ManufacturingDate > request.ExpirationDate)
            {
                throw new Exception("Manufacturing date cannot be greater than expiration date");
            }

            await _unitOfWork.Resolve<Lot>().CreateAsync(lot);
            await _unitOfWork.SaveChangesAsync();
        }


    }
}