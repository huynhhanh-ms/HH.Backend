using HH.Application.Common;
using HH.Domain.Common;
using HH.Domain.Dto;
using HH.Domain.Models;
using HH.Domain.Repositories.Common;
using HH.Persistence.Repositories.Common;
using Mapster;

namespace HH.Application.Services
{
    public class ExpenseService : BaseService, IExpenseService
    {
        public ExpenseService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<ApiResponse<int>> Create(ExpenseCreateDto request)
        {
            var Expense = request.Adapt<Expense>();

            await _unitOfWork.Resolve<Expense>().CreateAsync(Expense);
            await _unitOfWork.SaveChangesAsync();

            return Success<int>(Expense.Id, "Tạo thành công");
        }

        public async Task<ApiResponse<bool>> Delete(int id)
        {
            await _unitOfWork.Resolve<Expense>().DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();
            return Success<bool>("Xóa thành công");
        }

        public async Task<ApiResponse<ExpenseGetDto>> Get(int id)
        {
            var Expense = await _unitOfWork.Resolve<Expense>().FindAsync(id);
            var ExpenseGetDto = Expense.Adapt<ExpenseGetDto>();

            if (Expense == null)
                return Failed<ExpenseGetDto>("Không tìm thấy");

            return Success<ExpenseGetDto>(ExpenseGetDto);
        }

        public async Task<ApiResponse<List<Expense>>> Gets(SearchBaseRequest request)
        {
            var Expenses = await _unitOfWork.Resolve<Expense>().GetAllAsync();
            Expenses = Expenses.OrderBy(Expense => -Expense.Id);

            if (Expenses == null)
                return Failed<List<Expense>>("Không tìm thấy");
            return Success<List<Expense>>(Expenses.ToList());
        }

        public async Task<ApiResponse<bool>> Update(ExpenseUpdateDto request)
        {
            var Expense = await _unitOfWork.Resolve<Expense>().FindAsync(request.Id);

            if (Expense == null)
                return Failed<bool>("Không tìm thấy");

            var updatedExpense = request.Adapt<Expense>();

            await _unitOfWork.Resolve<Expense>().UpdateAsync(updatedExpense);
            await _unitOfWork.SaveChangesAsync();

            return Success<bool>("Cập nhật thành công");
        }
    }
}
