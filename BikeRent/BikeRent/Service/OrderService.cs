using BikeRent.Domain.Entities;
using BikeRent.Infra.Interfaces;
using BikeRent.Publisher.Interfaces;
using System.Text.Json;

namespace BikeRent.Publisher.Service
{
    public class OrderService : ServiceBase<Order>, IOrderService
    {
        private readonly IDeliverymanRepository _deliverymanRepository;
        private readonly IOrderMessageRepository _orderMessageRepository;
        private readonly IMessageService _messageService;
        public OrderService(IRepository<Order> repository, IDeliverymanRepository deliverymanRepository, IOrderMessageRepository orderMessageRepository, IMessageService messageService) : base(repository)
        {
            _deliverymanRepository = deliverymanRepository;
            _messageService = messageService;
            _orderMessageRepository = orderMessageRepository;
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

        public async Task<IEnumerable<OrderMessage>?> FindMessagesByOrderId(Guid orderId)
        {
            var order = _repository.FindById(orderId);
            if (order == null)
            {
                AddNotification(nameof(Order), "Order not found");
                return new List<OrderMessage>();
            }

            var messages = await _orderMessageRepository.FindAllByOrderId(orderId);
            return messages;
        }
    }
}
