using System;
using System.Linq;
using System.Reflection;
using System.Text;
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
        /// <param name="hostName">Default "localhost"</param>
        public static void Recieve<T>(string queueName, string serviceInterface, string hostName = null)
        {
            if (string.IsNullOrEmpty(hostName)) hostName = "localhost";

            var factory = new ConnectionFactory() { HostName = hostName };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: queueName, durable: false,
                  exclusive: false, autoDelete: false, arguments: null);
                channel.BasicQos(0, 1, false);
                var consumer = new EventingBasicConsumer(channel);
                channel.BasicConsume(queue: queueName,
                  autoAck: false, consumer: consumer);
                Console.WriteLine(" [x] Awaiting RPC requests");

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


                        //Get service
                        var serviceType = Assembly.GetEntryAssembly().DefinedTypes.First(s => s.ImplementedInterfaces.Where( i=>i.Name == serviceInterface) != null && s.IsClass);

                        var service = Activator.CreateInstance(serviceType.AsType());

                        var methodName = request.Headers.First(k => k.Key.ToLowerInvariant() == "method").Value;
                        var method = service.GetType().GetMethod(methodName);
                        if (method == null) throw new NotImplementedException();
                        
                        string[] args = new string[] { request.Body };
                        
                        response = (Response<T>)method.Invoke(service, args);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(" [.] " + e.Message);
                        response.Succes = false;
                    }
                    finally
                    {
                        var jsonResponse = JsonConvert.SerializeObject(response);

                        var responseBytes = Encoding.UTF8.GetBytes(jsonResponse);

                        channel.BasicPublish(exchange: "", routingKey: props.ReplyTo,
                          basicProperties: replyProps, body: responseBytes);
                        channel.BasicAck(deliveryTag: ea.DeliveryTag,
                          multiple: false);
                    }
                };

                Console.WriteLine(" Press [enter] to exit.");
                Console.ReadLine();
            }
        }
    }
}