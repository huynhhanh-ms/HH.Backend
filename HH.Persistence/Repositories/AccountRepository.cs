using System.Linq.Dynamic.Core;
using HH.Domain.Common;
using HH.Domain.Enums;
using HH.Domain.Models;
using HH.Domain.Repositories;
using HH.Persistence.Repositories.Common;
using HH.Persistence.Repositories.Helper;
using Microsoft.EntityFrameworkCore;


namespace HH.Persistence.Repositories;

public class AccountRepository : GenericRepository<Account>, IAccountRepository
{
    public AccountRepository(DbContext context) : base(context)
    {
    }
}