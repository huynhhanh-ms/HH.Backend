using HH.Domain.Common;
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
        Task<ApiResponse<string>> CreateAccount(Account request);
        Task<ApiResponse<List<Account>>> Search(SearchBaseRequest param);
    }
}
