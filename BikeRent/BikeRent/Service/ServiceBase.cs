using BikeRent.Domain.Entities;
using BikeRent.Infra.Interfaces;
using BikeRent.Publisher.Interfaces;
using Flunt.Notifications;

namespace BikeRent.Publisher.Service
{
    public class ServiceBase<T> : Notifiable<Notification>, IServiceBase<T> where T : Entity
    {
        protected readonly IRepository<T> _repository;

        public ServiceBase(IRepository<T> repository)
        {
            _repository = repository;
        }

        public async Task<T?> FindById(Guid id)
        {
            var entity = await _repository.FindById(id);
            return entity;
        }
        public async Task<IEnumerable<T>> FindAll()
        {
            var entities = await _repository.FindAll();
            return entities;
        }

        public IEnumerable<Notification> GetNotifications()
        {
            return Notifications;
        }
    }
}
