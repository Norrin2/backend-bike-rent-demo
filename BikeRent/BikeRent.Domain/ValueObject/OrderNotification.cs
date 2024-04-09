namespace BikeRent.Domain.ValueObject
{
    public class OrderNotification
    {
        public Guid DeliveryManId { get; private set; }
        public string DeliveryManCnpj { get; private set; }

        public OrderNotification(Guid deliveryManId, string deliveryManCnpj)
        {
            DeliveryManId = deliveryManId;
            DeliveryManCnpj = deliveryManCnpj;
        }
    }
}
