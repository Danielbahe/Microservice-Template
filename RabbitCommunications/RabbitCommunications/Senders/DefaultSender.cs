using System.Text;
using RabbitMQ.Client;

namespace RabbitCommunications.Senders
{
    public class DefaultSender
    {
        public static void Send(string message, string queueName, string hostName = null)
        {
            if (string.IsNullOrEmpty(hostName)) hostName = "localhost";

            var factory = new ConnectionFactory() {HostName = hostName};
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: queueName,
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);

                var body = Encoding.UTF8.GetBytes(message);

                var properties = channel.CreateBasicProperties();
                properties.Persistent = true;

                channel.BasicPublish(exchange: "",
                    routingKey: queueName,
                    basicProperties: properties,
                    body: body);
            }
        }
    }
}