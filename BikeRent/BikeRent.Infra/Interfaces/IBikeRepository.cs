using BikeRent.Domain.Entities;

namespace BikeRent.Infra.Interfaces
{
    public interface IBikeRepository: IRepository<Bike>
    {
        Task<Bike?> FindByLicensePlate(string licensePlate);
    }
}