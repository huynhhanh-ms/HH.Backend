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
    public interface ISessionService
    {
        Task<ApiResponse<int>> Create(SessionCreateDto request);
        Task<ApiResponse<bool>> Update(SessionUpdateDto request);
        Task<ApiResponse<List<Session>>> Gets(SearchBaseRequest request);
        Task<ApiResponse<SessionGetDto>> Get(int id);
        Task<ApiResponse<bool>> Delete(int id);

        Task<ApiResponse<bool>> Close(int id);
    }
}
