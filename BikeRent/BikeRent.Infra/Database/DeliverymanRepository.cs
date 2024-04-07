using BikeRent.Domain.Entities;
using BikeRent.Infra.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BikeRent.Infra.Database
{
    public class DeliverymanRepository : Repository<Deliveryman>, IDeliverymanRepository
    {
        public DeliverymanRepository(BikeRentDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Deliveryman?> FindByCnpj(string cnpj)
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.Cnpj == cnpj);
        }

        public async Task<Deliveryman?> FindByCnh(string cnh)
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.Cnh.Number == cnh);
        }
    }
}
