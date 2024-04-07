using AutoMapper;
using BikeRent.Domain.Entities;
using BikeRent.Infra.Interfaces;
using BikeRent.Publisher.Interfaces;
using BikeRent.Publisher.ViewModel;

namespace BikeRent.Publisher.Service
{
    public class BikeService : ServiceBase<Bike>, IBikeService
    {
        private readonly IBikeRepository _bikeRepository;
        private readonly IMapper _mapper;
        public BikeService(IBikeRepository repository, IMapper mapper): base(repository) 
        { 
            _bikeRepository = repository;
            _mapper = mapper;
        }

        private async Task ValidateExistingLicensePlate(string licensePlate)
        {
            var existingBike = await _bikeRepository.FindByLicensePlate(licensePlate);
            if (existingBike != null)
            {
                AddNotification(licensePlate, "New license alredy exists");
            }
        }

        public async Task<Bike?> AddBike(BikeViewModel viewModel)
        {
            var bike = _mapper.Map<Bike>(viewModel);
            AddNotifications(bike);
            if (!IsValid) return null;

            await ValidateExistingLicensePlate(bike.LicensePlate);
            if (!IsValid) return null;

            await _repository.Add(bike);

            return bike;
        }

        public async Task<Bike?> UpdateLicensePlate(Guid id, string licensePlate)
        {
            var bike = await _repository.FindById(id);
            if (bike == null)
                return null;

            bike.UpdateLicensePlate(licensePlate);
            AddNotifications(bike);
            if (!IsValid) return null;

            await ValidateExistingLicensePlate(licensePlate);
            if (!IsValid) return null;

            await _repository.Update(bike);
            return bike;
        }
    }
}
