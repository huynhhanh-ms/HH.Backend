using HH.Application.Common;
using HH.Domain.Common;
using HH.Domain.Dto;
using HH.Domain.Enums;
using HH.Domain.Models;
using HH.Domain.Repositories;
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
            FuelImport.Status = FuelImportStatus.Processing.ToString();
            FuelImport.TotalSalePrice = 0;

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

        public async Task<ApiResponse<FuelImportGetDto>> Get(int id)
        {
            var FuelImport = await _unitOfWork.Resolve<FuelImport>().FindAsync(id);

            if (FuelImport == null)
                return Failed<FuelImportGetDto>("Không tìm thấy");

            return Success<FuelImportGetDto>(FuelImport.Adapt<FuelImportGetDto>());
        }

        public async Task<ApiResponse<List<FuelImportGetDto>>> Gets(SearchBaseRequest request)
        {
            var FuelImports = await _unitOfWork.Resolve<IFuelImportRepository>().GetAllWithInclude();
            if (FuelImports == null)
                return Failed<List<FuelImportGetDto>>("Không tìm thấy");

            return Success<List<FuelImportGetDto>>(FuelImports.Adapt<List<FuelImportGetDto>>());
        }

        public async Task<ApiResponse<bool>> Update(FuelImportUpdateDto request)
        {
            var FuelImport = await _unitOfWork.Resolve<FuelImport>().FindAsync(request.Id);
            if (FuelImport == null)
                return Failed<bool>("Không tìm thấy");

            var FuelImportConverted = request.Adapt(FuelImport);

            await _unitOfWork.Resolve<FuelImport>().UpdateAsync(FuelImportConverted);
            return Success<bool>("Cập nhật thành công");
        }
    }
}
