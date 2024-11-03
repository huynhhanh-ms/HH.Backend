using HH.Domain.Infrastructure.File;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace HH.Api.Controllers
{
    [Route("api/v1/file")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IFileService _fileService;

        public FileController(IFileService fileService)
        {
            _fileService = fileService;
        }

        [HttpGet]
        public IActionResult GetFileUrl(string storageName)
        {
            var result = _fileService.GetFileUrl(storageName);
            return StatusCode((int)HttpStatusCode.OK, result);
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadAsync(string? storagePath, IFormFile file)
        {
            var result = await _fileService.UploadAsync(storagePath, file);
            return StatusCode((int)HttpStatusCode.OK, result);
        }

        [HttpPost("upload-multiple")]
        public async Task<IActionResult> UploadAsync(string storageName, List<IFormFile> files)
        {
            var result = await _fileService.UploadAsync(storageName, files);
            return StatusCode((int)HttpStatusCode.OK, result);
        }
    }

}
