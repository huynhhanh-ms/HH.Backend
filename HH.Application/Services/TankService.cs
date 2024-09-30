using HH.Application.Common;
using HH.Domain.Common;
using HH.Domain.Dto;
using HH.Domain.Models;
using HH.Domain.Repositories.Common;
using HH.Persistence.Repositories.Common;
using Mapster;

namespace HH.Application.Services
{
    public class TankService : BaseService, ITankService
    {
        public TankService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<ApiResponse<bool>> Create(TankCreateDto request)
        {
            var tank = request.Adapt<Tank>();

            tank.CreatedBy = 0;
            tank.UpdatedBy = 0;
            tank.Id = 10;

            await _unitOfWork.Resolve<Tank>().CreateAsync(tank);
            await _unitOfWork.SaveChangesAsync();

            return Success<bool>("Tạo thành công");
        }

        public async Task<ApiResponse<bool>> Delete(int id)
        {
            await _unitOfWork.Resolve<Tank>().DeleteAsync(id);
            return Success<bool>("Xóa thành công");
        }

        public async Task<ApiResponse<Tank>> Get(int id)
        {
            var tank = await _unitOfWork.Resolve<Tank>().FindAsync(id);

            if (tank == null)
                return Failed<Tank>("Không tìm thấy");

            return Success<Tank>(tank);
        }

        public async Task<ApiResponse<List<Tank>>> Gets(SearchBaseRequest request)
        {
            var tanks = await _unitOfWork.Resolve<Tank>().GetAllAsync();
            if (tanks == null)
                return Failed<List<Tank>>("Không tìm thấy");
            return Success<List<Tank>>(tanks.ToList());
        }

        public async Task<ApiResponse<bool>> Update(Tank request)
        {
            var tank = await _unitOfWork.Resolve<Tank>().FindAsync(request.Id);
            if (tank == null)
                return Failed<bool>("Không tìm thấy");

            await _unitOfWork.Resolve<Tank>().UpdateAsync(request);
            return Success<bool>("Cập nhật thành công");
        }
    }
}
