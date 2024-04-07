using AutoMapper;
using BikeRent.Domain.Entities;
using BikeRent.Publisher.ViewModel;

namespace BikeRent.Publisher.Mappings
{
    public class BikeProfile : Profile
    {
        public BikeProfile()
        {
            CreateMap<BikeViewModel, Bike>();
        }
    }
}
