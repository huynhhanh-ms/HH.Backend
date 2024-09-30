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
    public interface ITankService
    {
        Task<ApiResponse<bool>> Create(TankCreateDto request);
        Task<ApiResponse<bool>> Update(Tank request);
        Task<ApiResponse<List<Tank>>> Gets(SearchBaseRequest request);
        Task<ApiResponse<Tank>> Get(int id);
        Task<ApiResponse<bool>> Delete(int id);
    }
}
