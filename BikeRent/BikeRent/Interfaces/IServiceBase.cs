using BikeRent.Domain;
using Flunt.Notifications;

namespace BikeRent.Publisher.Interfaces
{
    public interface IServiceBase<T> where T : Entity
    {
        Task<T?> FindById(Guid id);
        IEnumerable<Notification> GetNotifications();
    }
}