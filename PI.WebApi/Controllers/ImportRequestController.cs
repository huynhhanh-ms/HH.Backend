using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PI.Domain.Dto.ImportRequest;
using PI.Domain.Dto.ImportRequest.MergeImportRequest;
using PI.Domain.Enums;

namespace PI.WebApi.Controllers
{
    [Route("api/v1/import-request")]
    [ApiController]
    public class ImportRequestController : ControllerBase
    {
        private readonly IImportRequestService _importRequestService;

        public ImportRequestController(IImportRequestService importRequestService)
        {
            _importRequestService = importRequestService;
        }


        [HttpGet]
        [Authorize]
        public async Task<IActionResult> SearchImportRequest([FromQuery] SearchImportReqRequest searchReq)
        {
            var response = await _importRequestService.SearchImportRequest(searchReq);
            return StatusCode((int)response.StatusCode,response);
        }

        /// <summary>
        /// Get detail information of an import request
        /// </summary>
        /// <param name="importRequestId"></param>
        /// <returns></returns>
        [HttpGet("{importRequestId}")]
        [Authorize]
        public async Task<IActionResult> GetImportRequestDetail(int importRequestId)
        {
            var response = await _importRequestService.GetImportRequestDetail(importRequestId);
            return StatusCode((int)response.StatusCode, response);
        }

        /// <summary>
        /// Create a new import request
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "Staff")]
        public async Task<IActionResult> CreateImportRequest([FromBody] CreateImportReqRequest request)
        {
            var response = await _importRequestService.CreateImportRequest(request);
            return StatusCode((int)response.StatusCode, response);
        }

        /// <summary>
        /// Merge many import request to one import request
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("merge")]
        [Authorize(Roles = "Staff")]
        public async Task<IActionResult> MergeImportRequest([FromBody] MergeImportReqRequest request)
        {
            var response = await _importRequestService.MergeImportRequest(request);
            return StatusCode((int)response.StatusCode, response);
        }

        /// <summary>
        /// Get total quantity of products in all import request
        /// </summary>
        /// <param name="searchRequest"></param>
        /// <returns></returns>
        [HttpGet("products")]
        [Authorize]
        public async Task<IActionResult> GetTotalImportRequestQuantity([FromQuery] SearchBaseRequest searchRequest)
        {
            var response = await _importRequestService.GetTotalImportRequestQuantity(searchRequest);
            return StatusCode((int)response.StatusCode, response);
        }

        /// <summary>
        /// Delete import requests
        /// </summary>
        /// <param name="importRequestIds"></param>
        /// <returns></returns>
        [HttpDelete]
        [Authorize(Roles = "Stockkeeper")]
        public async Task<IActionResult> DeleteImportRequest([FromQuery] int[] importRequestIds)
        {
            var response = await _importRequestService.DeleteImportRequest(importRequestIds);
            return StatusCode((int)response.StatusCode, response);
        }

        /// <summary>
        /// Reject import requests
        /// </summary>
        /// <param name="importRequestIds"></param>
        /// <returns></returns>
        [HttpPut("reject")]
        [Authorize(Roles = "Stockkeeper")]
        public async Task<IActionResult> RejectImportRequest([FromQuery] int[] importRequestIds)
        {
            var response = await _importRequestService.ChangeImportRequestStatus(ImportRequestStatus.Rejected,importRequestIds);
            return StatusCode((int)response.StatusCode, response);
        }

        /// <summary>
        /// Approve import requests
        /// </summary>
        /// <param name="importRequestIds"></param>
        /// <returns></returns>
        [HttpPut("approve")]
        [Authorize(Roles = "Stockkeeper")]
        public async Task<IActionResult> ApproveImportRequest([FromQuery] int[] importRequestIds)
        {
            var response = await _importRequestService.ChangeImportRequestStatus(ImportRequestStatus.Accepted, importRequestIds);
            return StatusCode((int)response.StatusCode, response);
        }
    }
}
