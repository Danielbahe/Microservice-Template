using System;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using Autofac;
using Newtonsoft.Json;
using RabbitCommunications.Models;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace RabbitCommunications.Recivers
{
    public class ResponseReceiver
    {
        /// <summary>
        /// Start the listener
        /// </summary>
        /// <param name="queueName">The queue name we want to listen</param>
        /// <param name="serviceInterface">Interface name of the service we want to execute actions</param>
        /// <param name="container">container of app</param>
        /// <param name="hostName">Default "localhost"</param>
        public static void Recieve<T>(string queueName, string serviceInterface, IContainer container,
            string hostName = null)
        {
            if (string.IsNullOrEmpty(hostName)) hostName = "localhost";

            var factory = new ConnectionFactory() {HostName = hostName};
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: queueName, durable: false,
                    exclusive: false, autoDelete: false, arguments: null);
                channel.BasicQos(0, 1, false);
                var consumer = new EventingBasicConsumer(channel);
                channel.BasicConsume(queue: queueName,
                    autoAck: false, consumer: consumer);
                Console.WriteLine("Initializing... Queue Name:'" + queueName+ "' Service Name: '"+ serviceInterface+"'");
                
                consumer.Received += (model, ea) =>
                {
                    Response<T> response = new Response<T>();

                    var body = ea.Body;
                    var props = ea.BasicProperties;
                    var replyProps = channel.CreateBasicProperties();
                    replyProps.CorrelationId = props.CorrelationId;

                    try
                    {
                        var message = Encoding.UTF8.GetString(body);

                        var request = JsonConvert.DeserializeObject<RequestModel>(message);


                        var serviceType = Assembly.GetEntryAssembly().DefinedTypes
                            .First(s => s.IsInterface && s.Name == serviceInterface);

                        var service = container.Resolve(serviceType);

                        var methodName = request.Headers.First(k => k.Key.ToLowerInvariant() == "method").Value;
                        var method = service.GetType().GetMethod(methodName);
                        if (method == null) throw new NotImplementedException();

                        string[] args = {request.Body};

                        response = (Response<T>) method.Invoke(service, args);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(" [Error] " + e.Message);
                        response.Succes = false;
                        response.ExceptionList.Add(e);
                    }
                    finally
                    {
                        var clientResponse = MapResponse<T>(response);

                        var jsonResponse = JsonConvert.SerializeObject(clientResponse);

                        var responseBytes = Encoding.UTF8.GetBytes(jsonResponse);

                        channel.BasicPublish(exchange: "", routingKey: props.ReplyTo,
                            basicProperties: replyProps, body: responseBytes);
                        channel.BasicAck(deliveryTag: ea.DeliveryTag,
                            multiple: false);
                    }
                };

                Console.WriteLine("Awaiting RPC requests");
                Console.ReadLine();
            }
        }

        private static ClientResponse<T> MapResponse<T> (Response<T> response)
        {
            var clientResponse = new ClientResponse<T>();
            clientResponse.Data = response.Data;
            
            clientResponse.HaveException = response.HaveException;
            if (clientResponse.HaveException)
            {
                clientResponse.ErrorCode = response.ExceptionList.FirstOrDefault().GetType().ToString();
                clientResponse.ErrorMessage = response.GetError().Message;
            }
            clientResponse.Succes = response.Succes;

            return clientResponse;
        }
    }
}