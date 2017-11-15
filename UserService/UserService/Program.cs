using System;
using System.Collections.Generic;
using System.Threading;
using RabbitCommunications.Recivers;
using UserService.Config;
using UserService.Mail;
using UserService.Models.Dto;
using UserService.Service;

namespace UserService
{
    class Program
    {
        static void Main()
        {
            SendEmailTest();
            Worker workerObject = new Worker();
            Thread workerThread = new Thread(workerObject.DoWork);
            workerThread.Start();
            Console.WriteLine("main thread: Starting worker thread...");
        }

        private static void SendEmailTest()
        {
            var emailService = new EmailService(new EmailConfiguration());
            var message = new EmailMessage
            {
                Content = "miau miau miau www.google.com",
                FromAddresses = new List<EmailAddress> { new EmailAddress { Address = "danielbahedev@gmail.com", Name = "Daniel"} },
                Subject ="Miau test",
                ToAddresses = new List<EmailAddress> { new EmailAddress { Address = "danielbahedev@gmail.com", Name = "Daniel" } },
            };
            emailService.Send(message);
        }
    }

    public class Worker
    {
        public void DoWork()
        {
            MapBuilder.BuildMap();
            var autofac = new AutofacBuilder();
            var container = autofac.Initialize();

            using (container)
            {
                ResponseReceiver.Recieve<UserDto>("userqueue", "IUserServiceDto", container);
                ResponseReceiver.Recieve<List<UserDto>>("userlistqueue", "IUserServiceDto", container);
                while (true)
                {
                    Console.ReadLine();
                }
            }
        }
    }
}
