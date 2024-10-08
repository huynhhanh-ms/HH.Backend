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
        Task<ApiResponse<bool>> Create(SessionCreateDto request);
        Task<ApiResponse<bool>> Update(Session request);
        Task<ApiResponse<List<Session>>> Gets(SearchBaseRequest request);
        Task<ApiResponse<Session>> Get(int id);
        Task<ApiResponse<bool>> Delete(int id);
    }
}
