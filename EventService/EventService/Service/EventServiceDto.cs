﻿using System.Collections.Generic;
using AutoMapper;
using EventService.Models;
using EventService.Service.Interfaces;
using Newtonsoft.Json;
using RabbitCommunications.Models;

namespace EventService.Service
{
    public class EventServiceDto: IEventServiceDto
    {
        public IEventService EventService { get; set; }

        public EventServiceDto(IEventService EventService)
        {
            this.EventService = EventService;
        }

        public Response<List<EventDto>> GetWeekEvents(string jsonDate)
        {
            var eventDto = JsonConvert.DeserializeObject<EventDto>(jsonDate);
            var date = eventDto.Date;
            var response = this.EventService.GetWeekEvents(date);


            var eventDtoList = new List<EventDto>();

            foreach (var Event in response.Data)
            {
                eventDtoList.Add(Mapper.Map<EventDto>(Event));
            }

            var responseDto = new Response<List<EventDto>>
            {
                Data = eventDtoList,
                ExceptionList = response.ExceptionList,
                Succes = response.Succes
            };

            return responseDto;
        }

        public Response<EventDto> SaveEvent(string jsonEvent)
        {
            throw new System.NotImplementedException();
        }

        public Response<EventDto> CreateEvent(string jsonEvent)
        {
            throw new System.NotImplementedException();
        }

        public Response<EventDto> DeleteEvent(string jsonEvent)
        {
            throw new System.NotImplementedException();
        }

        private Event MapDto(string dto)
        {
            var eventDto = JsonConvert.DeserializeObject<EventDto>(dto);
            var user = Mapper.Map<Event>(eventDto);
            return user;

        }

        private Response<EventDto> MapResponse(Response<Event> response)
        {
            var responseDto = Mapper.Map<Response<EventDto>>(response);
            return responseDto;
        }
    }
}