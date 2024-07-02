using PI.Domain.Dto.Lot;

namespace PI.Application.Service
{
    public interface ILotService
    {
        Task Create(CreateLotRequest request);
    }
}