using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PI.Domain.Dto.Shipment;

namespace PI.WebApi.Controllers
{
    [ApiController]
    [Route("api/v1/export-shipment")]
    [ApiExplorerSettings(IgnoreApi = true)]

    public class ExportShipmentController : ControllerBase
    {
        private readonly IShipmentService _shipmentService;

        public ExportShipmentController(IShipmentService shipmentService)
        {
            _shipmentService = shipmentService;
        }

        [HttpPost]
        [Authorize(Roles = "Staff")]
        public async Task<IActionResult> ExportShipment([FromBody] ExportShipmentRequest request)
        {
            var response = await _shipmentService.CreateExportShipment(request);
            return StatusCode((int)response.StatusCode, response);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> SearchExportShipment([FromQuery] SearchShipmentReq request)
        {
            var response = await _shipmentService.SearchExportShipment(request);
            return StatusCode((int)response.StatusCode, response);
        }
    }
}