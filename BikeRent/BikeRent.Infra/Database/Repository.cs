using BikeRent.Domain;
using BikeRent.Infra.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BikeRent.Infra.Database
{
    public class Repository<T> : IRepository<T> where T: Entity
    {
        protected readonly BikeRentDbContext _dbContext;
        protected readonly DbSet<T> _dbSet;
        public Repository(BikeRentDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }

        public async Task Add(T entity) 
        {
            await _dbContext.AddAsync(entity);
        }

        public async Task<T?> FindById(Guid id)
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<T?> Update(T entity)
        {
            var existingEntity = await FindById(entity.Id);
            if (existingEntity != null)
            {
                _dbContext.Entry(existingEntity).CurrentValues.SetValues(entity);
                return existingEntity;
            }

            return null;
        }

        public async Task SaveChanges()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
