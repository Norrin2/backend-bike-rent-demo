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

        public Order(decimal value)
        {
            CreatedAt = DateTime.Now;
            Value = value;
            Status = OrderStatus.Availabe;
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
