using BikeRent.Domain.Entities;

namespace BikeRent.Infra.Interfaces
{
    public interface IOrderMessageRepository: IRepository<OrderMessage>
    {
        Task<IEnumerable<OrderMessage>> FindAllByOrderId(Guid orderId);
        Task<OrderMessage?> FindByDeliveryManAndOrderId(Guid deliverymanId, Guid orderId);
    }
}