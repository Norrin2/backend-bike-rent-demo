using BikeRent.Domain.Entities;

namespace BikeRent.Publisher.Interfaces
{
    public interface IOrderService: IServiceBase<Order>
    {
        Task<Order> PlaceOrder(decimal value);
    }
}