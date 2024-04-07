using AutoMapper;
using BikeRent.Domain.Entities;
using BikeRent.Infra.Interfaces;
using BikeRent.Publisher.Interfaces;
using BikeRent.Publisher.ViewModel;

namespace BikeRent.Publisher.Service
{
    public class DeliverymanService : ServiceBase<Deliveryman>, IDeliverymanService
    {
        private readonly IMapper _mapper;
        private readonly IDeliverymanRepository _deliverymanRepository;
        public DeliverymanService(IDeliverymanRepository repository, IMapper mapper) : base(repository)
        {
            _mapper = mapper;
            _deliverymanRepository = repository;
        }

        public async Task<Deliveryman?> Add(DeliverymanViewModel viewModel)
        {
            var deliveryMan = _mapper.Map<Deliveryman>(viewModel);
            AddNotifications(deliveryMan);
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
    }
}
