using PI.Domain.Common;
using PI.Domain.Common.PagedLists;
using PI.Domain.Dto.Manufacturer;

namespace PI.Application.Service
{
    public interface IManufacturerService
    {
        Task<ApiResponse<string>> Create(CreateManufacturerRequest request);

        Task<ApiResponse<IEnumerable<ManufacturerResponse>>> GetAll();
    }
}