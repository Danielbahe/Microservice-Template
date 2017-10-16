using System;
using PersonSevice.Models;
using RabbitCommunications.Recivers;

namespace PersonSevice
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ResponseReceiver.Recieve<PersonDto>("hello", "IPersonServiceDto");
            while (true)
            {
                Console.ReadLine();
            }            
        }
    }
}
