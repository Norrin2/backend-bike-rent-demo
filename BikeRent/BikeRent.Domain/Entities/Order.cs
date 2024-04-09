using BikeRent.Domain.ValueObject;
using System.Text.Json.Serialization;

namespace BikeRent.Domain.Entities
{
    public class Order: Entity
    {
        public DateTime CreatedAt { get; private set; }
        public DateTime FinishedAt { get; private set; }
        public DateTime AcceptedAt { get; private set; }
        public decimal Value { get; private set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public OrderStatus Status { get; private set; }
        public IEnumerable<OrderNotification> OrderNotifications { get; private set; }

        public Order(decimal value, IEnumerable<OrderNotification> orderNotifications)
        {
            CreatedAt = DateTime.Now;
            Value = value;
            Status = OrderStatus.Availabe;
            OrderNotifications = orderNotifications;
        }

        public void AcceptOrder()
        {
            Status = OrderStatus.Accepted;
            AcceptedAt = DateTime.Now;
        }

        public void FinishOrder()
        { 
            Status = OrderStatus.Finished;
            FinishedAt = DateTime.Now;
        }
    }
}
