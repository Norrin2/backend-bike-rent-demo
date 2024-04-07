using BikeRent.Publisher.Interfaces;
using BikeRent.Publisher.Service;

namespace BikeRent.Publisher.Configuration
{
    public static class ServicesConfiguration
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IBikeService, BikeService>();
            services.AddScoped<IDeliverymanService, DeliverymanService>();

            return services;
        }
    }
}
