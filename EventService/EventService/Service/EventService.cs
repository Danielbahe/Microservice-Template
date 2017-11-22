using System;
using System.Collections.Generic;
using System.Linq;
using EventService.Models;
using EventService.Service.Interfaces;
using RabbitCommunications.Models;

namespace EventService.Service
{
    public class EventService : IEventService
    {
        public IEventServiceRepository EventServiceRepository { get; set; }

        public EventService(IEventServiceRepository EventServiceRepository)
        {
            this.EventServiceRepository = EventServiceRepository;
        }

        public Response<List<Event>> GetWeekEvents(DateTime date)
        {
            var eventList = this.EventServiceRepository.GetWeekEvents(date);
            return eventList;
        }

        public Response<Event> SaveEvent(Event Event)
        {
            var eve = this.EventServiceRepository.SaveEvent(Event);
            return eve;
        }

        public Response<Event> CreateEvent(Event Event)
        {
            var eve = this.EventServiceRepository.CreateEvent(Event);
            return eve;
        }

        public Response<Event> DeleteEvent(Event Event)
        {
            var eve = this.EventServiceRepository.DeleteEvent(Event);
            return eve;
        }

        public Response<Event> AsistToEvent(Event eventData)
        {
            var eventList = this.EventServiceRepository.AsistToEvent(eventData);
            return eventList;
        }

        public Response<Event> CancelAsistToEvent(Event eventData)
        {
            var eve = this.EventServiceRepository.CancelAsistToEvent(eventData);
            return eve;
        }

        public Response<Event> AsistBusToEvent(Event eventData)
        {
            var eve = this.EventServiceRepository.AsistBusToEvent(eventData);
            return eve;
        }

        public Response<Event> CancelAsistBusToEvent(Event eventData)
        {
            var eve = this.EventServiceRepository.CancelAsistBusToEvent(eventData);
            return eve;
        }

        public Response<Event> AsistDietToEvent(Event eventData)
        {
            var eventList = this.EventServiceRepository.AsistDietToEvent(eventData);
            return eventList;
        }

        public Response<Event> CancelAsistDietToEvent(Event eventData)
        {
            var eve = this.EventServiceRepository.CancelAsistDietToEvent(eventData);
            return eve;
        }

        public Response<Event> AsistVeganToEvent(Event eventData)
        {
            var eventList = this.EventServiceRepository.AsistVeganToEvent(eventData);
            return eventList;
        }

        public Response<Event> CancelAsistVeganToEvent(Event eventData)
        {
            var eve = this.EventServiceRepository.CancelAsistVeganToEvent(eventData);
            return eve;
        }

        public Response<Event> GetEventAssistance(Event eventData)
        {
            var eve = this.EventServiceRepository.GetEventAssistance(eventData);
            return eve;
        }

        public Response<Event> UpdatePerson(Person person)
        {
            var eve = this.EventServiceRepository.UpdatePerson(person);
            return eve;
        }

        public Response<Event> AddPerson(Person person)
        {
            var eve = this.EventServiceRepository.AddPerson(person);
            return eve;
        }

        public Response<Event> DeletePerson(Person person)
        {
            var eve = this.EventServiceRepository.DeletePerson(person);
            return eve;
        }
    }
}