using PI.Domain.Common;
using PI.Domain.Common.PagedLists;
using PI.Domain.Dto.Medicine;

namespace PI.Application.Service
{
    public interface IMedicineService
    {
        Task CreateMedicine();

        Task<PagingApiResponse<MedicineResponse>> SearchMedicineAsync(string keySearch,
            PagingQuery pagingQuery, string orderBy);

    }
}