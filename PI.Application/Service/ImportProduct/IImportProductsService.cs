using PI.Domain.Common;
using PI.Domain.Dto.Product.Import;

namespace PI.Application.Service
{
    public interface IImportProductsService : IDisposable
    {
        Task<ApiResponse<byte[]>> GenerateImportProductsExcelTemplate();
        Task<ApiResponse<IEnumerable<ImportProductExcelData>>> ReadImportProductFile(byte[] file);

    }
}
