using System;
using System.Collections.Generic;
using System.Text;
using Autofac;

namespace TransactionService
{
    class AutofacBuilder
    {
        public static IContainer Container;
        public IContainer Initialize()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<TransactionService>().As<ITransactionService>();
            Container = builder.Build();

            return Container;
        }
    }
}
