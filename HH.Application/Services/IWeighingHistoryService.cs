using HH.Domain.Common;
using HH.Domain.Dto;
using HH.Domain.Dto.WeighingHistory;
using HH.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HH.Application.Services
{
    public interface IWeighingHistoryService
    {
        Task<ApiResponse<bool>> Create(WeighingHistoryCreateDto request);
        Task<ApiResponse<bool>> Update(WeighingHistory request);
        Task<ApiResponse<List<WeighingHistory>>> Gets(SearchBaseRequest request);
        Task<ApiResponse<WeighingHistory>> Get(int id);
        Task<ApiResponse<bool>> Delete(int id);
    }
}
