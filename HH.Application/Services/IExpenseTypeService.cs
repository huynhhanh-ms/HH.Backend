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
    public interface IExpenseTypeService
    {
        Task<ApiResponse<int>> Create(ExpenseTypeCreateDto request);
        Task<ApiResponse<bool>> Update(ExpenseTypeUpdateDto request);
        Task<ApiResponse<List<ExpenseType>>> Gets(SearchBaseRequest request);
        Task<ApiResponse<ExpenseTypeGetDto>> Get(int id);
        Task<ApiResponse<bool>> Delete(int id);
    }
}
