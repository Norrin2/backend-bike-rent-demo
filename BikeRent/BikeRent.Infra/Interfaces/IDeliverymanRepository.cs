using BikeRent.Domain.Entities;

namespace BikeRent.Infra.Interfaces
{
    public interface IDeliverymanRepository: IRepository<Deliveryman>
    {
        Task<Deliveryman?> FindByCnh(string cnh);
        Task<Deliveryman?> FindByCnpj(string cnpj);
        Task<bool> CheckIfBikeIsRentedByADeliveryman(Guid bikeId);
    }
}