using PI.Domain.Common;

namespace PI.Domain.Dto.Product
{
    public class SearchProductRequest : SearchBaseRequest
    {
        public int? CategoryId { get; set; }
        public bool? IsAvailable { get; set; }
        public bool? IsLowOnStock { get; set; }
    }
}