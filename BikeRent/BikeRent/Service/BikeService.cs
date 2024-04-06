using BikeRent.Domain;
using BikeRent.Infra.Interfaces;
using Flunt.Notifications;

namespace BikeRent.Publisher.Service
{
    public class BikeService: Notifiable<Notification>
    {
        private readonly IRepository<Bike> _repository;

        public BikeService(IRepository<Bike> repository)
        {
            _repository = repository;
        }

        public async Task<Bike?> AddBike(Bike bike)
        {
            AddNotifications(bike);

            if (!IsValid) return null;

            await _repository.Add(bike);
            return bike;
        }

        public async Task<Bike?> UpdateLicensePlate(Bike bike)
        {

        }
    }
}
