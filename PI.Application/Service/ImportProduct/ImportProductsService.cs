using OfficeOpenXml;
using PI.Domain.Common.Excel;
using PI.Domain.Dto.Product.Import;
using PI.Domain.Repositories.Common;

namespace PI.Application.Service
{
    public class ImportProductsService : BaseService, IImportProductsService
    {
        public ImportProductsService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        }

        public async Task<ApiResponse<byte[]>> GenerateImportProductsExcelTemplate()
        {
            var headers = new Dictionary<string, string>()
            {
                {"SKU", "ProductSKU" },
                {"Tên sản phẩm", "ProductName"},
                {"Đơn vị", "ProductUnit"},
                {"Lô hàng", "LotCode" },
                {"Ngày sản xuất", "ManufacturingDate"},
                {"Hạn sử dụng", "ExpiryDate" },
                {"Số lượng", "ProductQuantity" },
                {"Giá nhập", "ProductCost" }
            };

            try
            {
                byte[] file = await ExcelExtensions.ExportExcelTemplate(new ExcelTemplateExportModel()
                {
                    Headers = new List<string>(headers.Keys),
                    ValueHeaders = new List<string>(headers.Values),
                    SheetName = "ImportProduct_Data",
                    FileName = "ImportProductsFileTemplate",
                    StringFormatColumns = new List<string>(headers.Keys)
                });

                return Success(file);
            }
            catch (Exception ex)
            {
                return Failed<byte[]>(ex.GetExceptionMessage());
            }
        }

        public async Task<ApiResponse<IEnumerable<ImportProductExcelData>>> ReadImportProductFile(byte[] file)
        {
            try
            {
                var excels = await ExcelExtensions.ConvertExcelToList<ImportProductExcelData>(file);

                if (excels.Status == false || excels.Data == null || !excels.Data.Any())
                {
                    return Failed<IEnumerable<ImportProductExcelData>>("Lỗi đọc dữ liệu từ file excel", statusCode: HttpStatusCode.BadRequest);
                }

                return Success(excels.Data);
            }
            catch (Exception ex)
            {

                return Failed<IEnumerable<ImportProductExcelData>>(ex.GetExceptionMessage());
            }
        }
    }
}
