using AutoMapper;
using EventService.Models;
using RabbitCommunications.Models;

namespace EventService.Config
{
    public static class MapBuilder
    {
        private static bool _initialized;
        public static void BuildMap()
        {
            if (!_initialized)
            {
                Mapper.Initialize(cfg => {
                    cfg.CreateMap<User, UserDto>().ReverseMap();
                    cfg.CreateMap<Response<User>, Response<UserDto>>();
                });

                Mapper.Configuration.AssertConfigurationIsValid();

                _initialized = true;
            }
        }

    }
}