using System;
using System.Collections.Generic;
using System.Threading;
using EventService.Config;
using EventService.Models;
using RabbitCommunications.Recivers;

namespace EventService
{
    class Program
    {
        static void Main()
        {
            MapBuilder.BuildMap();

            Worker workerObject = new Worker();
            Thread workerThread = new Thread(workerObject.DoWork);
            workerThread.Start();

            var autofac = new AutofacBuilder();
            var container = autofac.Initialize();

            using (container)
            {
                ResponseReceiver.Recieve<EventDto>("eventqueue", "IEventServiceDto", container);
                while (true)
                {
                    Console.ReadLine();
                }
            }
        }
    }

    public class Worker
    {
        public void DoWork()
        {
            var autofac = new AutofacBuilder();
            var container = autofac.Initialize();

            using (container)
            {
                ResponseReceiver.Recieve<List<EventDto>>("eventlistqueue", "IEventServiceDto", container);
                while (true)
                {
                    Console.ReadLine();
                }
            }
        }
    }
}    
