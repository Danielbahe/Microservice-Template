using System.Collections.Generic;
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

        public Response<List<Event>> GetWeekEvents(string date)
        {
            var eventList = this.EventServiceRepository.GetWeekEvents(date);
            return eventList;
        }

        public Response<Event> SaveEvent(Event Event)
        {
            throw new System.NotImplementedException();
        }

        public Response<Event> CreateEvent(Event Event)
        {
            throw new System.NotImplementedException();
        }

        public Response<Event> DeleteEvent(Event Event)
        {
            throw new System.NotImplementedException();
        }
    }
}