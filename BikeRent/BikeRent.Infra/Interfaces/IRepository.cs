using BikeRent.Domain.Entities;

namespace BikeRent.Infra.Interfaces
{
    public interface IRepository<T> where T : Entity
    {
        Task Add(T entity);
        Task<T?> FindById(Guid id);
        Task<T?> Update(T entity);
        Task SaveChanges();
    }
}
