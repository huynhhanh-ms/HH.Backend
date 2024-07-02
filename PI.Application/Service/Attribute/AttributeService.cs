using PI.Domain.Dto.ProductAttribute;
using PI.Domain.Models;
using PI.Domain.Repositories.Common;

namespace PI.Application.Service.Attribute
{
    public class AttributeService : BaseService, IAttributeService
    {
        public AttributeService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<ApiResponse<string>> CreateAttribute(CreateProductAttributeRequest request)
        {
            try
            {
                //check if attribute name is existed
                var existedAttribute = await _unitOfWork.Resolve<ProductAttribute>()
                    .FindAsync(x => x.ProductAttName == request.ProductAttName);
                if (existedAttribute != null)
                {
                    return Failed<string>("Attribute name is existed", HttpStatusCode.BadRequest);
                }

                var attribute = request.Adapt<ProductAttribute>();
                await _unitOfWork.Resolve<ProductAttribute>().CreateAsync(attribute);
                await _unitOfWork.SaveChangesAsync();
                return Success<string>("Create attribute successfully");
            }
            catch (Exception ex)
            {
                return Failed<string>(ex.GetExceptionMessage());
            }
        }

        public async Task<ApiResponse<IEnumerable<ProductAttributeResponse>>> GetAttributes()
        {
            var attributes = await _unitOfWork.Resolve<ProductAttribute>().GetAllAsync();
            return Success<IEnumerable<ProductAttributeResponse>>(attributes.Adapt<IEnumerable<ProductAttributeResponse>>());
        }
    }
}