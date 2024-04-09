using BikeRent.Domain.Entities;
using MongoDB.Driver;

namespace BikeRent.Infra.Database
{
    internal class OrderRepository : Repository<Order>
    {
        public OrderRepository(IMongoDatabase dataBase) : base(dataBase)
        {
        }
    }
}
