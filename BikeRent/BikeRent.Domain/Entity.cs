using Flunt.Notifications;

namespace BikeRent.Domain
{
    public class Entity: Notifiable<Notification>
    {
        public Guid Id { get; set; }

        protected Entity() 
        {
            Id = Guid.NewGuid();
        }
    }
}
