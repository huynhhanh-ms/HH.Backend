using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PI.Domain.Dto.Shipment;

namespace PI.WebApi.Controllers
{
    [ApiController]
    [Route("api/v1/import-shipment")]
    public class ImportShipmentController : ControllerBase
    {
        private readonly IShipmentService _shipmentService;

        public ImportShipmentController(IShipmentService shipmentService)
        {
            _shipmentService = shipmentService;
        }

        [Authorize(Roles = "Staff")]
        [HttpPost]
        public async Task<IActionResult> ImportShipment(ImportShipmentRequest request)
        {
            var result = await _shipmentService.CreateImportShipment(request);
            return StatusCode((int)result.StatusCode, result);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> SearchImportShipment([FromQuery]SearchShipmentReq request)
        {
            var result = await _shipmentService.SearchImportShipment(request);
            return StatusCode((int)result.StatusCode, result);
        }
    }
}