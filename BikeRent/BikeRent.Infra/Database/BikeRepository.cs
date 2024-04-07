using BikeRent.Domain.Entities;
using BikeRent.Infra.Interfaces;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace BikeRent.Infra.Database
{
    public class BikeRepository : Repository<Bike>, IBikeRepository
    {
        public BikeRepository(IMongoDatabase database) : base(database)
        {
        }

        public async Task<Bike?> FindByLicensePlate(string licensePlate)
        {
            return await _collection.AsQueryable().FirstOrDefaultAsync(x => x.LicensePlate == licensePlate);
        }
    }
}
