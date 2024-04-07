using AutoMapper;
using BikeRent.Domain.Entities;
using BikeRent.Domain.ValueObject;
using BikeRent.Publisher.ViewModel;

namespace BikeRent.Publisher.Mappings
{
    public class DeliverymanProfile: Profile
    {
        public DeliverymanProfile() 
        {
            CreateMap<CnhViewModel, Cnh>();
            CreateMap<DeliverymanViewModel, Deliveryman>();
        }
    }
}
