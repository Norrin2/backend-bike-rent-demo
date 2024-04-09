using BikeRent.Domain.Entities;
using BikeRent.Domain.ValueObject;
using BikeRent.Infra.Interfaces;
using BikeRent.Publisher.Interfaces;

namespace BikeRent.Publisher.Service
{
    public class OrderService : ServiceBase<Order>, IOrderService
    {
        private readonly IDeliverymanRepository _deliverymanRepository;
        public OrderService(IRepository<Order> repository, IDeliverymanRepository deliverymanRepository) : base(repository)
        {
            _deliverymanRepository = deliverymanRepository;
        }

        public async Task<Order> PlaceOrder(decimal value)
        {
            var availableDeliveryman = await _deliverymanRepository.FindAvalilableDeliveryman();
            var deliverymanToNotify = availableDeliveryman.Select(d =>
                new OrderNotification(d.Id, d.Cnpj));

            var order = new Order(value, deliverymanToNotify);
            await _repository.Add(order);

            return order;
        }
    }
}
