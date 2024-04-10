using BikeRent.Domain.Entities;
using BikeRent.Infra.Database;
using BikeRent.Infra.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace BikeRent.Infra.Configuration
{
    public static class RepositoryConfiguration
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IBikeRepository, BikeRepository>();
            services.AddScoped<IDeliverymanRepository, DeliverymanRepository>();
            services.AddScoped<IRepository<Order>, OrderRepository>();
            services.AddSingleton<IOrderMessageRepository, OrderMessageRepository>();

            return services;
        }
    }
}
