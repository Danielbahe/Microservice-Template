using System;
using RabbitCommunications.Recivers;

namespace TransactionService
{
    class Program
    {
        static void Main()
        {
            var autofac = new AutofacBuilder();
            var container = autofac.Initialize();
            using (container)
            {
                ResponseReceiver.Recieve<string>("transaction", "ITransactionService", container);
                while (true)
                {
                    Console.ReadLine();
                }
            }
        }
    }
}
