using System.Collections.Generic;
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
                Mapper.Initialize(cfg =>
                {
                    cfg.CreateMap<Event, EventDto>().ReverseMap();
                    cfg.CreateMap<Response<Event>, Response<EventDto>>().ReverseMap();
                    cfg.CreateMap<Response<List<Event>>, Response<List<EventDto>>>().ReverseMap();
                });

                Mapper.Configuration.AssertConfigurationIsValid();

                _initialized = true;
            }
        }
    }
}