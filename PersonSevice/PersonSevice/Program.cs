using System;
using Autofac;
using PersonSevice.Config;
using PersonSevice.Models;
using RabbitCommunications.Recivers;

namespace PersonSevice
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var autofac = new AutofacBuilder();
            var container = autofac.Initialize();
            using (container)
            {
                ResponseReceiver.Recieve<PersonDto>("hello", "IPersonServiceDto", container);
                while (true)
                {
                    Console.ReadLine();
                }
            }
        }
    }
}
