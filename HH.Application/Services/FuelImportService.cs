using HH.Application.Common;
using HH.Domain.Common;
using HH.Domain.Dto;
using HH.Domain.Models;
using HH.Domain.Repositories.Common;
using HH.Persistence.Repositories.Common;
using Mapster;

namespace HH.Application.Services
{
    public class FuelImportService : BaseService, IFuelImportService
    {
        public FuelImportService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<ApiResponse<bool>> Create(FuelImportCreateDto request)
        {
            var FuelImport = request.Adapt<FuelImport>();

            FuelImport.TotalCost = FuelImport.ImportPrice * FuelImport.ImportVolume;
            FuelImport.ImportDate = DateTime.Now;

            await _unitOfWork.Resolve<FuelImport>().CreateAsync(FuelImport);
            await _unitOfWork.SaveChangesAsync();

            return Success<bool>("Tạo thành công");
        }

        public async Task<ApiResponse<bool>> Delete(int id)
        {
            await _unitOfWork.Resolve<FuelImport>().DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();
            return Success<bool>("Xóa thành công");
        }

        public async Task<ApiResponse<FuelImport>> Get(int id)
        {
            var FuelImport = await _unitOfWork.Resolve<FuelImport>().FindAsync(id);

            if (FuelImport == null)
                return Failed<FuelImport>("Không tìm thấy");

            return Success<FuelImport>(FuelImport);
        }

        public async Task<ApiResponse<List<FuelImport>>> Gets(SearchBaseRequest request)
        {
            var FuelImports = await _unitOfWork.Resolve<FuelImport>().GetAllAsync();
            if (FuelImports == null)
                return Failed<List<FuelImport>>("Không tìm thấy");
            return Success<List<FuelImport>>(FuelImports.ToList());
        }

        public async Task<ApiResponse<bool>> Update(FuelImport request)
        {
            var FuelImport = await _unitOfWork.Resolve<FuelImport>().FindAsync(request.Id);
            if (FuelImport == null)
                return Failed<bool>("Không tìm thấy");

            await _unitOfWork.Resolve<FuelImport>().UpdateAsync(request);
            return Success<bool>("Cập nhật thành công");
        }
    }
}
