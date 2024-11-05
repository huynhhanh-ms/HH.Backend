using HH.Application.Common;
using HH.Domain.Common;
using HH.Domain.Dto.WeighingHistory;
using HH.Domain.Models;
using HH.Domain.Repositories;
using HH.Domain.Repositories.Common;
using HH.Persistence.Repositories.Common;
using Mapster;

namespace HH.Application.Services
{
    public class WeighingHistoryService : BaseService, IWeighingHistoryService
    {
        public WeighingHistoryService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<ApiResponse<bool>> Create(WeighingHistoryCreateDto request)
        {
            var WeighingHistory = request.Adapt<WeighingHistory>();

            await _unitOfWork.Resolve<IWeighingHistoryRepository>().CreateAsync(WeighingHistory);
            await _unitOfWork.SaveChangesAsync();

            return Success<bool>("Tạo thành công");
        }

        public async Task<ApiResponse<bool>> Delete(int id)
        {
            await _unitOfWork.Resolve<IWeighingHistoryRepository>().DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();
            return Success<bool>("Xóa thành công");
        }

        public async Task<ApiResponse<WeighingHistoryGetDto>> Get(int id)
        {
            var WeighingHistory = await _unitOfWork.Resolve<IWeighingHistoryRepository>().FindAsync(id);
            var WeighingHistoryGetDto = WeighingHistory.Adapt<WeighingHistoryGetDto>();

            if (WeighingHistory == null)
                return Failed<WeighingHistoryGetDto>("Không tìm thấy");

            return Success<WeighingHistoryGetDto>(WeighingHistoryGetDto);
        }

        public async Task<ApiResponse<List<WeighingHistoryGetDto>>> Gets(WeighingHistorySearch request)
        {
            var WeighingHistorys = await _unitOfWork.Resolve<IWeighingHistoryRepository>().GetsInDateRange(request.StartDate, request.EndDate);
            WeighingHistorys = WeighingHistorys.OrderBy(item => item.Id);
            var WeighingHistoryGetDtos = WeighingHistorys.Adapt<List<WeighingHistoryGetDto>>();

            if (WeighingHistorys == null)
                return Failed<List<WeighingHistoryGetDto>>("Không tìm thấy");
            return Success<List<WeighingHistoryGetDto>>(WeighingHistoryGetDtos);
        }

        public async Task<ApiResponse<bool>> Update(WeighingHistoryUpdateDto request)
        {
            var WeighingHistory = await _unitOfWork.Resolve<IWeighingHistoryRepository>().FindAsync(request.Id);
            if (WeighingHistory == null)
                return Failed<bool>("Không tìm thấy");
            var updatedObject = request.Adapt(WeighingHistory);

            await _unitOfWork.Resolve<WeighingHistory>().UpdateAsync(updatedObject);
            await _unitOfWork.SaveChangesAsync();

            return Success<bool>("Cập nhật thành công");
        }
    }
}
