using BikeRent.Domain;
using BikeRent.Infra.Interfaces;
using BikeRent.Publisher.Interfaces;

namespace BikeRent.Publisher.Service
{
    public class BikeService : ServiceBase<Bike>, IBikeService
    {
        private readonly IBikeRepository _bikeRepository;
        public BikeService(IBikeRepository repository): base(repository) 
        { 
            _bikeRepository = repository;
        }

        private async Task ValidateExistingLicensePlate(string licensePlate)
        {
            var existingBike = await _bikeRepository.FindByLicensePlate(licensePlate);
            if (existingBike != null)
            {
                AddNotification(licensePlate, "New license alredy exists");
            }
        }

        public async Task<Bike?> AddBike(Bike bike)
        {
            AddNotifications(bike);
            if (!IsValid) return null;

            await ValidateExistingLicensePlate(bike.LicensePlate);
            if (!IsValid) return null;

            await _repository.Add(bike);
            await _repository.SaveChanges();
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
