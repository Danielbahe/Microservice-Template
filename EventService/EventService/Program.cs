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

            PersonListWorker personListWorker = new PersonListWorker();
            Thread workerThread1 = new Thread(personListWorker.DoWork);
            workerThread1.Start();

            UpdatePersonDatabaseWorker updatePersonDatabaseWorker = new UpdatePersonDatabaseWorker();
            Thread workerThread2 = new Thread(updatePersonDatabaseWorker.DoWork);
            workerThread2.Start();


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

    public class PersonListWorker
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

    public class UpdatePersonDatabaseWorker 
    {
        public void DoWork()
        {
            var autofac = new AutofacBuilder();
            var container = autofac.Initialize();

            using (container)
            {
                ResponseReceiver.Recieve<EventDto>("persondbqueue", "IEventServiceDto", container);
                while (true)
                {
                    Console.ReadLine();
                }
            }
        }
    }
}    
