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
    public interface IFuelImportService
    {
        Task<ApiResponse<bool>> Create(FuelImportCreateDto request);
        Task<ApiResponse<bool>> Update(FuelImport request);
        Task<ApiResponse<List<FuelImport>>> Gets(SearchBaseRequest request);
        Task<ApiResponse<FuelImport>> Get(int id);
        Task<ApiResponse<bool>> Delete(int id);
    }
}