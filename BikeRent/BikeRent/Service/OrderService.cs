using BikeRent.Domain.Entities;
using BikeRent.Domain.ValueObject;
using BikeRent.Infra.Interfaces;
using BikeRent.Publisher.Interfaces;
using System.Text.Json;

namespace BikeRent.Publisher.Service
{
    public class OrderService : ServiceBase<Order>, IOrderService
    {
        private readonly IDeliverymanRepository _deliverymanRepository;
        private readonly IMessageService _messageService;
        public OrderService(IRepository<Order> repository, IDeliverymanRepository deliverymanRepository, IMessageService messageService) : base(repository)
        {
            _deliverymanRepository = deliverymanRepository;
            _messageService = messageService;
        }

        public async Task<Order> PlaceOrder(decimal value)
        {
            var availableDeliveryman = await _deliverymanRepository.FindAvalilableDeliveryman();

            var order = new Order(value);
            var messages = availableDeliveryman.Select(d =>
                JsonSerializer.Serialize(
                new OrderMessage(order, d.Id, d.Cnpj)));

            await _repository.Add(order);
            _messageService.PublishMessages(messages);

            return order;
        }
    }
}
