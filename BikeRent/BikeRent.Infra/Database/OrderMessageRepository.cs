using BikeRent.Domain.Entities;
using BikeRent.Infra.Interfaces;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace BikeRent.Infra.Database
{
    public class OrderMessageRepository : Repository<OrderMessage>, IOrderMessageRepository
    {
        public OrderMessageRepository(IMongoDatabase dataBase) : base(dataBase)
        {
        }

        public async Task<OrderMessage?> FindByDeliveryManAndOrderId(Guid deliverymanId, Guid orderId)
        {
            return await _collection.AsQueryable()
                                    .FirstOrDefaultAsync(x => x.DeliveryManId == deliverymanId
                                                              && x.Order.Id == orderId);
        }

        public async Task<IEnumerable<OrderMessage>> FindAllByOrderId(Guid orderId)
        {
            return await _collection.AsQueryable()
                                    .Where(x => x.Order.Id == orderId)
                                    .ToListAsync();
        }
    }
}
