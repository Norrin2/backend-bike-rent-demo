using BikeRent.Domain.Entities;
using BikeRent.Infra.Interfaces;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace BikeRent.Consumer
{
    public class Worker : BackgroundService
    {
        private static readonly int WAIT_TIME = 15000;

        private readonly ILogger<Worker> _logger;
        private readonly IRabbitMQConnectionFactory _connectionFactory;
        private readonly IOrderMessageRepository _orderMessageRepository;

        public Worker(ILogger<Worker> logger, IRabbitMQConnectionFactory connectionFactory, IOrderMessageRepository orderMessageRepository)
        {
            _logger = logger;
            _connectionFactory = connectionFactory;
            _orderMessageRepository = orderMessageRepository;
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

            var consumer = new AsyncEventingBasicConsumer(channel);
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

        private async Task Consumer_Received(
            object sender, BasicDeliverEventArgs e)
        {
            var body = Encoding.UTF8.GetString(e.Body.ToArray());
            _logger.LogInformation(
                $"[new Message | {DateTime.Now:yyyy-MM-dd HH:mm:ss}] {body} ");

            try
            {
                var orderMessage = JsonSerializer.Deserialize<OrderMessage>(body);
                await _orderMessageRepository.Add(orderMessage);
            } catch (Exception ex)
            {
                _logger.LogError(
                    $"Error reading message: {ex} ");
            }

        }
    }
}
