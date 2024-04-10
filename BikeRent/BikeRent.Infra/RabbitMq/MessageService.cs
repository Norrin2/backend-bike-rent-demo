using BikeRent.Infra.Interfaces;
using System.Text;

namespace BikeRent.Infra.RabbitMq
{
    public class MessageService : IMessageService
    {
        private readonly IRabbitMQConnectionFactory _connectionFactory;

        public MessageService(IRabbitMQConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public void PublishMessages(IEnumerable<string> messages)
        {
            var connection = _connectionFactory.GetConnection();
            using var channel = connection.CreateModel();

            foreach (string message in messages)
            {
                ReadOnlyMemory<byte> body = Encoding.UTF8.GetBytes(message);
                channel.BasicPublish(exchange: "",
                                     routingKey: "order",
                                     mandatory: true,
                                     basicProperties: null,
                                     body: body);
            }


        }

    }
}
