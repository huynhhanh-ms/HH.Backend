using HH.Application.Common;
using HH.Domain.Common;
using HH.Domain.Dto.WeighingHistory;
using HH.Domain.Models;
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

            await _unitOfWork.Resolve<WeighingHistory>().CreateAsync(WeighingHistory);
            await _unitOfWork.SaveChangesAsync();

            return Success<bool>("Tạo thành công");
        }

        public async Task<ApiResponse<bool>> Delete(int id)
        {
            await _unitOfWork.Resolve<WeighingHistory>().DeleteAsync(id);
            return Success<bool>("Xóa thành công");
        }

        public async Task<ApiResponse<WeighingHistory>> Get(int id)
        {
            var WeighingHistory = await _unitOfWork.Resolve<WeighingHistory>().FindAsync(id);

            if (WeighingHistory == null)
                return Failed<WeighingHistory>("Không tìm thấy");

            return Success<WeighingHistory>(WeighingHistory);
        }

        public async Task<ApiResponse<List<WeighingHistory>>> Gets(SearchBaseRequest request)
        {
            var WeighingHistorys = await _unitOfWork.Resolve<WeighingHistory>().GetAllAsync();
            WeighingHistorys = WeighingHistorys.OrderBy(WeighingHistory => WeighingHistory.Id);

            if (WeighingHistorys == null)
                return Failed<List<WeighingHistory>>("Không tìm thấy");
            return Success<List<WeighingHistory>>(WeighingHistorys.ToList());
        }

        public async Task<ApiResponse<bool>> Update(WeighingHistory request)
        {
            var WeighingHistory = await _unitOfWork.Resolve<WeighingHistory>().FindAsync(request.Id);
            if (WeighingHistory == null)
                return Failed<bool>("Không tìm thấy");

            await _unitOfWork.Resolve<WeighingHistory>().UpdateAsync(request);
            return Success<bool>("Cập nhật thành công");
        }
    }
}
