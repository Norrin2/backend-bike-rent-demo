using BikeRent.Domain.Entities;
using Flunt.Notifications;

namespace BikeRent.Publisher.Interfaces
{
    public interface IServiceBase<T> where T : Entity
    {
        Task<T?> FindById(Guid id);
        Task<IEnumerable<T>> FindAll();
        IEnumerable<Notification> GetNotifications();
    }
}