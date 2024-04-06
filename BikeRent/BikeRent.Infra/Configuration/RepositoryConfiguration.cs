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

            return services;
        }
    }
}
