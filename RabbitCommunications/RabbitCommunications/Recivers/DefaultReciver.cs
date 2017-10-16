using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace RabbitCommunications.Recivers
{
    public class DefaultReceiver
    {
        public static void Recieve(string queueName, string hostName = null)
        {
            if (string.IsNullOrEmpty(hostName)) hostName = "localhost";

            var factory = new ConnectionFactory() { HostName = hostName };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: queueName,
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

                var consumer = new EventingBasicConsumer(channel);

                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body;
                    var message = Encoding.UTF8.GetString(body);

                    Console.WriteLine(" [x] Received {0}", message);

                    channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
                };
                channel.BasicConsume(queue: "hello",
                                     autoAck: false,
                                     consumer: consumer);

                Console.WriteLine("Micro service 1 running");
                while (true)
                {
                    Console.ReadLine();
                }
            }
        }
    }
}