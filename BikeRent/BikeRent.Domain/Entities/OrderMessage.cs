namespace BikeRent.Domain.Entities
{
    public class OrderMessage: Entity
    {
        public Order Order { get; private set; }
        public Guid DeliveryManId { get; private set; }
        public string DeliveryManCnpj { get; private set; }

        public OrderMessage(Order order, Guid deliveryManId, string deliveryManCnpj)
        {
            Order = order;
            DeliveryManId = deliveryManId;
            DeliveryManCnpj = deliveryManCnpj;
        }
    }
}
