using System.Collections.Generic;
using AutoMapper;
using EventService.Models;
using EventService.Service.Interfaces;
using Newtonsoft.Json;
using RabbitCommunications.Models;

namespace EventService.Service
{
    public class EventServiceDto : IEventServiceDto
    {
        public IEventService EventService { get; set; }

        public EventServiceDto(IEventService EventService)
        {
            this.EventService = EventService;
        }

        public Response<List<EventDto>> GetWeekEvents(string jsonEvent)
        {
            var eventDto = JsonConvert.DeserializeObject<EventDto>(jsonEvent);
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
            var eventDto = JsonConvert.DeserializeObject<EventDto>(jsonEvent);
            var Event = Mapper.Map<Event>(eventDto);

            var response = this.EventService.SaveEvent(Event);

            var responseDto = Mapper.Map<Response<EventDto>>(response);

            return responseDto;
        }

        public Response<EventDto> CreateEvent(string jsonEvent)
        {
            var eventDto = JsonConvert.DeserializeObject<EventDto>(jsonEvent);
            var Event = Mapper.Map<Event>(eventDto);

            var response = this.EventService.CreateEvent(Event);

            var responseDto = Mapper.Map<Response<EventDto>>(response);

            return responseDto;
        }

        public Response<EventDto> DeleteEvent(string jsonEvent)
        {
            var eventDto = JsonConvert.DeserializeObject<EventDto>(jsonEvent);
            var Event = Mapper.Map<Event>(eventDto);

            var response = this.EventService.DeleteEvent(Event);

            var responseDto = Mapper.Map<Response<EventDto>>(response);

            return responseDto;
        }

        public Response<EventDto> AsistBusToEvent(string jsonEvent)
        {
            var eventDto = JsonConvert.DeserializeObject<EventDto>(jsonEvent);
            var Event = Mapper.Map<Event>(eventDto);

            var response = this.EventService.AsistBusToEvent(Event);

            var responseDto = Mapper.Map<Response<EventDto>>(response);

            return responseDto;
        }

        public Response<EventDto> AsistDietToEvent(string jsonEvent)
        {
            var eventDto = JsonConvert.DeserializeObject<EventDto>(jsonEvent);
            var Event = Mapper.Map<Event>(eventDto);

            var response = this.EventService.AsistDietToEvent(Event);

            var responseDto = Mapper.Map<Response<EventDto>>(response);

            return responseDto;
        }

        public Response<EventDto> AsistVeganToEvent(string jsonEvent)
        {
            var eventDto = JsonConvert.DeserializeObject<EventDto>(jsonEvent);
            var Event = Mapper.Map<Event>(eventDto);

            var response = this.EventService.AsistVeganToEvent(Event);

            var responseDto = Mapper.Map<Response<EventDto>>(response);

            return responseDto;
        }

        public Response<EventDto> CancelAsistBusToEvent(string eventData)
        {
            var eventDto = JsonConvert.DeserializeObject<EventDto>(eventData);
            var Event = Mapper.Map<Event>(eventDto);

            var response = this.EventService.CancelAsistBusToEvent(Event);

            var responseDto = Mapper.Map<Response<EventDto>>(response);

            return responseDto;
        }

        public Response<EventDto> CancelAsistDietToEvent(string eventData)
        {
            var eventDto = JsonConvert.DeserializeObject<EventDto>(eventData);
            var Event = Mapper.Map<Event>(eventDto);

            var response = this.EventService.CancelAsistDietToEvent(Event);

            var responseDto = Mapper.Map<Response<EventDto>>(response);

            return responseDto;
        }

        public Response<EventDto> CancelAsistVeganToEvent(string eventData)
        {
            var eventDto = JsonConvert.DeserializeObject<EventDto>(eventData);
            var Event = Mapper.Map<Event>(eventDto);

            var response = this.EventService.CancelAsistVeganToEvent(Event);

            var responseDto = Mapper.Map<Response<EventDto>>(response);

            return responseDto;
        }

        public Response<EventDto> GetEventAssistance(string eventData)
        {
            var eventDto = JsonConvert.DeserializeObject<EventDto>(eventData);
            var Event = Mapper.Map<Event>(eventDto);

            var response = this.EventService.GetEventAssistance(Event);

            var responseDto = Mapper.Map<Response<EventDto>>(response);

            return responseDto;
        }

        public Response<EventDto> UpdatePersonsDatabase(string personJson)
        {
            var person = JsonConvert.DeserializeObject<Person>(personJson);

            var response = this.EventService.UpdatePersonsDatabase(person);

            var responseDto = Mapper.Map<Response<EventDto>>(response);

            return responseDto;
        }

        public Response<EventDto> InsertPersonsDatabase(string personJson)
        {
            var person = JsonConvert.DeserializeObject<Person>(personJson);

            var response = this.EventService.UpdatePersonsDatabase(person);

            var responseDto = Mapper.Map<Response<EventDto>>(response);

            return responseDto;
        }

        public Response<EventDto> DeletePerson(string personJson)
        {
            var person = JsonConvert.DeserializeObject<Person>(personJson);

            var response = this.EventService.UpdatePersonsDatabase(person);

            var responseDto = Mapper.Map<Response<EventDto>>(response);

            return responseDto;
        }


        public Response<EventDto> AsistToEvent(string jsonEvent)
        {
            var eventDto = JsonConvert.DeserializeObject<EventDto>(jsonEvent);
            var Event = Mapper.Map<Event>(eventDto);

            var response = this.EventService.AsistToEvent(Event);

            var responseDto = Mapper.Map<Response<EventDto>>(response);

            return responseDto;
        }
    }
}