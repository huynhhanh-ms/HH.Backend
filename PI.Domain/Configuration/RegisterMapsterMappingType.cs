using PI.Domain.Dto.Assignment;
using PI.Domain.Dto.ExportRequest;
using PI.Domain.Dto.ImportRequest;
using PI.Domain.Dto.Product;
using PI.Domain.Dto.ProductStock;
using PI.Domain.Dto.StockCheck;
using PI.Domain.Extensions;
using PI.Domain.Models;
using System.Xml.Serialization;
using PI.Domain.Dto.Account;
using PI.Domain.Enums;

namespace PI.Domain.Configuration
{
    public static class RegisterMapsterMappingType
    {
        public static void RegisterMapsterMappingTypes(this ContainerBuilder container)
        {
            #region  
            // Profile for User to UserResponse
            TypeAdapterConfig<Account, AccountResponse>
                .NewConfig()
                .Map(dest => dest.IsFree,
                    src => !src.StockCheckStockkeepers
                        .Select(x =>
                            x.Status == StockCheckEnum.StockCheckStatus.Assigned.ToString()).Any())
                .Map (dest => dest.TaskCount, src => src.StockCheckStockkeepers.Count)
                .IgnoreNullValues(true);

            #endregion
            #region Product 
            // Profile for Product to ProductResponse
            TypeAdapterConfig<Product, ProductResponse>
            .NewConfig()
            .Map(dest => dest.RegistrationNo, src => src.Medicine.RegistrationNo)
            .Map(dest => dest.Indication, src => src.Medicine.Indication)
            .Map(dest => dest.Medicinecol, src => src.Medicine.Medicinecol);
            #endregion Product

            #region ProductUnit
            // Profile for ProductUnit to ProductUnitResponse
            TypeAdapterConfig<ProductUnit, ProductUnitResponse>
                .NewConfig()
                .Map(dest => dest.ProductImage, src => src.Product.ProductImages.Select(p => p.ImageUrl))
                .IgnoreIf((src, dest) => src.Product == null || src.Product.ProductImages == null, dest => dest.ProductImage);
            #endregion ProductUnit

            #region Product Stock
            // Profile for ProductStock to ProductStockResponse
            TypeAdapterConfig<ProductStock, ProductStockResponse>
                .NewConfig()
                .Map(dest => dest.SkuCode, src => src.ProductUnit.SkuCode)
                .Map(dest => dest.ProductName, src => src.ProductUnit.Name);
            #endregion Product Stock

            #region ExportRequest 
            // Profile for ExportRequest to ExportRequestResponse
            TypeAdapterConfig<ExportRequest, ExportRequestResponse>
                .NewConfig()
                .Map(dest => dest.Shipments, src => src.Shipments.Select(i => i.ShipmentId))
                .Map(dest => dest.ProductNames, src => src.ExportRequestDetails.Select(i => i.ProductUnit.Product.Name));
            #endregion ExportRequest

            #region ImportRequest
            TypeAdapterConfig<ImportRequest, ImportRequestDetailResponse>
                .NewConfig()
                .Map(dest => dest.ImportRequestDetails, src => src.ImportRequestDetails);

            TypeAdapterConfig<ImportRequestDetail, ImportRequestDetailItemResponse>
                .NewConfig()
                .Map(dest => dest.SkuCode, src => src.ProductUnit.SkuCode)
                .Map(dest => dest.Name, src => src.ProductUnit.Name)
                .Map(dest => dest.UnitName, src => src.ProductUnit.Unit.Name);

            TypeAdapterConfig<ImportRequest, ImportRequestResponse>
                .NewConfig()
                .Map(dest => dest.ProductNames, src => src.ImportRequestDetails.Select(i => i.ProductUnit.Name));
            #endregion ImportRequest

            #region StockCheck
            // Profile for StockCheck to SearchStockCheckResponse
            TypeAdapterConfig<StockCheck, SearchStockCheckResponse>
                .NewConfig()
                .Map(dest => dest.StaffName, src => src.Staff.Fullname)
                .Map(dest => dest.StockkeeperName, src => src.Stockkeeper.Fullname)
                .Map(dest => dest.CreatedByName, src => src.CreatedByNavigation.Fullname)
                .Map(dest => dest.CreatedById, src => src.CreatedByNavigation.AccountId)
                .IgnoreNullValues(true);


            // Profile for StockCheck to StockCheckDetailResponse
            TypeAdapterConfig<StockCheck, StockCheckDetailResponse>
                .NewConfig()
                .Map(dest => dest.StaffName, src => src.Staff.Fullname)
                .Map(dest => dest.StockkeeperName, src => src.Stockkeeper.Fullname)
                .Map(dest => dest.CreatedByName, src => src.CreatedByNavigation.Fullname)
                .Map(dest => dest.CreatedById, src => src.CreatedByNavigation.AccountId)
                .Map(dest => dest.StockCheckDetails, src => src.StockCheckDetails)
                .IgnoreNullValues(true);

            // Profile for StockCheckDetail to StockCheckDetailItemResponse
            TypeAdapterConfig<StockCheckDetail, StockCheckDetailItem>
                .NewConfig()
                .Map(dest => dest.ProductName, src => src.ProductUnit.Name)
                .Map(dest => dest.ProductSkuCode, src => src.ProductUnit.SkuCode);

            //// Profile for UpdateStockCheckRequest to StockCheck 
            //TypeAdapterConfig<SubmitStockCheckRequest, StockCheck>
            //    .NewConfig()
            //    .Ignore(x => x.StockCheckDetails);

            #endregion StockCheck

            #region Assignments
            // Profile for CreateAssignmentRequest to Assignment
            TypeAdapterConfig<CreateAssignmentRequest, Assignment>
                .NewConfig()
                .Map(dest => dest.Status, src => src.Status.ToEnumString())
                .Map(dest => dest.Priority, src => src.Priority.ToEnumString());

            // Profile for UpdateAssignmentRequest to Assignment
            TypeAdapterConfig<UpdateAssignmentRequest, Assignment>
                .NewConfig()
                .Map(dest => dest.Status, src => src.Status.ToEnumString())
                .Map(dest => dest.Priority, src => src.Priority.ToEnumString());


            #endregion Assignments
        }
    }
}
