using HH.Application.Common;
using HH.Domain.Common;
using HH.Domain.Dto;
using HH.Domain.Models;
using HH.Domain.Repositories.Common;
using HH.Persistence.Repositories.Common;
using Mapster;
using Microsoft.EntityFrameworkCore.Storage.Internal;

namespace HH.Application.Services
{
    public class TankHistoryService : BaseService, ITankHistoryService
    {
        public TankHistoryService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<ApiResponse<bool>> Create(TankHistoryCreateDto request)
        {
            var TankHistory = request.Adapt<TankHistory>();

            await _unitOfWork.Resolve<TankHistory>().CreateAsync(TankHistory);
            await _unitOfWork.SaveChangesAsync();

            return Success<bool>("Tạo thành công");
        }

        public async Task<ApiResponse<bool>> Delete(int id)
        {
            await _unitOfWork.Resolve<TankHistory>().DeleteAsync(id);
            return Success<bool>("Xóa thành công");
        }

        public async Task<ApiResponse<TankHistory>> Get(int id)
        {
            var TankHistory = await _unitOfWork.Resolve<TankHistory>().FindAsync(id);

            if (TankHistory == null)
                return Failed<TankHistory>("Không tìm thấy");

            return Success<TankHistory>(TankHistory);
        }

        public async Task<ApiResponse<List<TankHistory>>> Gets(SearchBaseRequest request)
        {
            var TankHistorys = await _unitOfWork.Resolve<TankHistory>().GetAllAsync();
            TankHistorys = TankHistorys.OrderBy(TankHistory => -TankHistory.Id);

            if (TankHistorys == null)
                return Failed<List<TankHistory>>("Không tìm thấy");
            return Success<List<TankHistory>>(TankHistorys.ToList());
        }

        public async Task<ApiResponse<bool>> Save(TankHistorySaveDto request)
        {

            var TankHistory = request.TankHistories.Adapt<List<TankHistory>>();
            if (TankHistory == null)
                return Failed<bool>("Không tìm thấy");

            var uniqueTankHistories = TankHistory
            .GroupBy(t => t.TankId)
            .Select(g => g.First())
            .ToList();
            
            if (uniqueTankHistories?.Count != TankHistory.Count)
                return Failed<bool>("Mảng bồn không phân biệt lẫn nhau");

            //Update Tank from TankHistory List
            var Tank = await _unitOfWork.Resolve<Tank>().GetAllAsync();
            foreach (var item in Tank)
            {
                var matchedItem = TankHistory.FirstOrDefault(x => x.TankId == item.Id);

                if (matchedItem != null)
                {
                    var tmpVolume = item.CurrentVolume;

                    //item.Name = matchedItem.Name; 
                    item.CurrentVolume = matchedItem.CurrentVolume;

                    matchedItem.CurrentVolume = tmpVolume;
                    matchedItem.CreatedAt = DateTime.Now;
                }
            }

            await _unitOfWork.Resolve<TankHistory>().CreateAsync(TankHistory.ToArray());
            await _unitOfWork.Resolve<Tank>().UpdateAsync(Tank.ToArray());
            await _unitOfWork.SaveChangesAsync();

            return Success<bool>("Tạo thành công");
        }

        public async Task<ApiResponse<bool>> Update(TankHistory request)
        {
            var TankHistory = await _unitOfWork.Resolve<TankHistory>().FindAsync(request.Id);
            if (TankHistory == null)
                return Failed<bool>("Không tìm thấy");

            await _unitOfWork.Resolve<TankHistory>().UpdateAsync(request);
            return Success<bool>("Cập nhật thành công");
        }
    }
}
