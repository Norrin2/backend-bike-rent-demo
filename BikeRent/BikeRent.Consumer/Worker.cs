using BikeRent.Infra.Interfaces;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace BikeRent.Consumer
{
    public class Worker : BackgroundService
    {
        private static int WAIT_TIME = 15000;

        private readonly ILogger<Worker> _logger;
        private readonly IRabbitMQConnectionFactory _connectionFactory;
        public Worker(ILogger<Worker> logger, IRabbitMQConnectionFactory connectionFactory)
        {
            _logger = logger;
            _connectionFactory = connectionFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var connection = _connectionFactory.GetConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(queue: "order",
                                durable: false,
                                exclusive: false,
                                autoDelete: false,
                                arguments: null);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += Consumer_Received;
            channel.BasicConsume(
                "order",
                autoAck: true,
                consumer: consumer);

            while (!stoppingToken.IsCancellationRequested)
            {
                if (_logger.IsEnabled(LogLevel.Information))
                {
                    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                }
                await Task.Delay(WAIT_TIME, stoppingToken);
            }
        }

        private void Consumer_Received(
            object sender, BasicDeliverEventArgs e)
        {
            _logger.LogInformation(
                $"[new Message | {DateTime.Now:yyyy-MM-dd HH:mm:ss}] " +
                Encoding.UTF8.GetString(e.Body.ToArray()));
        }
    }
}
