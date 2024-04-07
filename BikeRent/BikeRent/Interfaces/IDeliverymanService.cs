using BikeRent.Domain.Entities;
using BikeRent.Publisher.ViewModel;

namespace BikeRent.Publisher.Interfaces
{
    public interface IDeliverymanService: IServiceBase<Deliveryman>
    {
        Task<Deliveryman?> Add(DeliverymanViewModel viewModel);
    }
}