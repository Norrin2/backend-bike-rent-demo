using BikeRent.Domain;

namespace BikeRent.Infra.Interfaces
{
    public interface IBikeRepository: IRepository<Bike>
    {
        Task<Bike?> FindByLicensePlate(string licensePlate);
    }
}