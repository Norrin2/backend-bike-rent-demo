using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;

namespace BikeRent.Infra.Interfaces
{
    public interface IRabbitMQConnectionFactory
    {
        IConnection GetConnection();
    }
}