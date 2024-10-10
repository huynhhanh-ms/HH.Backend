using HH.Domain.Dto;
using HH.Domain.Dto.Account;
using HH.Domain.Models;
using System.Xml.Serialization;

namespace HH.Domain.Common;

public static class RegisterMapsterMappingType
{
    public static void RegisterMapsterMappingTypes(this ContainerBuilder container)
    {
        #region Account

        // Profile
        TypeAdapterConfig<Account, AccountRes>
            .NewConfig()
            .Map(dest => dest.Fullname, src => src.Fullname)
            .IgnoreNullValues(true);

        TypeAdapterConfig<Expense, ExpenseGetDto>
            .NewConfig()
            .Map(dest => dest.ExpenseTypeName, src => src.ExpenseType.Name)
            .IgnoreNullValues(true);

        #endregion
    }
}