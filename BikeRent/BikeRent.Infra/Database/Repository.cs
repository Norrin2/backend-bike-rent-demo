using BikeRent.Domain;
using BikeRent.Infra.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BikeRent.Infra.Database
{
    public abstract class Repository<T> : IRepository<T> where T: Entity
    {
        protected readonly BikeRentDbContext _dbContext;
        protected abstract DbSet<T> DbSet { get; }
        public Repository(BikeRentDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Add(T entity) 
        {
            await _dbContext.AddAsync(entity);
        }

        public async Task<T?> FindById(Guid id)
        {
            return await DbSet.FirstOrDefaultAsync(x => x.Id == id);
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
