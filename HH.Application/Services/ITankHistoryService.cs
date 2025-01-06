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
    public interface ITankHistoryService
    {
        Task<ApiResponse<bool>> Create(TankHistoryCreateDto request);
        Task<ApiResponse<bool>> Save(TankHistorySaveDto request);
        Task<ApiResponse<bool>> Update(TankHistory request);
        Task<ApiResponse<List<TankHistory>>> Gets(SearchBaseRequest request);
        Task<ApiResponse<TankHistory>> Get(int id);
        Task<ApiResponse<bool>> Delete(int id);
    }
}
