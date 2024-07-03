using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PI.WebApi.Controllers
{
    [ApiController]
    [Route("api/v1/medicine")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class MedicineController : ControllerBase
    {
        
        private readonly IMedicineService _medicineService;
        
        public MedicineController(IMedicineService medicineService)
        {
            _medicineService = medicineService;
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> SearchMedicine([FromQuery] SearchBaseRequest searchBaseReq)
        {
            var result = await _medicineService.SearchMedicineAsync(searchBaseReq.KeySearch, searchBaseReq.PagingQuery, searchBaseReq.OrderBy);
            return StatusCode((int)result.StatusCode, result);
        }
        
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> CreateMedicine()
        {
            await _medicineService.CreateMedicine();
            return Ok();
        }
    }
}