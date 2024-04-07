using BikeRent.Domain.Entities;
using BikeRent.Infra.Interfaces;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace BikeRent.Infra.Database
{
    public class DeliverymanRepository : Repository<Deliveryman>, IDeliverymanRepository
    {
        public DeliverymanRepository(IMongoDatabase database) : base(database)
        {
        }

        public async Task<Deliveryman?> FindByCnpj(string cnpj)
        {
            return await _collection.AsQueryable().FirstOrDefaultAsync(x => x.Cnpj == cnpj);
        }

        public async Task<Deliveryman?> FindByCnh(string cnh)
        {
            return await _collection.AsQueryable().FirstOrDefaultAsync(x => x.Cnh.Number == cnh);
        }
    }
}
