using PI.Domain.Dto.Manufacturer;
using PI.Domain.Models;
using PI.Domain.Repositories.Common;

namespace PI.Application.Service
{
    public class ManufacturerService : BaseService, IManufacturerService
    {
        public ManufacturerService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<ApiResponse<string>> Create(CreateManufacturerRequest request)
        {
            var manufacturer = request.Adapt<Manufacturer>();
            await _unitOfWork.Resolve<Manufacturer>().CreateAsync(manufacturer);
            await _unitOfWork.SaveChangesAsync();
            return Success<string>("Create manufacturer successfully!");
        }

        //get all manufacturer
        public async Task<ApiResponse<IEnumerable<ManufacturerResponse>>> GetAll()
        {
            var manufacturers = await _unitOfWork.Resolve<Manufacturer>().GetAllAsync();
            var response = manufacturers.Adapt<IEnumerable<ManufacturerResponse>>();
            return Success(response);
        }
    }
}