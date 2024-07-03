using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PI.Application.Service.Attribute;
using PI.Domain.Dto.ProductAttribute;

namespace PI.WebApi.Controllers
{
    [ApiController]
    [Route("api/v1/attributes")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class AttributeController : ControllerBase
    {
        private readonly IAttributeService _attributeService;

        public AttributeController(IAttributeService attributeService)
        {
            _attributeService = attributeService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAttributes()
        {
            var result = await _attributeService.GetAttributes();
            return StatusCode((int)result.StatusCode, result);
        }

        [HttpPost]
        [Authorize(Roles = "Manager, Stockkeeper")]
        public async Task<IActionResult> CreateAttribute([FromBody] CreateProductAttributeRequest request)
        {
            var result = await _attributeService.CreateAttribute(request);
            return StatusCode((int)result.StatusCode, result);
        }
    }
}