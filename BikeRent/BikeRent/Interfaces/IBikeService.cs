using BikeRent.Domain;
namespace BikeRent.Publisher.Interfaces
{
    public interface IBikeService: IServiceBase<Bike>
    {
        Task<Bike?> AddBike(Bike bike);
        Task<Bike?> UpdateLicensePlate(Guid id, string licensePlate);
    }
}