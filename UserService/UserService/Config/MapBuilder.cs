using AutoMapper;
using RabbitCommunications.Models;
using System.Collections.Generic;
using UserService.Models;
using UserService.Models.Dto;

namespace UserService.Config
{
    public static class MapBuilder
    {
        private static bool _initialized;

        public static void BuildMap()
        {
            if (!_initialized)
            {
                Mapper.Initialize(cfg =>
                {
                    cfg.CreateMap<User, UserDto>().ReverseMap();
                    cfg.CreateMap<Response<User>, Response<UserDto>>();
                    cfg.CreateMap<Response<List<User>>, Response<List<UserDto>>>();
                });

                Mapper.Configuration.AssertConfigurationIsValid();

                _initialized = true;
            }
        }
    }
}