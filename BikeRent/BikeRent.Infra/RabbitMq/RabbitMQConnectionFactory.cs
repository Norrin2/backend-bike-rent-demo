using BikeRent.Infra.Interfaces;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using System.Text.Json;

namespace BikeRent.Infra.RabbitMq
{
    public class RabbitMQConnectionFactory : IRabbitMQConnectionFactory
    {
        private readonly IConfiguration _configuration;

        private IConnection _connection;

        public RabbitMQConnectionFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IConnection GetConnection()
        {
            if (_connection != null)
                return _connection;

            var rabbitMQSettings = _configuration.GetSection("RabbitMQ");
            var factory = new ConnectionFactory()
            {
                UserName = rabbitMQSettings.GetSection("UserName").Value,
                Password = rabbitMQSettings.GetSection("Password").Value,
                Port = int.Parse(rabbitMQSettings.GetSection("Port").Value),
                HostName = rabbitMQSettings.GetSection("HostName").Value,
            };

            _connection = factory.CreateConnection();

            using var channel = _connection.CreateModel();
            channel.ExchangeDeclare("orderExchange", ExchangeType.Fanout);
           channel.Close();

            return _connection;
        }

        public void Dispose()
        {
            if (_connection != null)
            {
                _connection?.Close();
                _connection?.Dispose();
            }
        }
    }
}
