using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PI.Domain.Dto.ExportRequest;

namespace PI.WebApi.Controllers
{
    [Route("api/v1/export-request")]
    [ApiController]
    public class ExportRequestController : ControllerBase
    {
        private readonly IExportRequestService _exportReqService;

        public ExportRequestController(IExportRequestService exportReqService)
        {
            _exportReqService = exportReqService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Search([FromQuery] SearchExportReqRequest searchReq)
        {
            var res = await _exportReqService.SearchExportRequest(searchReq);

            return StatusCode((int)res.StatusCode, res);
        }

        [HttpGet("{exportRequestId}")]
        [Authorize]
        public async Task<IActionResult> GetDetail(int exportRequestId)
        {
            var res = await _exportReqService.GetExportRequestDetail(exportRequestId);

            return StatusCode((int)res.StatusCode, res);
        }

        [HttpPost]
        [Authorize(Roles = "Staff")]
        public async Task<IActionResult> Create([FromBody] CreateExportRequestReq request)
        {
            var res = await _exportReqService.CreateExportRequest(request);

            return StatusCode((int)res.StatusCode, res);
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> Delete([FromQuery] int[] exportRequestIds)
        {
            var res = await _exportReqService.DeleteExportRequest(exportRequestIds);

            return StatusCode((int)res.StatusCode, res);
        }

        /// <summary>
        /// Approve export request
        /// </summary>
        /// <param name="exportRequestIds"></param>
        /// <returns></returns>
        [HttpPut("approve")]
        [Authorize(Roles = "Stockkeeper")]
        public async Task<IActionResult> Approve([FromQuery] int[] exportRequestIds) 
        {
            var res = await _exportReqService.ChangeExportRequestStatus(Domain.Enums.ExportRequestStatus.Processing, exportRequestIds);

            return StatusCode((int)res.StatusCode, res);
        }

        /// <summary>
        /// Reject export request
        /// </summary>
        /// <param name="exportRequestIds"></param>
        /// <returns></returns>
        [HttpPut("reject")]
        [Authorize(Roles = "Stockkeeper")]
        public async Task<IActionResult> Reject([FromQuery] int[] exportRequestIds)
        {
            var res = await _exportReqService.ChangeExportRequestStatus(Domain.Enums.ExportRequestStatus.Rejected, exportRequestIds);

            return StatusCode((int)res.StatusCode, res);
        }
    }
}
