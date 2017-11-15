﻿using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using UserService.Service;
using UserService.Service.Interfaces;

namespace UserService.Config
{
    public class AutofacBuilder
    {
        public static IContainer Container;
        public IContainer Initialize()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<Service.UserService>().As<IUserService>();
            builder.RegisterType<UserServiceDto>().As<IUserServiceDto>();
            builder.RegisterType<UserRepository>().As<IUserRepository>();

            Container = builder.Build();

            return Container;
        }
    }
}