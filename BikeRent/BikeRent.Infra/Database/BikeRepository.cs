using BikeRent.Domain.Entities;
using BikeRent.Infra.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BikeRent.Infra.Database
{
    public class BikeRepository : Repository<Bike>, IBikeRepository
    {
        public BikeRepository(BikeRentDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Bike?> FindByLicensePlate(string licensePlate)
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.LicensePlate == licensePlate);
        }
    }
}
