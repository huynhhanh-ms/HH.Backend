using PI.Domain.Dto.Product;
using PI.Domain.Models;
using PI.Domain.Repositories;
using System.Linq.Dynamic.Core;

namespace PI.Persitence.Repository
{
    public class LotRepository : GenericRepository<Lot>, ILotRepository
    {
        public LotRepository(DbContext context) : base(context)
        {
        }

        public override Task<IPagedList<Lot>> SearchAsync(string keySearch, PagingQuery pagingQuery, string orderBy)
        {
            throw new NotImplementedException();
        }

        public override Task<IPagedList<TResult>> SearchAsync<TResult>(string keySearch, PagingQuery pagingQuery,
            string orderBy)
        {
            throw new NotImplementedException();
        }

        public Task<IPagedList<ProductLotResponse>> SearchAsync(int productUnitId, PagingQuery pagingQuery,
            string orderBy, LotStatus? lotStatus)
        {
            return _dbSet.AsNoTracking()
                .Where(p => p.ProductUnitId == productUnitId)
                .WhereWithExist(p => lotStatus == null || p.LotStatus == lotStatus.ToString().ToLower())
                .WithOrderByString(orderBy)
                .SelectWithField<Lot, ProductLotResponse>(
                    p => new ProductLotResponse
                    {
                        LotId = p.LotId,
                        LotCode = p.LotCode,
                        LotStatus = Enum.Parse<LotStatus>(p.LotStatus),
                        ManufacturingDate = p.ManufacturingDate,
                        ExpirationDate = p.ExpirationDate,
                        StockQuantity = p.ShipmentDetails.Sum(s => s.Quantity),
                        ProductUnitId = p.ProductUnitId
                    }
                )
                .ToPagedListAsync(pagingQuery);
        }
    }
}