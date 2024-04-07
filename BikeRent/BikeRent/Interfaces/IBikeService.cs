using BikeRent.Domain.Entities;
using BikeRent.Publisher.ViewModel;
namespace BikeRent.Publisher.Interfaces
{
    public interface IBikeService: IServiceBase<Bike>
    {
        Task<Bike?> AddBike(BikeViewModel viewModel);
        Task<Bike?> UpdateLicensePlate(Guid id, string licensePlate);
    }
}