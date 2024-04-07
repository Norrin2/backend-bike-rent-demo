using BikeRent.Domain.Entities;
using BikeRent.Infra.Interfaces;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace BikeRent.Infra.Database
{
    public class Repository<T> : IRepository<T> where T: Entity
    {
        protected readonly IMongoDatabase _database;
        protected readonly IMongoCollection<T> _collection;
        public Repository(IMongoDatabase dataBase)
        {
            _database = dataBase;

            List<string> collectionNames = _database.ListCollectionNames().ToList();

            if (!collectionNames.Any(d => d == $"{typeof(T).Name}"))
                _database.CreateCollection(typeof(T).Name);

            _collection = _database.GetCollection<T>(typeof(T).Name);
        }

        public async Task Add(T entity) 
        {
            await _collection.InsertOneAsync(entity);
        }

        public async Task<T?> FindById(Guid id)
        {
            return await _collection.AsQueryable().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<T?> Update(T entity)
        {
            var result = await _collection.ReplaceOneAsync(
                x => x.Id == entity.Id,
                entity
            );

            if (result.ModifiedCount > 0)
            {
                return entity;
            }

            return null;
        }
    }
}
