using AutoMapper;
using BikeRent.Domain.Entities;
using BikeRent.Domain.ValueObject;
using BikeRent.Infra.Interfaces;
using BikeRent.Publisher.Interfaces;
using BikeRent.Publisher.ViewModel;

namespace BikeRent.Publisher.Service
{
    public class DeliverymanService : ServiceBase<Deliveryman>, IDeliverymanService
    {
        private readonly IMapper _mapper;
        private readonly IDeliverymanRepository _deliverymanRepository;
        private readonly IBikeRepository _bikeRepository;
        private readonly IRepository<Order> _orderRepository;

        public DeliverymanService(IMapper mapper, IDeliverymanRepository deliverymanRepository, IBikeRepository bikeRepository, IRepository<Order> orderRepository) : base(deliverymanRepository)
        {
            _mapper = mapper;
            _deliverymanRepository = deliverymanRepository;
            _bikeRepository = bikeRepository;
            _orderRepository = orderRepository;
        }

        public async Task<Deliveryman?> Add(DeliverymanViewModel viewModel)
        {
            var deliveryMan = _mapper.Map<Deliveryman>(viewModel);
            AddNotifications(deliveryMan.Notifications);
            if (!IsValid) return null;

            await Task.WhenAll(ValidateExistingCnh(viewModel.Cnh.Number),
                               ValidateExistingCnpj(viewModel.Cnpj));
            if (!IsValid) return null;

            await _deliverymanRepository.Add(deliveryMan);
            return deliveryMan;
        }

        private async Task ValidateExistingCnpj(string cnpj)
        {
            var existingDeliveryman = await _deliverymanRepository.FindByCnpj(cnpj);
            if (existingDeliveryman != null)
            {
                AddNotification(cnpj, "CNPJ alredy exists");
            }
        }

        private async Task ValidateExistingCnh(string cnh)
        {
            var existingDeliveryman = await _deliverymanRepository.FindByCnh(cnh);
            if (existingDeliveryman != null)
            {
                AddNotification(cnh, "CNH alredy exists");
            }
        }

        public async Task<Deliveryman?> RentBike(BikeRentViewModel viewModel)
        {
            
            var bikeTask = _bikeRepository.FindById(viewModel.BikeId);
            var deliveryManTask = _deliverymanRepository.FindById(viewModel.DeliveryManId);

            await Task.WhenAll(bikeTask, deliveryManTask);
            var bike = bikeTask.Result;
            var deliveryman = deliveryManTask.Result;

            if (bike == null)
            {
                AddNotification(nameof(bike), "Bike not found");
                return null;
            }

            if (await _deliverymanRepository.CheckIfBikeIsRentedByADeliveryman(viewModel.BikeId))
            {
                AddNotification(nameof(bike), "Bike is not available");
                return null;
            }

            if (deliveryman == null)
            {
                AddNotification(nameof(bike), "Deliveryman not found");
                return null;
            }

            deliveryman.RentBike(bike, viewModel.Plan);
            AddNotifications(deliveryman.Notifications);

            if (IsValid)
            {
                await _deliverymanRepository.Update(deliveryman);
            }
            return deliveryman;
        }

        public async Task<decimal> FinishRentAndGetCost(FinishRentViewModel viewModel)
        {
            var bikeTask = _bikeRepository.FindById(viewModel.BikeId);
            var deliveryManTask = _deliverymanRepository.FindById(viewModel.DeliveryManId);

            await Task.WhenAll(bikeTask, deliveryManTask);
            var bike = bikeTask.Result;
            var deliveryman = deliveryManTask.Result;
            decimal cost = 0;
            if (bike == null)
            {
                AddNotification(nameof(bike), "Bike not found");
                return cost;
            }

            if (deliveryman == null)
            {
                AddNotification(nameof(bike), "Deliveryman not found");
                return cost;
            }

            cost = deliveryman.FinishRentAndGetCost(bike, viewModel.EndDate);
            AddNotifications(deliveryman.Notifications);

            if (IsValid)
            {
                await _deliverymanRepository.Update(deliveryman);
            }

            return cost;
        }

        public async Task AcceptOrder(AcceptOrderViewModel viewModel)
        {
            var orderTask = _orderRepository.FindById(viewModel.OrderId);
            var deliveryManTask = _deliverymanRepository.FindById(viewModel.DeliverymanId);

            await Task.WhenAll(orderTask, deliveryManTask);
            var order = orderTask.Result;
            var deliveryman = deliveryManTask.Result;
            if (order == null)
            {
                AddNotification(nameof(order), "Order not found");
                return;
            }

            if (deliveryman == null)
            {
                AddNotification(nameof(deliveryman), "Deliveryman not found");
                return;
            }

            if (!order.OrderNotifications.Any(n => n.DeliveryManId == deliveryman.Id))
            {
                AddNotification(nameof(OrderMessage), "Cant accept an order you have not been notified of");
                return;
            }

            if (order.Status != OrderStatus.Availabe)
            {
                AddNotification(nameof(OrderStatus), "Order is not Available");
                return;
            }

            deliveryman.StartOrder(order.Id);
            order.AcceptOrder();

            await Task.WhenAll(_deliverymanRepository.Update(deliveryman),
                               _orderRepository.Update(order));

        }

        public async Task FinishOrder(Guid deliverymanId)
        {
            var deliveryman = await _repository.FindById(deliverymanId);
            if (deliveryman == null)
            {
                AddNotification(nameof(deliveryman), "Deliveryman not found");
                return;
            }

            if (deliveryman.CurrentOrderId == null)
            {
                AddNotification(nameof(Order), "Deliveryman has no order");
                return;
            }

            var order = await _orderRepository.FindById(deliveryman.CurrentOrderId.GetValueOrDefault());
            order.FinishOrder();
            deliveryman.FinishOrder();

            await Task.WhenAll(_deliverymanRepository.Update(deliveryman),
                   _orderRepository.Update(order));
        }
    }
}
