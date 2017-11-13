using System;
using System.Collections.Generic;
using System.Threading;
using RabbitCommunications.Recivers;
using UserService.Config;
using UserService.Models.Dto;

namespace UserService
{
    class Program
    {
        static void Main()
        {
            Worker workerObject = new Worker();
            Thread workerThread = new Thread(workerObject.DoWork);
            workerThread.Start();
            Console.WriteLine("main thread: Starting worker thread...");
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
