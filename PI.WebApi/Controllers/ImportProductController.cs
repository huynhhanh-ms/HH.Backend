using Microsoft.AspNetCore.Mvc;
using System.Net;
using Microsoft.AspNetCore.Authorization;

namespace PI.WebApi.Controllers
{
    [Route("api/product")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ImportProductController : ControllerBase
    {
        private readonly IImportProductsService _importProductsService;

        public ImportProductController(IImportProductsService importProductsService)
        {
            _importProductsService = importProductsService;
        }

        [HttpGet("import-product-excel-template")]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> GetImportProductExcelTemplate()
        {
            var result = await _importProductsService.GenerateImportProductsExcelTemplate();

            if (result.StatusCode == HttpStatusCode.OK)
            {
                if (result.Data == null)
                {
                    return StatusCode((int)HttpStatusCode.NoContent);
                }

                return File(result.Data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileDownloadName: "import-product-template");
            }
            else
                return StatusCode((int)result.StatusCode, result.Message);
        }

        [HttpPost("read-import-product-file")]
        [Authorize]
        public async Task<IActionResult> ReadImportProductFile(IFormFile formFile)
        {
            try
            {
                if (formFile == null || formFile.Length <= 0)
                {
                    return BadRequest("File is empty or not provided");
                }

                byte[] file;

                using (var memoryStream = new MemoryStream())
                {
                    await formFile.CopyToAsync(memoryStream);
                    file = memoryStream.ToArray();
                }

                var result = await _importProductsService.ReadImportProductFile(file);

                return StatusCode((int)result.StatusCode, result);
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while processing the file.");
            }
        }
    }
}
