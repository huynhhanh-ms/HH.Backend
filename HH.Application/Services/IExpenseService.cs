using HH.Domain.Common;
using HH.Domain.Dto;
using HH.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HH.Application.Services
{
    public interface IExpenseService
    {
        Task<ApiResponse<int>> Create(ExpenseCreateDto request);
        Task<ApiResponse<bool>> Update(ExpenseUpdateDto request);
        Task<ApiResponse<List<Expense>>> Gets(SearchBaseRequest request);
        Task<ApiResponse<ExpenseGetDto>> Get(int id);
        Task<ApiResponse<bool>> Delete(int id);
    }
}
