﻿using HH.Domain.Common;
using HH.Domain.Dto;
using HH.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HH.Application.Services
{
    public interface IAccountService
    {
        Task<ApiResponse<bool>> Create(AccountCreateDto request);
        Task<ApiResponse<bool>> Update(Account request);
        Task<ApiResponse<List<Account>>> Gets(SearchBaseRequest request);
        Task<ApiResponse<Account>> Get(int id);
        Task<ApiResponse<bool>> Delete(int id);
    }
}
