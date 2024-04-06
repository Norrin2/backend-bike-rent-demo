using BikeRent.Domain;
using BikeRent.Infra.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BikeRent.Infra.Database
{
    public class BikeRepository : Repository<Bike>, IBikeRepository
    {
        public BikeRepository(BikeRentDbContext dbContext) : base(dbContext)
        {
        }

        protected override DbSet<Bike> DbSet => _dbContext.Bikes;

        public async Task<Bike?> FindByLicensePlate(string licensePlate)
        {
            return await DbSet.FirstOrDefaultAsync(x => x.LicensePlate == licensePlate);
        }
    }
}
