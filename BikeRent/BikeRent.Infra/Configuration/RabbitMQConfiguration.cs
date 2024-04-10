using BikeRent.Infra.Interfaces;
using BikeRent.Infra.RabbitMq;
using Microsoft.Extensions.DependencyInjection;

namespace BikeRent.Infra.Configuration
{
    public static class RabbitMQConfiguration
    {
        public static IServiceCollection AddRabbitMQ(this IServiceCollection services)
        {
            services.AddSingleton<IRabbitMQConnectionFactory, RabbitMQConnectionFactory>();
            services.AddScoped<IMessageService, MessageService>();
            return services;
        }
    }
}
