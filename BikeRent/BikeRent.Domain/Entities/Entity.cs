using Flunt.Notifications;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace BikeRent.Domain.Entities
{
    public class Entity : INotifiable
    {
        public Guid Id { get; set; }
        [JsonIgnore]
        [BsonIgnore]
        public IList<Notification> Notifications => _notifications == null ? _notifications = [] : _notifications;
        private IList<Notification> _notifications;

        [JsonIgnore]
        [BsonIgnore]
        public bool IsValid => Notifications != null && Notifications.Any();

        protected Entity()
        {
            Id = Guid.NewGuid();
            _notifications = [];
        }

        public void AddNotifications(IEnumerable<Notification> notifications)
        {
            if (_notifications == null) _notifications = [];
            var newNotifications = notifications.ToList();
            newNotifications.AddRange(Notifications);
            _notifications = newNotifications;
        }

        public void AddNotification(string key, string message)
        {
            if (_notifications == null) _notifications = [];
            var notification = new Notification() { Key = key, Message = message };
            Notifications.Add(notification);
        }
    }
}
