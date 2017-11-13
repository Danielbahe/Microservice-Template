using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using RabbitCommunications.Models;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace RabbitCommunications.Senders
{
    public class ResponseSender
    {
        private readonly IConnection connection;
        private readonly IModel channel;
        private readonly string replyQueueName;
        private readonly EventingBasicConsumer consumer;
        private readonly BlockingCollection<string> respQueue = new BlockingCollection<string>();
        private readonly IBasicProperties props;

        public ResponseSender(string hostName = null)
        {
            if (string.IsNullOrEmpty(hostName)) hostName = "localhost";

            var factory = new ConnectionFactory() { HostName = hostName };

            connection = factory.CreateConnection();
            channel = connection.CreateModel();
            replyQueueName = channel.QueueDeclare().QueueName;
            consumer = new EventingBasicConsumer(channel);

            props = channel.CreateBasicProperties();
            var correlationId = Guid.NewGuid().ToString();
            props.CorrelationId = correlationId;
            props.ReplyTo = replyQueueName;

            consumer.Received += (model, ea) =>
            {
                var body = ea.Body;
                var response = Encoding.UTF8.GetString(body);
                if (ea.BasicProperties.CorrelationId == correlationId)
                {
                    respQueue.Add(response);
                }
            };
        }

        public string Call(string message, string queueName)
        {
            var messageBytes = Encoding.UTF8.GetBytes(message);
            channel.BasicPublish(
                exchange: "",
                routingKey: queueName,
                basicProperties: props,
                body: messageBytes);

            channel.BasicConsume(
                consumer: consumer,
                queue: replyQueueName,
                autoAck: true);

            var response = respQueue.Take();

            return response;
        }

        public string IsHealthy(string queueName)
        {
            var headers = new Dictionary<string, string>();
            headers.Add("method", "ishealthy");

            var request = new RequestModel
            {
                Headers = headers
            };

            var message = JsonConvert.SerializeObject(request);

            var response = this.Call(message, queueName);

            return response;
        }

        public void Close()
        {
            connection.Close();
        }
    }
}