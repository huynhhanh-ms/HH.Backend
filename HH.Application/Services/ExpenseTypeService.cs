using HH.Application.Common;
using HH.Domain.Common;
using HH.Domain.Dto;
using HH.Domain.Models;
using HH.Domain.Repositories.Common;
using HH.Persistence.Repositories.Common;
using Mapster;

namespace HH.Application.Services
{
    public class ExpenseTypeService : BaseService, IExpenseTypeService
    {
        public ExpenseTypeService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<ApiResponse<int>> Create(ExpenseTypeCreateDto request)
        {
            var ExpenseType = request.Adapt<ExpenseType>();

            await _unitOfWork.Resolve<ExpenseType>().CreateAsync(ExpenseType);
            await _unitOfWork.SaveChangesAsync();

            return Success<int>(ExpenseType.Id, "Tạo thành công");
        }

        public async Task<ApiResponse<bool>> Delete(int id)
        {
            await _unitOfWork.Resolve<ExpenseType>().DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();
            return Success<bool>("Xóa thành công");
        }

        public async Task<ApiResponse<ExpenseTypeGetDto>> Get(int id)
        {
            var ExpenseType = await _unitOfWork.Resolve<ExpenseType>().FindAsync(id);
            var ExpenseTypeGetDto = ExpenseType.Adapt<ExpenseTypeGetDto>();

            if (ExpenseType == null)
                return Failed<ExpenseTypeGetDto>("Không tìm thấy");

            return Success<ExpenseTypeGetDto>(ExpenseTypeGetDto);
        }

        public async Task<ApiResponse<List<ExpenseType>>> Gets(SearchBaseRequest request)
        {
            var ExpenseTypes = await _unitOfWork.Resolve<ExpenseType>().GetAllAsync();
            ExpenseTypes = ExpenseTypes.OrderBy(ExpenseType => -ExpenseType.Id);

            if (ExpenseTypes == null)
                return Failed<List<ExpenseType>>("Không tìm thấy");
            return Success<List<ExpenseType>>(ExpenseTypes.ToList());
        }

        public async Task<ApiResponse<bool>> Update(ExpenseTypeUpdateDto request)
        {
            var ExpenseType = await _unitOfWork.Resolve<ExpenseType>().FindAsync(request.Id);

            if (ExpenseType == null)
                return Failed<bool>("Không tìm thấy");

            var updatedExpenseType = request.Adapt<ExpenseType>();

            await _unitOfWork.Resolve<ExpenseType>().UpdateAsync(updatedExpenseType);
            await _unitOfWork.SaveChangesAsync();

            return Success<bool>("Cập nhật thành công");
        }
    }
}
